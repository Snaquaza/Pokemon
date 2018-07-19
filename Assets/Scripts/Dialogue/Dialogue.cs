using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    // Make Dialogue system work with animation, not enabling.

	public Canvas dialogueCanvas;
	public Text nameText;
	public Text dialogueText;

	private Queue<string> dialogue;

	// Use this for initialization
	void Start () {
		dialogueCanvas.enabled = false;
		dialogue = new Queue<string>();
	}

	public void StartDialogue(TextHolder text)
	{
		dialogue.Clear();
		dialogueCanvas.enabled = true;
		nameText.text = text.name;
        foreach (string sentence in text.text)
		{
			dialogue.Enqueue(sentence);
		}
		NextDialogue();
	}
    
    public bool NextDialogue()
	{
		if (dialogue.Count == 0) { EndDialogue(); return false; }
        // Possibly slowly write text.
		string currentSentence = dialogue.Dequeue();
		dialogueText.text = currentSentence;
		return true;
	}
    
    public void EndDialogue()
	{
		dialogue.Clear();
        dialogueCanvas.enabled = false;
	}
}
