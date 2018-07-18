using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animations : MonoBehaviour
{
	public float speed;
    protected List<Sprite> sprites = new List<Sprite>();
	public abstract List<Sprite> Animation();   
}
