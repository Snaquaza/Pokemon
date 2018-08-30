using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEvents : MonoBehaviour {

	public List<Warp> warpArray;
    
	private List<StaticEvent>[] eventArray;

	// Use this for initialization
	void Start () {
		eventArray = new List<StaticEvent>[32 * 32];
		for (int i = 0; i < eventArray.Length; i++)
			eventArray[i] = new List<StaticEvent>();

		foreach (Warp warp in warpArray)
			eventArray[(int)warp.Location().x + (int)warp.Location().y * 32].Add(warp);
		
        // need to eventually be read from somewhere
        // eventArray[11 + 14 * 32].Add(new Warp(19, 14));
	}

    public List<StaticEvent> GetEvent(int x, int y)
	{
		return eventArray[x + y * 32];
	}
}