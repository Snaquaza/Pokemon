using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : EntityControl {

	// Code "alert" - notice if running.
	// Code "bounds" while random walking

    public bool isTrainer;
	public bool hasBattled;
    public int sight;

	public TextHolder text;

	NPCBehavior behavior;

	private void Start()
	{
		behavior = GetComponent<NPCBehavior>();
		if (!isTrainer)
			sight = 0;
		else
			hasBattled = false;
	}

	public override void OnInteract()
	{
		FindObjectOfType<Dialogue>().StartDialogue(text);
	}

	// Update is called once per frame
	void FixedUpdate () {
		// Can glitch and appear near player
		// Walks before player animation is done (may be good)
        // Can walk through NPCs sometimes. Sometimes spots with no NPCs.
		if (isTrainer && !hasBattled && canMove)
		{
			if (!GetComponent<Movement>().DetectPlayer(sight))
			{
				behavior.Behavior();
			} else {
				canMove = false;
				hasBattled = true;
			}
		} else if (!isTrainer && canMove) {
			behavior.Behavior();
		}
		
	}
}
