using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public QuestObject[] quests;
	public bool[] questCompleted;

	[HideInInspector]
	public string itemCollected;

	[HideInInspector]
	public string enemyKilled = "";

	private void Start()
	{
		questCompleted = new bool[quests.Length];
	}

	public void ShowQuestText(Dialogue questDialogue)
	{
		MessagingSystem.Publish(MessageType.StartDialogue, questDialogue);
	}

	public void EndQuest(int _questNumber)
	{
		MessagingSystem.Publish(MessageType.QuestFinished, _questNumber);
	}
}
