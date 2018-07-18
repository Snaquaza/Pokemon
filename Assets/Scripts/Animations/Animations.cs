using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animations : MonoBehaviour {   
    protected List<Sprite> sprites = new List<Sprite>();
	public abstract List<Sprite> Animation();   
}
