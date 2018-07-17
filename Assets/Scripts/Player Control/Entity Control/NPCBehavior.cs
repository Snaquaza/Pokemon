using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour {
	
	public static void RandomBehavior(GameObject gameObject, Behavior behavior)
	{
		Vector2 moveVector = Vector2.zero;
        int chance = Random.Range(0, 100);
        if (chance == 0)
        {
            Direction direction = (Direction)Random.Range(0, 4);
            switch (direction)
            {
                case Direction.North:
                    moveVector = Vector2.up;
                    break;
                case Direction.East:
                    moveVector = Vector2.right;
                    break;
                case Direction.South:
                    moveVector = Vector2.down;
                    break;
                case Direction.West:
                    moveVector = Vector2.left;
                    break;
            }
			if (behavior == Behavior.RandomTurning)
			    gameObject.GetComponent<Movement>().Move(moveVector, false, true);
			else if (behavior == Behavior.RandomMovement)
                gameObject.GetComponent<Movement>().Move(moveVector, false, false);
        }
	}
}
