using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public bool showOnce;
    private bool shownAllready = false;

    public Dialogue dialogue;

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    //When entering dialogue or NPC dialogue zone
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if(!shownAllready)
                TriggerDialogue();
            if (showOnce)
                shownAllready = true;
        }
    }
}
