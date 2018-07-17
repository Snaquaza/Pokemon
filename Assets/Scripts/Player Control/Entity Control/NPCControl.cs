using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : EntityControl {

	public Direction direction;
    private int chance;
	private Vector2 moveVector;

	public Behavior behavior;

	private void Start()
	{
		
	}

	// Update is called once per frame
	void Update () {
		switch (behavior)
		{
			case Behavior.RandomTurning:
			case Behavior.RandomMovement:
				NPCBehavior.RandomBehavior(gameObject, behavior);
				break;
		}
	}
}
