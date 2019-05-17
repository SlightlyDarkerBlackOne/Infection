using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public int questNumber;

    private QuestManager theQM;

    public string itemName;

    // Start is called before the first frame update
    void Start()
    {
        theQM = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            if(!theQM.questCompleted[questNumber] && theQM.quests[questNumber].gameObject.activeSelf) {
                theQM.itemCollected = itemName;
                gameObject.SetActive(false);
                
            }
        }
    }
}
