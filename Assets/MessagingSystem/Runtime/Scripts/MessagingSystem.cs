using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessagingSystem
{
	private static Dictionary<MessageType, List<MessageAction>> m_subscriptions =
		new Dictionary<MessageType, List<MessageAction>>();

	private static List<MessagingExecutionQueue> m_executingQueues = new List<MessagingExecutionQueue>();

	/// <summary>
	/// Subscribe to select MessageType, subscribed entity performs action on publish (processing published object) for select MessageType. 
	/// </summary>
	public static void Subscribe(MessageType _messageType, MessageAction _action)
	{
		if (_action == null)
		{
			UnityEngine.Debug.LogWarning($"Action is null");
			return;
		}

		if (CheckIsActionSubscribed(_messageType, _action))
		{
			string name = _action.m_Action != null ? _action.m_Action.Method.Name : _action.m_AsyncAction.Method.Name;
			UnityEngine.Debug.LogWarning($"Action {name} already subscribed to {_messageType}");
			return;
		}

		if (m_subscriptions.ContainsKey(_messageType))
		{
			if (_action.m_Config.m_IsPriority)
			{
				m_subscriptions[_messageType].Insert(0, _action);
			}
			else
			{
				m_subscriptions[_messageType].Add(_action);
			}
		}
		else
		{
			m_subscriptions.Add(_messageType, new List<MessageAction>() { _action });
		}

		if (_messageType != MessageType.Log && Application.isEditor)
		{
			PublishLog(LogAction.Subscribe, _messageType, _action);
		}
	}

	/// <summary>
	/// Unsubscribe chosen action from select MessageType
	/// </summary>
	internal static void Unsubscribe(MessageType _messageType, MessageAction _action)
	{
		if (!m_subscriptions.ContainsKey(_messageType))
		{
			Debug.Log($"No actions subscribed to message type:{_messageType}");
			return;
		}

		if (!CheckIsActionSubscribed(_messageType, _action))
		{
			//Firing often due to scene switching with loadingController
			//UnityEngine.Debug.LogError($"No messages of type {_messageType} for unsubscribing object");
			return;
		}

		m_subscriptions[_messageType].RemoveAll((x) => x == _action);
		if (m_subscriptions[_messageType].Count == 0)
		{
			m_subscriptions.Remove(_messageType);
		}

		if (_messageType != MessageType.Log && Application.isEditor)
		{
			PublishLog(LogAction.Unsubscribe, _messageType, _action);
		}
	}

	/// <summary>
	/// Publish message of selected type with object optionally attached. Processing of the attached object is determined by subscribed objects.
	/// </summary>
	public static void Publish(MessageType _messageType, object _message = null)
	{
		if (m_subscriptions.ContainsKey(_messageType))
		{
			bool doStop = m_subscriptions[_messageType].Exists((x) => x.m_Config.m_DoStopAll == true);
			if (!doStop)
			{
				Publish(_messageType, _message, false); //publish sync then async
				Publish(_messageType, _message, true);
			}
		}
		else
		{
			Debug.LogWarning($"Published but no messages of type {_messageType}");
			return;
		}
	}

	private static void Publish(MessageType _messageType, object _message, bool isAsync)
	{
		if (!m_subscriptions.ContainsKey(_messageType))
		{
			Debug.Log($"No actions subscribed to message type:{_messageType}");
			return;
		}
		List<MessageAction> actions = m_subscriptions[_messageType].FindAll((x) => x.IsAsync == isAsync);

		if (actions.Count == 0)
		{
			return;
		}

		MessagingExecutionQueue executionQueue =
			m_executingQueues.Find((x) => x.m_MessageType == _messageType && x.m_isAsync == isAsync);

		if (executionQueue == null)
		{
			if (isAsync)
			{
				executionQueue = new MessagingExecutionAsyncQueue(_messageType, _message);
			}
			else
			{
				executionQueue = new MessagingExecutionSyncQueue(_messageType, _message);
			}
		}

		foreach (MessageAction subscription in actions)
		{
			executionQueue.AddToQueue(subscription);
		}

		executionQueue.StartExecute();

		if (!executionQueue.m_Finished)
		{
			m_executingQueues.Add(executionQueue);
		}
	}

	internal static void ClearExecutedQueues()
	{
		m_executingQueues.RemoveAll((x) => x.m_Finished);
	}

	private static bool CheckIsActionSubscribed(MessageType _messageType, MessageAction _action)
	{
		return m_subscriptions.ContainsKey(_messageType) && (m_subscriptions[_messageType].Exists((x) => x == _action));
	}

	public static void PublishLog(LogAction _logAction, MessageType _messageType, MessageAction _action)
	{
		LogMessage logMessage =
			new LogMessage() { m_LogAction = _logAction, m_MessageType = _messageType, m_Action = _action };

		Publish(MessageType.Log, logMessage);
	}
}

public class LogMessage
{
	public LogAction m_LogAction;
	public MessageType m_MessageType;
	public MessageAction m_Action;
}

public enum LogAction
{
	Subscribe,
	Unsubscribe,
	Publish
}