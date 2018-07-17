using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : EntityControl {

	public Direction direction;
    private int chance;
	private Vector2 moveVector;

	NPCBehavior behavior;

	private void Start()
	{
		behavior = GetComponent<NPCBehavior>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		behavior.Behavior();
	}
}
