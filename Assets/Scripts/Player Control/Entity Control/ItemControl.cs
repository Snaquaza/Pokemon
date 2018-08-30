using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : EntityControl {
    
	public Items item;

	// Use this for initialization
    public override void OnInteract(GameObject interacting)
	{
        gameObject.SetActive(false);
		movement.RemoveEntity();
		FindObjectOfType<InventoryHandler>().AddItem(item);
		Debug.Log("Obtained " + item.name);
	}
}
