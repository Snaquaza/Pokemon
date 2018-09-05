using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour {

	public float setting;

    public void SetSpeed()
	{
		FindObjectOfType<Options>().textSpeed = setting;
		FindObjectOfType<Options>().OnUpdateOptions();   
	}
}
