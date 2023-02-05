using System;
using UnityEngine;

public class MessagingBehaviour : MonoBehaviour
{
    private MessagingClass m_messagingClass;

    private MessagingClass m_MessagingClass
    {
        get
        {
            if (m_messagingClass == null)
            {
                m_messagingClass = new MessagingClass();
            }
            return m_messagingClass;
        }
    }

    protected void Subscribe(MessageType _messageType, Action<object> _action, MessagingSubscriptionConfig _config = default(MessagingSubscriptionConfig))
    {
        m_MessagingClass.Subscribe(_messageType, _action, _config);
    }
    protected void Subscribe(MessageType _messageType, Action<object, Action> _action, MessagingSubscriptionConfig _config = default(MessagingSubscriptionConfig))
    {
        m_MessagingClass.Subscribe(_messageType, _action, _config);
    }

    protected void Unsubscribe(MessageType _messageType, Action<object> _action)
    {
        m_MessagingClass.Unsubscribe(_messageType, _action);
    }
    protected void Unsubscribe(MessageType _messageType, Action<object, Action> _action)
    {
        m_MessagingClass.Unsubscribe(_messageType, _action);
    }

    private void OnDestroy()
    {
        if (m_messagingClass != null)
        {
            m_messagingClass.Cleanup();
        }
    }
}
