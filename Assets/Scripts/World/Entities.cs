using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour {

	private GameObject[] entities;

	// Use this for initialization
	void OnEnable () {
		entities = new GameObject[32 * 32];
	}

	public void UpdateEntities(GameObject gameObject, int x, int y)
	{
		entities[x + y * 32] = gameObject;
	}

	public GameObject GetEntity(int x, int y)
	{
        if (x < 0 || y < 0)
            return null; 
		return entities[x + y * 32];
	}
}