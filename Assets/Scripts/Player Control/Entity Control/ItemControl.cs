using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : EntityControl {
    
	public Items item;
	private string article;
	private TextHolder text;
    
	private void Start()
	{
        movement = GetComponent<Movement>();
		text = new TextHolder();
		text.text = new string[1];
		text.name = item.name;
		article = "a ";
		if ("aeiouAEIOU".IndexOf(text.name[0]) >= 0)
			article = "an ";
		text.text[0] = "You obtained " + article + text.name;
		text.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	}

	// Use this for initialization
	public override void OnInteract(GameObject interacting)
	{
		FindObjectOfType<InventoryHandler>().AddItem(item);
		Debug.Log("Obtained " + item.name);
		FindObjectOfType<Dialogue>().StartDialogue(text);
		Debug.Log(movement);
		movement.RemoveEntity();     
        gameObject.SetActive(false);    
	}
}
