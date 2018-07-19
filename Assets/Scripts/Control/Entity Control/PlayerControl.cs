using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityControl {
	
	Vector2 inputXY;
	bool inputInteract;

	public bool isTalking;   
	public bool seen;

	// Update is called once per frame
	void Update () {
		inputXY = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		inputInteract = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(0);

		canMove = !(isTalking || seen);
        
        if (Mathf.Abs(inputXY.x) > Mathf.Abs(inputXY.y))
            inputXY.y = 0;
        else
            inputXY.x = 0;

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
