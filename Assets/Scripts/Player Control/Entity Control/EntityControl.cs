using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityControl : MonoBehaviour {
   
    protected Movement movement;

	// Use this for initialization
	void Start () {
        movement = GetComponent<Movement>();
        // Entities aren't on there before they move.
	}

	private void LateUpdate()
	{
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
	}

	public abstract void OnInteract();   
}
