using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Warp : StaticEvent {
	public Vector2 from, to;
	public override void OnEvent(GameObject gameObject)
	{
        gameObject.GetComponent<Movement>().Warp((int)to.x, (int)to.y);
	}
    public Vector2 Location()
	{
		return from;
	}
}
