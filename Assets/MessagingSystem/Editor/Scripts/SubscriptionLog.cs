using System;
using UnityEngine;

[Serializable]
public class SubscriptionLog
{
    [HideInInspector] private MessageAction m_action; //keeping references to distinguish by for removal
    [SerializeField] private bool m_isAsync;
    [SerializeField] private string m_function;
    [SerializeField] private string m_messageType;
    [SerializeField] private string m_time;

    [HideInInspector] public MessageAction Action => m_action;

    public SubscriptionLog(MessageType _message, MessageAction _action)
    {
        m_isAsync = _action.IsAsync;
        m_action = _action;
        m_function = $"{_action.Target} : {_action.Name}"; ;
        m_messageType = _message.ToString();
        m_time = DateTime.Now.ToString();
    }
}
