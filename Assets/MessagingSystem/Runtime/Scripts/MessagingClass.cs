using System;
using System.Collections.Generic;
using UnityEngine;

public class MessagingClass
{
    private Dictionary<MessageType, List<MessageAction>> m_subscriptions = new Dictionary<MessageType, List<MessageAction>>();

    public void Subscribe(MessageType _messageType, Action<object> _action, MessagingSubscriptionConfig _config = default(MessagingSubscriptionConfig))
    {
        MessageAction msgAction = _action == null ? null : new MessageAction(_action, _config);
        Subscribe(_messageType, msgAction);
    }
    public void Subscribe(MessageType _messageType, Action<object, Action> _action, MessagingSubscriptionConfig _config = default(MessagingSubscriptionConfig))
    {
        MessageAction msgAction = _action == null ? null : new MessageAction(_action, _config);
        Subscribe(_messageType, msgAction);
    }

    private void Subscribe(MessageType _messageType, MessageAction _action)
    {
        if (_action == null)
        {
            Debug.LogWarning($"Action is null");
            return;
        }

        if (m_subscriptions.ContainsKey(_messageType) && (m_subscriptions[_messageType].FindAll((x) => x == _action)).Count != 0)
        {
            Debug.LogWarning($"Class already subscribed to message {_messageType} for action {_action.Name}");
            return;
        }

        if (m_subscriptions.ContainsKey(_messageType))
        {
            m_subscriptions[_messageType].Add(_action);
        }
        else
        {
            m_subscriptions.Add(_messageType, new List<MessageAction>() { _action });
        }

        MessagingSystem.Subscribe(_messageType, _action);
    }

    public void Unsubscribe(MessageType _messageType, Action<object> _action)
    {
        MessageAction msgAction = _action == null ? null : new MessageAction(_action);
        Unsubscribe(_messageType, msgAction);
    }
    public void Unsubscribe(MessageType _messageType, Action<object, Action> _action)
    {
        MessageAction msgAction = _action == null ? null : new MessageAction(_action);
        Unsubscribe(_messageType, msgAction);
    }

    private void Unsubscribe(MessageType _messageType, MessageAction _action)
    {
	    if (!m_subscriptions.ContainsKey(_messageType))
	    {
		    Debug.Log($"No actions subscribed to message type:{_messageType}");
		    return;
	    }
        if (m_subscriptions.ContainsKey(_messageType) && (m_subscriptions[_messageType].FindAll((x) => x == _action)).Count == 0)
        {
            Debug.LogWarning($"No messages of type {_messageType} for unsubscribing object");
            return;
        }

        m_subscriptions[_messageType].RemoveAll((x) => x == _action);
        if (m_subscriptions[_messageType].Count == 0)
        {
            m_subscriptions.Remove(_messageType);
        }

        MessagingSystem.Unsubscribe(_messageType, _action);
    }

    public void Cleanup()
    {
        foreach (KeyValuePair<MessageType, List<MessageAction>> pair in m_subscriptions)
        {
            foreach (MessageAction action in pair.Value)
            {
                MessagingSystem.Unsubscribe(pair.Key, action);
            }
        }
    }
    ~MessagingClass()
    {
        Cleanup();
    }
}
