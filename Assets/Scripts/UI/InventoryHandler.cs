using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour {
	public Canvas inventoryCanvas;
	private List<Items> items;

	private void Start()
	{
		items = new List<Items>();
		inventoryCanvas.enabled = false;
	}
    
    public void OpenInventory(bool state)
	{
        inventoryCanvas.enabled = state;
	}   

    public void AddItem(Items item)
	{
		items.Add(item);
	}

	public List<Items> GetItems()
	{
		return items;
	}
}
