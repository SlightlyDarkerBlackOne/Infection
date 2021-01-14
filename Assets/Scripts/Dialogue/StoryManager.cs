using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour 
{ 
    public Text nameText;
    public Text dialogueText;

    public float letterSpeed = 0.03f;
    public float sentenceWaitTime = 1f;

    public AudioSource typing;
    private bool firstClick = false;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void SpeedUp()
    {
        letterSpeed = 0.001f;
        sentenceWaitTime = 0f;
    }
    public void ChangeClick()
    {
        firstClick = !firstClick;
        if (firstClick)
            SpeedUp();
        else if (!firstClick)
        {
            letterSpeed = 0.03f;
            sentenceWaitTime = 0.4f;
            DisplayNextSentence();
        }
    }

    //For starting a dialogue and also for starting quest dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences = new Queue<string>();
        if (sentences.Count != 0)
            sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        //Stops animating last sentence if we start with the new one (continue button)
        StopAllCoroutines();
        typing.Play();
        StartCoroutine(TypeSentence(sentence));

        /*StartCoroutine(WaitForNextSentence());
        if (sentences.Count != 0)
            DisplayNextSentence();*/
    }

    //Writes letter by letter in dialogue
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if(letter.Equals(".") || letter.CompareTo('.') == 0)
            {
                typing.Stop();
                yield return new WaitForSeconds(sentenceWaitTime);
                typing.Play();
            }
            yield return new WaitForSeconds(letterSpeed);
        }
        typing.Stop();
    }

    IEnumerator WaitForNextSentence(){
        yield return new WaitForSeconds(3f);
    }


        void EndDialogue()
    {
        typing.Stop();
        Loader.Load(Loader.Scene.PurpleLevel);
    }
}

