using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityControl {
	
	Vector2 inputXY;
	bool inputInteract;
	bool inputOpenInventory;

	public bool isTalking;   
	public bool seen; // Change to isEvent
	public bool isInventory;

	// Update is called once per frame
	void Update () {
		inputXY = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		inputInteract = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(0);
		inputOpenInventory = Input.GetKeyDown(KeyCode.V);

		canMove = !(isTalking || seen || isInventory);
        
        // Convert input

        if (Mathf.Abs(inputXY.x) > Mathf.Abs(inputXY.y))
            inputXY.y = 0;
        else
            inputXY.x = 0;

        // Check input for specific situations

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
        
		else if (inputOpenInventory)
		{
			isInventory = true;
            FindObjectOfType<InventoryHandler>().OpenInventory(true);
		}
         
        else if (Input.GetKeyDown(KeyCode.X) && isInventory)
		{
            FindObjectOfType<InventoryHandler>().OpenInventory(false);
        }

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
