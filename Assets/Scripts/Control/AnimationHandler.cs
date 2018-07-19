using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
   
    public Sprite northSprite, eastSprite, southSprite, westSprite;
	public Animations currentAnimation;
    private float speed;
	private List<Sprite> sprites;

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

    public void DirectionToAnimation(Direction direction, float speed)
	{
		switch (direction)
		{
            case Direction.North:
                LoadAnimation(Animation.Up, speed);
				break;
            case Direction.East:
                LoadAnimation(Animation.Right, speed);
				break;
            case Direction.South:
                LoadAnimation(Animation.Down, speed);
				break;
            case Direction.West:
                LoadAnimation(Animation.Left, speed);
                break;
		}
	}

	public void LoadAnimation(Animation animation, float speed)
	{
		this.speed = speed;
		switch(animation)
		{
			case Animation.Up:
				currentAnimation = GetComponent<Animation_Up>();
				sprites = currentAnimation.Animation();
				speed *= currentAnimation.speed;
				StartCoroutine(Animate());
				break;
		}
		
	}

	// May want to do 1 frame per movement (somehow)
	private IEnumerator Animate()
	{
        float t = 0;
		while (t < 1f)
		{
			t += Time.deltaTime * speed;
			for (int i = 0; i < sprites.Count; i++)
			{
				if (t > ((1 / (float)sprites.Count) * i) && (t < ((1 / (float)sprites.Count) * (i + 1))))
				{
					gameObject.GetComponent<SpriteRenderer>().sprite = sprites[i];
                    yield return null;
				}
			}
            // Not in the middle of the tile right now? (1/3 y, 1/2 x?)
        }
		yield return 0;
	}
}
