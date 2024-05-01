using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MessagingBehaviour
{
	public Text nameText;
	public Text dialogueText;
	public Animator animator;

	private Queue<string> m_sentences;

	private void Awake()
	{
		Subscribe(MessageType.StartDialogue, OnTryStartDialogue);
		Subscribe(MessageType.PlayerDied, OnPlayerDied);
	}

	private void Start()
	{
		m_sentences = new Queue<string>();
	}

	private void OnTryStartDialogue(object _obj)
	{
		if (_obj is Dialogue dialogue)
		{
			StartDialogue(dialogue);
		}
	}

	private void OnPlayerDied(object _obj)
	{
		EndDialogue();
	}

	public void StartDialogue(Dialogue _dialogue)
	{
		MessagingSystem.Publish(MessageType.FreezePlayer, true);

		if (animator != null)
		{
			animator.SetBool("IsOpen", true);
		}

		nameText.text = _dialogue.name;

		m_sentences = new Queue<string>();

		if (m_sentences.Count != 0)
			m_sentences.Clear();

		foreach (string sentence in _dialogue.sentences)
		{
			m_sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (m_sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = m_sentences.Dequeue();

		//Stops animating last sentence if we start with the new one (continue button)
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	private IEnumerator TypeSentence(string _sentence)
	{
		dialogueText.text = "";
		foreach (char letter in _sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	private void EndDialogue()
	{
		if (animator != null)
		{
			animator.SetBool("IsOpen", false);
		}

		MessagingSystem.Publish(MessageType.FreezePlayer, false);
	}
}
