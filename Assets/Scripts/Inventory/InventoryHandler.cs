using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour {

	public Canvas inventoryCanvas;

	// Use this for initialization
	void Start () {
		inventoryCanvas.enabled = false;
	}

    public void OpenInventory(bool state)   
	{
		inventoryCanvas.enabled = state;
	}
}
