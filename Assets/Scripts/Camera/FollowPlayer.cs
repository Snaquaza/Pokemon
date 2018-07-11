using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject target;

	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
	}
}
