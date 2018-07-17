using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : EntityControl {

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

	// Update is called once per frame
	void FixedUpdate () {
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
