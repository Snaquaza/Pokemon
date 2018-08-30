using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : StaticEvent
{
	private GameObject interacting;
    public override void OnEvent(GameObject gameObject)
    {
        gameObject.GetComponent<EntityControl>().OnInteract(interacting);
        interacting.GetComponent<EntityControl>().OnInteract(gameObject);
    }
	public Interact(GameObject interacting)
	{
		this.interacting = interacting;
	}
}
