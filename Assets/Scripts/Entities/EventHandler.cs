using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    StaticEvents staticEvents;
    List<StaticEvent> currentEvents;

	// Use this for initialization
	void Start () {
		staticEvents = GetComponent<Movement>().grid.GetComponent<StaticEvents>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RunEvent(int x, int y)
    {
        currentEvents = staticEvents.GetEvent(x, y);
		Debug.Log(currentEvents.Count);
        for (int i = 0; i < currentEvents.Count; i++)
        {
            currentEvents[i].OnEvent(gameObject);
        }
    }
}
