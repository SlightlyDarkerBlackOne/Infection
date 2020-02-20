using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestObject[] quests;
    public bool[] questCompleted;

    private DialogueManager theDM;

    public string itemCollected;

    public string enemyKilled;

    private void Start() {
        questCompleted = new bool[quests.Length];
        theDM = FindObjectOfType<DialogueManager>();
    }

    public void ShowQuestText(Dialogue questDialogue) {
        TriggerQuestDialogue(questDialogue);
    }

    public void TriggerQuestDialogue(Dialogue questDialogue) {
        theDM.StartDialogue(questDialogue);
    }
}
