using System.Collections;
using UnityEngine;

class PlayerMovement : MonoBehaviour
{
    public Grid grid;
    SolidTiles tiles;
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
        tiles = grid.GetComponent<SolidTiles>();
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
                }
                if (input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if (input.y < 0)
                {
                    currentDir = Direction.South;
                }
                if (input.y > 0)
                {
                    currentDir = Direction.North;
                }

                Move();
            }
        }
    }

    public void Move()
    {
        // TODO: Find world coordinate
        Debug.Log(currentDir);

        switch (currentDir)
        {
            case Direction.North:
                gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                if (!tiles.CanMove(locX, locY + 1))
                {
					locY += 1;
                    StartCoroutine(SmoothMove());
                }
                break;
            case Direction.East:
                gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                if (!tiles.CanMove(locX + 1, locY))
                {
					locX += 1;
                    StartCoroutine(SmoothMove());
                }
                break;
            case Direction.South:
                gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                if (!tiles.CanMove(locX, locY - 1))
                {
					locY -= 1;
                    StartCoroutine(SmoothMove());
                }
                break;
            case Direction.West:
                gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                if (!tiles.CanMove(locX - 1, locY))
                {
					locX -= 1;
                    StartCoroutine(SmoothMove());
                }
                break;
        }
    }

	private IEnumerator SmoothMove()
	{
        isMoving = true;
        Vector2 currentPos = gridToWorld(pointToVector(pointInGrid));
        Vector2 endPos = gridToWorld(pointToVector(PointInGrid()));
        float t = 0;
        Debug.Log(locX + " and " + locY);
        while (t < 1f)
        {
            // Not in the middle of the tile right now? (1/3 y, 1/2 x?)
            t += Time.deltaTime * walkSpeed;
            Vector2 lerpPos = Vector2.Lerp(currentPos, endPos, t);
			transform.position = lerpPos;
			Debug.Log(t + ", " + transform.position);
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
        return new Vector2(gridVector.x + 0.5f + 32 * grid.GetComponent<SolidTiles>().locX, gridVector.y + 1 + 32 * grid.GetComponent<SolidTiles>().locY);
    }
}

enum Direction
{
    North,
    East,
    South,
    West
}