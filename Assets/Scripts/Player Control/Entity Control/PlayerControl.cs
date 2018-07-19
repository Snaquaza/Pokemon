using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityControl {
	
	Vector2 inputXY;
	bool inputInteract;

	// Update is called once per frame
	void Update () {
		inputXY = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		inputInteract = Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.C);
        
        if (Mathf.Abs(inputXY.x) > Mathf.Abs(inputXY.y))
            inputXY.y = 0;
        else
            inputXY.x = 0;
		
		if (inputXY != Vector2.zero)
		{
			movement.Move(inputXY, 
			              Input.GetKey(KeyCode.Space), 
			              Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift), 1);
		}
	}

    public override void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
