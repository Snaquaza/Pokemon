using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items {
	public string name;
    public Items(string name)
	{
		this.name = name;
	}
}

public enum itemCategory {
	Berry, // Bag Cat 1
    Medicine_Points, // Bag Cat 2
    Medicine_Status,
	Technical_Machine, // Bag Cat 3
    Hidden_Machine,
    Key_Item, // Bag Cat 4
    Other, // Bag Cat 5
}