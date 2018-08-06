﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    // Should movement still be split into a player and a NPC?

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
      
        UpdateEntities();
	}

	public void Move(Vector2 input, bool running, bool shift, int length)
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
			if (CanMove(locX, locY, currentDir, length))
			{
				switch (currentDir)
				{
					case Direction.North:
						locY += length;
						break;
					case Direction.East:
						locX += length;
						break;
					case Direction.South:
						locY -= length;
						break;
					case Direction.West:
						locX -= length;
						break;
				}
                entities.UpdateEntities(this.gameObject, locX, locY);
                StartCoroutine(SmoothMove(length));
                eventHandler.RunEvent(locX, locY);
			}
		}
	}

	private bool CanMove(int currentX, int currentY, Direction direction, int length)
    {
		bool result = true;
		for (int i = 1; i <= length; i++)
		{
			switch (direction)
			{
                case Direction.North:
                    if (tiles.CanMove(currentX, currentY + i) ||
                        barriers.GetBarrier(currentX, currentY, direction) ||
                        entities.GetEntity(currentX, currentY + i) ||
                        shift)
                        result = false;
					break;
                case Direction.East:
                    if (tiles.CanMove(currentX + i, currentY) ||
                        barriers.GetBarrier(currentX, currentY, direction) ||
                        entities.GetEntity(currentX + i, currentY) ||
                        shift)
                        result = false;
					break;
				case Direction.South:
                    if (tiles.CanMove(currentX, currentY - i) ||
                        barriers.GetBarrier(currentX, currentY, direction) ||
                        entities.GetEntity(currentX, currentY - i) ||
                        shift)
                        result = false;
					break;
                case Direction.West:
                    if (tiles.CanMove(currentX - i, currentY) ||
                        barriers.GetBarrier(currentX, currentY, direction) ||
                        entities.GetEntity(currentX - i, currentY) ||
                        shift)
                        result = false;
                    break;
			}
		}

		return result;
    }

    // Add different movement types?
	private IEnumerator SmoothMove(int length)
    {
        isMoving = true;
		Vector2 currentPos = PointToWorld(startX + startY * 32);
		Vector2 endPos = PointToWorld(locX + locY * 32);
		float t = 0;
        if (running) animationHandler.DirectionToAnimation(currentDir, walkSpeed * 2);
		else animationHandler.DirectionToAnimation(currentDir, walkSpeed);
        while (t < 1f)
        {
            // Not in the middle of the tile right now? (1/3 y, 1/2 x?)
			t += (Time.deltaTime * walkSpeed) / length;
			if (running) t *= 2;
			transform.position = Vector2.Lerp(currentPos, endPos, t);
            yield return null;
        }
		transform.position = PointToWorld(locX + locY * 32);
		isMoving = false;
        UpdateEntities();
        yield return 0;
    }

    private IEnumerator QueueMove(Vector2 target, int i)
	{
		while (isMoving)
			yield return null;
		Move(target, false, false, i);
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

    private bool IsBetween(Vector2 comparison, Vector2 edge1, Vector2 edge2)
	{
		return (((comparison.x >= edge1.x && comparison.x <= edge2.x)
             || (comparison.x <= edge1.x && comparison.x >= edge2.x))
            && ((comparison.y >= edge1.y && comparison.y <= edge2.y)
                || (comparison.y <= edge1.y && comparison.y >= edge2.y)))
            && (((comparison.x >= edge1.x && comparison.x < edge2.x)
             || (comparison.x <= edge1.x && comparison.x > edge2.x))
            || ((comparison.y >= edge1.y && comparison.y < edge2.y)
                || (comparison.y <= edge1.y && comparison.y > edge2.y)));
	}

    // SPECIAL MOVEMENT //

	public void Warp(int x, int y)
    {
        locX = x;
        locY = y;
    }

	public void NextMove(Vector2 edge1, Vector2 edge2, Vector2 edge3, Vector2 edge4, bool running)
	{
		Vector2 comparison = new Vector2(locX, locY);
        
		if (IsBetween(comparison, edge1, edge2))
		{
			Vector2 moveVector = edge2 - edge1;
			Move(moveVector, running, false, 1);
		} else if (IsBetween(comparison, edge2, edge3))
        {
            Vector2 moveVector = edge3 - edge2;
            Move(moveVector, running, false, 1);
		} else if (IsBetween(comparison, edge3, edge4))
        {
            Vector2 moveVector = edge4 - edge3;
            Move(moveVector, running, false, 1);
		} else if (IsBetween(comparison, edge4, edge1))
        {
            Vector2 moveVector = edge1 - edge4;
            Move(moveVector, running, false, 1);
        }
	}

    public bool DetectPlayer(int sight)
	{
		for (int i = 0; i < sight; i++)
		{
			switch (currentDir)
			{
                case Direction.North:
					if (entities.GetEntity(locX, locY + i + 1) && entities.GetEntity(locX, locY + i + 1).CompareTag("Player"))
					{
						StartCoroutine(QueueMove(Vector2.up, i));
						return true;
					}
					break;
                case Direction.East:
					if (entities.GetEntity(locX + i + 1, locY) && entities.GetEntity(locX + i + 1, locY).CompareTag("Player"))
					{
                        StartCoroutine(QueueMove(Vector2.right, i));
                        return true;
                    }
					break;
                case Direction.South:
					if (entities.GetEntity(locX, locY - i - 1) && entities.GetEntity(locX, locY - i - 1).CompareTag("Player"))
					{
                        StartCoroutine(QueueMove(Vector2.down, i));
                        return true;
                    }
					break;
                case Direction.West:
					if (entities.GetEntity(locX - i - 1, locY) && entities.GetEntity(locX - i - 1, locY).CompareTag("Player"))
					{
                        StartCoroutine(QueueMove(Vector2.left, i));
                        return true;
                    }
                    break;
			}
		}
		return false;
	}

    // UPDATING ENTITYCONTROL //

	public void UpdateEntities()
	{
        entities.UpdateEntities(null, startX, startY);
		entities.UpdateEntities(this.gameObject, locX, locY);
	}
}