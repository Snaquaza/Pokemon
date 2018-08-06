using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehavior : NPCBehavior {
    
    public bool running, turning;

    public override void Behavior()
    {
		// Add minimum waiting time
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
            gameObject.GetComponent<Movement>().Move(moveVector, running, turning, 1);
        }
    }
}
