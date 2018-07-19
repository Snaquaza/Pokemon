using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEvents : MonoBehaviour {
    
	private List<StaticEvent>[] eventArray;

	// Use this for initialization
	void Start () {
		eventArray = new List<StaticEvent>[32 * 32];
		for (int i = 0; i < eventArray.Length; i++)
			eventArray[i] = new List<StaticEvent>();

        // need to eventually be read from somewhere
		eventArray[1 + 2 * 32].Add(new Warp(11, 9));
	}

    public List<StaticEvent> GetEvent(int x, int y)
	{
		return eventArray[x + y * 32];
	}
}

public abstract class StaticEvent {
	public abstract void OnEvent(GameObject player);
}

public class Warp : StaticEvent {
	private int x, y;
	public Warp(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public override void OnEvent(GameObject player)
	{
		player.GetComponent<Movement>().Warp(x, y);
	}
}

// Create Trainer event
public class Interact : StaticEvent
{
	private GameObject gameObject;
	public Interact(GameObject gameObject)
    {
		this.gameObject = gameObject;
    }

    public override void OnEvent(GameObject player)
	{
        gameObject.GetComponent<EntityControl>().OnInteract();
    }
}

public class Face : StaticEvent
{
	private Direction direction;

	public Face(Direction direction)
	{
		this.direction = direction;
	}

	public override void OnEvent(GameObject player)
	{
		player.GetComponent<Movement>().Face(direction);
	}
}