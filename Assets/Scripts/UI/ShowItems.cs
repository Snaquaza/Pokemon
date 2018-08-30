using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItems : MonoBehaviour {
	public List<Text> text;
	List<Items> items;
    
	// Call only when bag opened
	public void OnOpenBag()
	{
		items = new List<Items>();
		items = FindObjectOfType<InventoryHandler>().GetItems();
		for (int i = 0; i < items.Count; i++)
		{
			text[i].text = items[i].name;
		}
	}
}
