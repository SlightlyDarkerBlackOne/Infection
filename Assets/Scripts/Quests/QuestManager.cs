using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestObject[] quests;
    public bool[] questCompleted;

    private DialogueManager theDM;

    [HideInInspector]
    public string itemCollected;

    [HideInInspector]
    public string enemyKilled;

    private void Start() {
        questCompleted = new bool[quests.Length];
        theDM = FindObjectOfType<DialogueManager>();
    }

    public void ShowQuestText(Dialogue questDialogue) {
        theDM.StartDialogue(questDialogue);
    }

    /*public void TriggerQuestDialogue(Dialogue questDialogue) {
        theDM.StartDialogue(questDialogue);
    }*/
}
