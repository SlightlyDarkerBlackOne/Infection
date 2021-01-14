using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField]
    private StoryManager storyManager;

    public bool story;

    public Dialogue dialogue;


    // Start is called before the first frame update
    void Start()
    {
        TriggerStory();
    }
    public void TriggerStory()
    {
        storyManager.StartDialogue(dialogue);
    }
}
