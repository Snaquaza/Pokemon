using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularBehavior : NPCBehavior {

	public bool running;
	public int x1, y1, x2, y2, x3, y3, x4, y4;

	public override void Behavior()
	{
		Vector2 edge1 = new Vector2(x1, y1);
		Vector2 edge2 = new Vector2(x2, y2);
		Vector2 edge3 = new Vector2(x3, y3);
        Vector2 edge4 = new Vector2(x4, y4);
		gameObject.GetComponent<Movement>().NextMove(edge1, edge2, edge3, edge4, running);
	}

	public override void onInteract()
	{
	}
}
