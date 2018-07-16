using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerMovement : MonoBehaviour
{
    public Grid grid;
    SolidTiles tiles;
	Barriers barriers;
	StaticEvents staticEvents;
	List<StaticEvent> currentEvents;
    
    private int locX, locY;
    private int pointInGrid;

    public float walkSpeed;
    public Sprite northSprite, eastSprite, southSprite, westSprite;

    bool isMoving = false;
	bool isRunning = false;
    bool isAllowedToMove = true;

    Direction currentDir;

    Vector2 input;

    private void Start()
    {
		pointInGrid = locX + locY * 32;
		tiles = grid.GetComponent<SolidTiles>();
		barriers = grid.GetComponent<Barriers>();
		staticEvents = grid.GetComponent<StaticEvents>();
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

		isRunning = false;
		if (Input.GetKey("space"))
			isRunning = true;

        switch (currentDir)
        {
            case Direction.North:
                gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
				if (CanMove(locX, locY, locX, locY + 1, currentDir))
                {
					locY += 1;
                    StartCoroutine(SmoothMove());
                }
                break;
            case Direction.East:
				gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                if (CanMove(locX, locY, locX + 1, locY, currentDir))
                {
					locX += 1;
                    StartCoroutine(SmoothMove());
                }
                break;
            case Direction.South:
				gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                if (CanMove(locX, locY, locX, locY - 1, currentDir))
                {
					locY -= 1;
                    StartCoroutine(SmoothMove());
                }
                break;
            case Direction.West:
				gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                if (CanMove(locX, locY, locX - 1, locY, currentDir))
                {
					locX -= 1;
                    StartCoroutine(SmoothMove());
                }
                break;
        }
		Debug.Log("I reached this.");
		RunEvent();
    }

	public void RunEvent()
	{
		currentEvents = staticEvents.GetEvent(locX, locY);
		for (int i = 0; i < currentEvents.Count; i++)
		{
			currentEvents[i].OnEvent(this.gameObject);
		}
	}
    
    public void Warp(int x, int y)
	{
		locX = x;
		locY = y;
		transform.position = gridToWorld(pointToVector(x + 32 * y));
	}

	private IEnumerator SmoothMove()
	{
        isMoving = true;
        Vector2 currentPos = gridToWorld(pointToVector(pointInGrid));
        Vector2 endPos = gridToWorld(pointToVector(PointInGrid()));
        float t = 0;
        while (t < 1f)
        {
            // Not in the middle of the tile right now? (1/3 y, 1/2 x?)
            t += Time.deltaTime * walkSpeed;
			if (isRunning) t *= 2;
            Vector2 lerpPos = Vector2.Lerp(currentPos, endPos, t);
			transform.position = lerpPos;
			yield return null;
		}
        isMoving = false;
		yield return 0;
    }

    private bool CanMove(int currentX, int currentY, int targetX, int targetY, Direction direction)
	{
		return !(tiles.CanMove(targetX, targetY) || barriers.GetBarrier(currentX, currentY, direction)) 
			&& !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
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
        return new Vector2(gridVector.x + 0.5f + 32 * grid.GetComponent<SolidTiles>().locX, gridVector.y + 1 + 32 * grid.GetComponent<SolidTiles>().locY);
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}