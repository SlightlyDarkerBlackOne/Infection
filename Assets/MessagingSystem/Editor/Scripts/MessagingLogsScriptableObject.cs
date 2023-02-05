using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(fileName = "MessagingLogsScriptableObject", menuName = "ScriptableObjects/MessagingLogsScriptableObject")]
public class MessagingLogsScriptableObject : MessagingSO
{
    private static readonly int s_publishingsQueueLimit = 10;

    [SerializeField] private List<MessageSubscriptionsLog> m_subscriptions;
    [SerializeField] private List<SubscriptionLog> m_publishings;

    private static MessagingLogsScriptableObject m_instance;

    [MenuItem("MessageLogs/MessageLogs")]
    public static void CheckMessageLogs()
    {
        Selection.activeObject = m_instance;
    }

    [InitializeOnEnterPlayMode]
    public static void Initialize(EnterPlayModeOptions options)
    {
        if (m_instance == null)
        {
            m_instance = CreateInstance<MessagingLogsScriptableObject>();
        }

        m_instance.Subscribe(MessageType.Log, m_instance.ProcessLog);
    }

    public void ProcessLog(object _logMessage)
    {
        if (_logMessage is not LogMessage) return;

        LogMessage logMessage = (LogMessage)_logMessage;

        switch (logMessage.m_LogAction)
        {
            case (LogAction.Subscribe):
                LogSubscription(logMessage.m_MessageType, logMessage.m_Action);
                break;
            case (LogAction.Unsubscribe):
                LogUnsubscribe(logMessage.m_MessageType, logMessage.m_Action);
                break;
            case (LogAction.Publish):
                LogPublish(logMessage.m_MessageType, logMessage.m_Action);
                break;
            default: break;
        }
    }
    public void LogSubscription(MessageType _message, MessageAction _action)
    {
        if (m_subscriptions == null)
        {
            m_subscriptions = new List<MessageSubscriptionsLog>();
        }

        MessageSubscriptionsLog MessageSubscriptionsLog = m_subscriptions.Find((x) => x.m_MessageType == _message);

        if (MessageSubscriptionsLog == null)
        {
            MessageSubscriptionsLog = new MessageSubscriptionsLog()
            {
                m_MessageType = _message,
                m_Subscriptions = new List<SubscriptionLog>()
            };
            m_subscriptions.Add(MessageSubscriptionsLog);
        }

        MessageSubscriptionsLog.m_Subscriptions.Add(new SubscriptionLog(_message, _action));
    }

    public void LogUnsubscribe(MessageType _message, MessageAction _action)
    {
        if (m_subscriptions == null)
        {
            Debug.LogError($"Subscriptions logs empty");
            return;
        }

        MessageSubscriptionsLog MessageSubscriptionsLogObj = m_subscriptions.Find((x) => x.m_MessageType == _message);

        if (MessageSubscriptionsLogObj == null)
        {
            Debug.LogError($"Subscriptions logs empty for message {_message}");
            return;
        }

        MessageSubscriptionsLogObj.m_Subscriptions.RemoveAll((y) => y.Action == _action);

        if (MessageSubscriptionsLogObj.m_Subscriptions.Count == 0) m_subscriptions.RemoveAll((x) => x.m_MessageType == _message);
    }

    public void LogPublish(MessageType _message, MessageAction _action)
    {
        if (m_publishings == null)
        {
            m_publishings = new List<SubscriptionLog>();
        }
        m_publishings.Add(new SubscriptionLog(_message, _action));
        if (m_publishings.Count >= s_publishingsQueueLimit)
        {
            m_publishings.RemoveAt(0); //can't be actual queue because of serialization
        }
    }
}

[Serializable]
public class MessageSubscriptionsLog
{
    [HideInInspector] public MessageType m_MessageType;
    [SerializeField] public List<SubscriptionLog> m_Subscriptions;
}
#endif