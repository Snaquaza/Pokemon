using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityControl {
	
	Vector2 inputXY;
	bool inputInteract;
	bool inputOpenInventory;
	bool menuUp, menuDown;

	public bool isTalking;   
	public bool seen; // Change to isEvent
	public bool isInventory;

	// Update is called once per frame
	void Update () {
		inputXY = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		inputInteract = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(0);

        // UI
		inputOpenInventory = Input.GetKeyDown(KeyCode.V);
		menuUp = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D);
        menuDown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A);

		canMove = !(isTalking || seen || isInventory);
        
        // CONVERT MOVEMENT INPUT

        if (Mathf.Abs(inputXY.x) > Mathf.Abs(inputXY.y))
            inputXY.y = 0;
        else
            inputXY.x = 0;

        // DIALOGUE INPUT

		if (inputInteract && isTalking)
		{
			isTalking = FindObjectOfType<Dialogue>().NextDialogue();
		}
        
		else if (Input.GetKeyDown(KeyCode.X) && isTalking)
		{
			FindObjectOfType<Dialogue>().EndDialogue();
		}
        
		else if (inputInteract)
		{
			GameObject entity = movement.DetectEntity();
			if (entity)
			{
				entity.GetComponent<EntityControl>().OnInteract(gameObject);
				isTalking = true;
			}
		}
        
		// INVENTORY INPUT

        else if (isInventory && inputInteract)
        {
            FindObjectOfType<InventoryUI>().clickButton();
        }

		else if ((Input.GetKeyDown(KeyCode.X) || inputOpenInventory) && isInventory)
        {
            FindObjectOfType<InventoryUI>().OpenInventory(false);
			isInventory = false;
        }
        
		else if (isInventory && (menuUp || menuDown))
		{
			if (menuUp)
                FindObjectOfType<InventoryUI>().Up();
			else 
                FindObjectOfType<InventoryUI>().Down();
		}      
        
		else if (inputOpenInventory)
		{
			isInventory = true;
            FindObjectOfType<InventoryUI>().OpenInventory(true);
		}

        // MOVEMENT INPUT

		else if (inputXY != Vector2.zero && canMove)
		{
			movement.Move(inputXY, 
			              Input.GetKey(KeyCode.Space), 
			              Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift), 1);
		}
	}

	public override void OnInteract(GameObject interacting)
	{      
        movement.Face(interacting.GetComponent<Movement>().currentDirection());
    }
}
