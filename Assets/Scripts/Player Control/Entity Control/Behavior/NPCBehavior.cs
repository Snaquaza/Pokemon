using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBehavior : MonoBehaviour {
	public abstract void Behavior();
	public abstract void onInteract();
}
