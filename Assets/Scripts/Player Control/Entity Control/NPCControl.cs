using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : EntityControl {

    // Code "alert" - notice if running.
	// Code "bounds" while random walking
	public Direction direction;
    private int chance;
	private Vector2 moveVector;

    public bool isTrainer;
	public bool hasBattled;
    public int sight;

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
		throw new System.NotImplementedException();
	}

	// Update is called once per frame
	void FixedUpdate () {
		// Can glitch and appear near player
		// Walks before player animation is done (may be good)
        // Can walk through NPCs sometimes. Sometimes spots with no NPCs.
		if (isTrainer && !hasBattled)
		{
			if (!GetComponent<Movement>().DetectPlayer(sight))
			{
				behavior.Behavior();
			} else {
				hasBattled = true;
			}
		} else if (!isTrainer) {
			behavior.Behavior();
		}
		
	}
}
