using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public Grid grid;
	public float walkSpeed;

	private bool isMoving;
	public bool isAllowedToMove;

	private bool shift, running;

	private int locX, locY;
    private int startX, startY;
	private Direction currentDir;

	private EventHandler eventHandler;
	private AnimationHandler animationHandler;
    private SolidTiles tiles;
    private Barriers barriers;
	private Entities entities;

	// Use this for initialization
	void Start () {
		Debug.Log("Ahoy!");
		locX = (int)transform.position.x;
		locY = (int)(transform.position.y - 1);
		startX = locX;
		startY = locY;

		isAllowedToMove = true;

		eventHandler = GetComponent<EventHandler>();
		animationHandler = GetComponent<AnimationHandler>();
		tiles = grid.GetComponent<SolidTiles>();
		barriers = grid.GetComponent<Barriers>();
		entities = grid.GetComponent<Entities>();
	}

	private void Update()
	{
		UpdateEntities();
	}

	public void Move(Vector2 input, bool running, bool shift)
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

		if (!isMoving && isAllowedToMove)
		{         
            animationHandler.UpdateSprites(currentDir);

			startX = locX;
			startY = locY;

			this.running = running;
			this.shift = shift;

			// TODO: Find world coordinate      
			switch (currentDir)
			{
				case Direction.North:
					if (CanMove(locX, locY, locX, locY + 1, currentDir))
					{
						locY += 1;
						StartCoroutine(SmoothMove());
					}
					break;
				case Direction.East:
					if (CanMove(locX, locY, locX + 1, locY, currentDir))
					{
						locX += 1;
						StartCoroutine(SmoothMove());
					}
					break;
				case Direction.South:
					if (CanMove(locX, locY, locX, locY - 1, currentDir))
					{
						locY -= 1;
						StartCoroutine(SmoothMove());
					}
					break;
				case Direction.West:
					if (CanMove(locX, locY, locX - 1, locY, currentDir))
					{
						locX -= 1;
						StartCoroutine(SmoothMove());
					}
					break;
			}
			eventHandler.RunEvent(locX, locY);
		}
	}

	private bool CanMove(int currentX, int currentY, int targetX, int targetY, Direction direction)
    {
		Debug.Log("X: " + targetX + ", Y: " + targetY + " causes " + (entities.GetEntity(targetX, targetY) == true));
		return !(tiles.CanMove(targetX, targetY) || barriers.GetBarrier(currentX, currentY, direction) || entities.GetEntity(targetX, targetY) || shift);
    }

    // CanMoveLine

	private IEnumerator SmoothMove()
    {
        isMoving = true;
		Vector2 currentPos = PointToWorld(startX + startY * 32);
		Vector2 endPos = PointToWorld(locX + locY * 32);
        float t = 0;
        while (t < 1f)
        {
            // Not in the middle of the tile right now? (1/3 y, 1/2 x?)
            t += Time.deltaTime * walkSpeed;
			if (running) t *= 2;
			transform.position = Vector2.Lerp(currentPos, endPos, t);
            yield return null;
        }
		transform.position = PointToWorld(locX + locY * 32);
        isMoving = false;
        yield return 0;
    }

    // CALCULATIONS //

    private Vector2 PointToWorld(int point)
    {
		Vector2 result;      
        int x, y;
        x = point % 32;
        y = point / 32;
		result = new Vector2(x, y);

        return new Vector2(result.x + 0.5f + 32 * grid.GetComponent<SolidTiles>().locX, result.y + 1 + 32 * grid.GetComponent<SolidTiles>().locY);
    }

    // SPECIAL MOVEMENT //

	public void Warp(int x, int y)
    {
        locX = x;
        locY = y;
    }

    // UPDATING ENTITYCONTROL //

	public void UpdateEntities()
	{
		entities.UpdateEntities(null, startX, startY);
		entities.UpdateEntities(this.gameObject, locX, locY);
	}
}
