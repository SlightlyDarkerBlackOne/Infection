using UnityEngine;

internal class MessagingExecutionSyncQueue : MessagingExecutionQueue
{
    internal MessagingExecutionSyncQueue(MessageType _messageType, object _executingObject) : base(_messageType, _executingObject)
    {
        m_messageType = _messageType;
        m_executingObject = _executingObject;
    }

    protected override void ExecuteAction(MessageAction _action)
    {
        _action.Invoke(m_executingObject);

        if (m_messageType != MessageType.Log && Application.isEditor)
        {
            MessagingSystem.PublishLog(LogAction.Publish, m_messageType, _action);
        }

        ExecuteNext();
    }
}
