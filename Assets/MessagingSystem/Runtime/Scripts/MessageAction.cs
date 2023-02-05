using System;

public class MessageAction
{
    internal MessageAction(Action<object> _action, MessagingSubscriptionConfig _config = default(MessagingSubscriptionConfig))
    {
        m_Action = _action;
        m_Config = _config;
    }
    internal MessageAction(Action<object, Action> _action, MessagingSubscriptionConfig _config = default(MessagingSubscriptionConfig))
    {
        m_AsyncAction = _action;
        m_Config = _config;
    }

    public object Target => m_Action != null ? m_Action.Target : m_AsyncAction.Target;
    public string Name => m_Action != null ? m_Action.Method.Name : m_AsyncAction.Method.Name;
    public bool IsAsync => m_Action == null;

    internal Action<object> m_Action = null;
    internal Action<object, Action> m_AsyncAction = null;
    internal MessagingSubscriptionConfig m_Config;

    internal virtual void Invoke(object _arg1) 
    { 
        m_Action?.Invoke(_arg1); 
    }
    internal virtual void Invoke(object _arg1, Action _arg2) 
    { 
        m_AsyncAction?.Invoke(_arg1, _arg2); 
    }

    public static bool operator == (MessageAction _lhs, MessageAction _rhs)
    {
        if (_rhs is null) return false;

        return _lhs.m_Action == _rhs.m_Action && _lhs.m_AsyncAction == _rhs.m_AsyncAction;
    }

    public static bool operator != (MessageAction _lhs, MessageAction _rhs) => !(_lhs == _rhs);

    public override bool Equals(object _other)
    {
        return _other is MessageAction && this == (MessageAction)_other;
    }

    public override int GetHashCode()
    {
        return m_Action != null ? m_Action.GetHashCode() : m_AsyncAction.GetHashCode();
    }
}
