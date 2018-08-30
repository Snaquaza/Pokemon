using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityControl {
	
	Vector2 inputXY;
	bool inputInteract;
	bool inputSelect;
	bool inputOpenMenu;
	bool menuUp, menuDown;

	public bool isTalking;   
	public bool isEvent; // Change to isEvent
	public bool isMenu;
	public bool isInventory;

	// Update is called once per frame
	void Update()
	{
		// Call events always?

		inputXY = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		inputSelect = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.C);
		inputInteract = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(0);

		// UI
		inputOpenMenu = Input.GetKeyDown(KeyCode.V);
		menuUp = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D);
		menuDown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A);

		canMove = !(isTalking || isEvent || isMenu || isInventory);

		// CONVERT MOVEMENT INPUT

		if (Mathf.Abs(inputXY.x) > Mathf.Abs(inputXY.y))
			inputXY.y = 0;
		else
			inputXY.x = 0;

		// OVERWORLD INPUT

		if (canMove)
		{
			if (inputInteract)
			{
				GameObject entity = movement.DetectEntity();
				if (entity)
				{
					entity.GetComponent<EntityControl>().OnInteract(gameObject);
					isTalking = true;
				}
			}
			else if (inputOpenMenu)
			{
				isMenu = true;
				FindObjectOfType<MenuUI>().OpenInventory(true);
			}
			else if (inputXY != Vector2.zero)
			{
				movement.Move(inputXY,
							  Input.GetKey(KeyCode.Space),
							  Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift), 1);
			}
		}

		// DIALOGUE INPUT

		else if (isTalking)
		{
			if (inputInteract)
			{
				isTalking = FindObjectOfType<Dialogue>().NextDialogue();
			}
			else if (Input.GetKeyDown(KeyCode.X))
			{
				FindObjectOfType<Dialogue>().EndDialogue();
			}
		}

		// MENU INPUT

		else if (isMenu)
		{
			if (Input.GetKeyDown(KeyCode.X) || inputOpenMenu)
			{
				FindObjectOfType<MenuUI>().OpenInventory(false);
				isMenu = false;
			}
			else if (inputSelect)
				FindObjectOfType<MenuUI>().clickButton();
			else if (menuUp)
				FindObjectOfType<MenuUI>().Up();
			else if (menuDown)
				FindObjectOfType<MenuUI>().Down();
		}

		// INVENTORY INPUT

		else if (isInventory)
		{
			if (Input.GetKeyDown(KeyCode.X) || inputOpenMenu)
			{
                isMenu = true;
				isInventory = false;
                FindObjectOfType<MenuUI>().OpenInventory(true);
				FindObjectOfType<InventoryHandler>().OpenInventory(false);
			}
		}
	}

	public override void OnInteract(GameObject interacting)
	{      
        movement.Face(interacting.GetComponent<Movement>().currentDirection());
    }
}