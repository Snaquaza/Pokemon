using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Up : Animations {
	public Sprite frame1, frame2;

	private void Start()
    {
		sprites.Add(frame1);
		sprites.Add(frame2);      
    }

	public override List<Sprite> Animation()
	{
		return sprites;
	}
}
