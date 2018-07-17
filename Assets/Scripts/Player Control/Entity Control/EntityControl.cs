using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityControl : MonoBehaviour {
   
    protected Movement movement;

	// Use this for initialization
	void Start () {
        movement = GetComponent<Movement>();
	}
}
