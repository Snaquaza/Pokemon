using System.Collections;
using UnityEngine;

class PlayerMovement : MonoBehaviour
{
	public Grid grid;
    private int locX, locY;
	private int pointInGrid;

	public float walkSpeed;
	public Sprite northSprite, eastSprite, southSprite, westSprite;

	bool isMoving = false;
	bool isAllowedToMove = true;

	Direction currentDir;

	Vector2 input;
    
	private void Start()
	{
		pointInGrid = locX + locY * 32;
	}

	private void Update()
	{
		if (!isMoving && isAllowedToMove)
		{
			input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
				input.y = 0;
			else
				input.x = 0;

			if (input != Vector2.zero)
			{
				if (input.x < 0)
				{
					currentDir = Direction.West;
					locX -= 1;
				}
				if (input.x > 0)
				{
					currentDir = Direction.East;
					locX += 1;
				}
				if (input.y < 0)
				{
					currentDir = Direction.South;
					locY -= 1;
				}
				if (input.y > 0)
				{
					currentDir = Direction.North;
					locY += 1;
				}

				switch (currentDir)
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

				StartCoroutine(Move());
			}
		}
	}

	public IEnumerator Move()
	{
		// TODO: Find world coordinate
		isMoving = true;      
		float t = 0;

		while (t < 1f)
		{
			// Not in the middle of the tile right now? (1/3 y, 1/2 x?)
			t += Time.deltaTime * walkSpeed;
			transform.position = Vector2.Lerp(gridToWorld(pointToVector(pointInGrid)), gridToWorld(pointToVector(PointInGrid())), t);
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}

    private int PointInGrid()
    {
        pointInGrid = locX + locY * 32;
        return pointInGrid;
    }

	private Vector2 pointToVector(int point)
	{
		int x, y;
		x = point % 32;
		y = point / 32;
		return new Vector2(x, y);
	}
    
	private Vector2 gridToWorld(Vector2 gridVector)
	{
		Debug.Log(locX + " and " + locY);
		return new Vector2(gridVector.x + 32 * grid.GetComponent<SolidTiles>().locX, gridVector.y + 32 * grid.GetComponent<SolidTiles>().locY);
	}
}

enum Direction
{
	North,
	East,
	South,
	West
}