using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
   
    public Sprite northSprite, eastSprite, southSprite, westSprite;

	public void UpdateSprites(Direction direction)
	{
		switch (direction)
		{
            case Direction.North:
                gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
				break;
            case Direction.East:
                gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
				break;
            case Direction.South:
                gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
				break;
            case Direction.West:
                gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                break;            
		}
	}
}
