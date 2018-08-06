using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextHolder {
	public string name;   

    [TextArea(1, 10)]
	public string[] text;

	public Sprite sprite;
}
