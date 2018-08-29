using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    // Make Dialogue system work with animation, not enabling.

	public Canvas dialogueCanvas;
	public float textSpeed;
	public Text nameText;
	public Text dialogueText;
	public Image avatar;

	PlayerControl player;

	private Queue<string> dialogue;

	// Use this for initialization
	void Start () {
		dialogueCanvas.enabled = false;
		dialogue = new Queue<string>();
		player = FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>();
	}

	public void StartDialogue(TextHolder text)
	{
		player.isTalking = true;
		dialogue.Clear();
		dialogueCanvas.enabled = true;
		nameText.text = text.name;
		avatar.sprite = text.sprite;
        foreach (string sentence in text.text)
		{
			dialogue.Enqueue(sentence);
		}
		NextDialogue();
	}
    
    public bool NextDialogue()
	{
		if (dialogue.Count == 0) { EndDialogue(); return false; }
		string currentSentence = dialogue.Dequeue();
		dialogueText.text = currentSentence;
		StopAllCoroutines();
		StartCoroutine(TypeSentence(currentSentence));
		Debug.Log(currentSentence);
		return true;
	}

	IEnumerator TypeSentence(string sentence)
	{      
		dialogueText.text = "";
		foreach (char letter in sentence)
		{
			yield return new WaitForSeconds(1 / textSpeed);
			dialogueText.text += letter;
			yield return null;
		}
	}
    
    public void EndDialogue()
	{
		dialogue.Clear();
        dialogueCanvas.enabled = false;
		player.isTalking = false;
	}
}