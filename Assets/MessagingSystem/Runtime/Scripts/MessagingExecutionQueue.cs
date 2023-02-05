using System.Collections.Generic;

public class MessagingExecutionQueue
{
    protected MessageType m_messageType;
    protected object m_executingObject;
    protected Queue<MessageAction> m_publishingQueue;

    protected bool m_finished = false;

    private bool m_started = false;

    internal bool m_isAsync => this is MessagingExecutionAsyncQueue;
    internal bool m_Finished => m_finished;
    internal MessageType m_MessageType => m_messageType;
    protected MessagingExecutionQueue(MessageType _messageType, object _executingObject)
    {
        m_messageType = _messageType;
        m_executingObject = _executingObject;
    }

    internal void StartExecute()
    {
        if (!m_started)
        {
            ExecuteNext();
            m_started = true;
        }
    }

    internal void AddToQueue(MessageAction _action)
    {
        if (m_publishingQueue == null)
        {
            m_publishingQueue = new Queue<MessageAction>();
        }

        m_publishingQueue.Enqueue(_action);
    }

    protected void CloseQueue()
    {
        m_finished = true;

        MessagingSystem.ClearExecutedQueues();
    }

    protected void ExecuteNext()
    {
        if (m_publishingQueue.Count == 0)
        {
            CloseQueue();
            return;
        }

        MessageAction nextAction = m_publishingQueue.Dequeue();

        ExecuteAction(nextAction);
    }

    protected virtual void ExecuteAction(MessageAction _action) { }
}
