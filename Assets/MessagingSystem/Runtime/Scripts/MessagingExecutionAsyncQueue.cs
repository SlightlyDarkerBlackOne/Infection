using System;
using System.Threading;
using UnityEngine;

internal class MessagingExecutionAsyncQueue : MessagingExecutionQueue
{
    private const int s_timerMinute = 60000;

    private Timer m_timeoutTimer;
    private int m_currentQueueId;

    internal MessagingExecutionAsyncQueue(MessageType _messageType, object _executingObject) : base(_messageType, _executingObject) { }

    protected override void ExecuteAction(MessageAction _action)
    {
        if (m_messageType != MessageType.Log && Application.isEditor)
        {
            MessagingSystem.PublishLog(LogAction.Publish, m_messageType, _action);
        }

        StartTimeoutTimer(_action.m_Config.m_TimeoutTime == 0 ? s_timerMinute : _action.m_Config.m_TimeoutTime);

        m_currentQueueId = m_publishingQueue.Count;
        int currentQueueIdVal = m_currentQueueId;
        _action.Invoke(m_executingObject, () => Acknowledge(currentQueueIdVal));
    }

    private void Acknowledge(int _queueId)
    {
        if (m_finished) return;

        if (_queueId != m_currentQueueId)
        {
            UnityEngine.Debug.LogError($"Trying to continue execution on already executing message queue of type {m_messageType}");
            return;
        }

        if (m_timeoutTimer != null)
        {
            m_timeoutTimer.Dispose();
        }

        ExecuteNext();
    }

    private void StartTimeoutTimer(int _time)
    {
        //throw exception from timer, stop queue execution on main thread
        Progress<Exception> progress = new Progress<Exception>((ex) => { throw ex; });

        m_timeoutTimer = new Timer(x => TimeoutEvent(progress), null, _time, Timeout.Infinite);
    }

    private void TimeoutEvent(IProgress<Exception> _progress)
    {
        //workaround to throw an exception from timer
        try
        {
            throw new Exception($"Timeout on queue for message type {m_messageType}");
        }
        catch (Exception e)
        {
            _progress.Report(e);
            CloseQueue();
            m_timeoutTimer.Dispose();
        }
    }

    ~MessagingExecutionAsyncQueue()
    {
        if (m_timeoutTimer != null)
        {
            m_timeoutTimer.Dispose();
        }
    }
}
