using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public int questNumber;

    public QuestManager theQM;

    public Dialogue startDialogue;
    public Dialogue endDialogue;

    public bool isItemQuest;
    public string targetItem;

    public bool isEnemyQuest;
    public string targetEnemy;
    public int enemiesToKill;
    public int enemyKillCount;

    // Start is called before the first frame update
    void Start()
    {
        endDialogue.name = startDialogue.name;
    }

    private void Update() {
        if (isItemQuest) {
            if(theQM.itemCollected == targetItem) {
                theQM.itemCollected = null;

                EndQuest();
            }
        }

        if (isEnemyQuest) {
            if(theQM.enemyKilled == targetEnemy) {
                theQM.enemyKilled = null;

                enemyKillCount++;
            }

            if(enemyKillCount >= enemiesToKill) {
                EndQuest();
            }
        }
    }

    public void StartQuest() {
        theQM.ShowQuestText(startDialogue);
    }

    public void EndQuest() {
        theQM.ShowQuestText(endDialogue);
        theQM.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
