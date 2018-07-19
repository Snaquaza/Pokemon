using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {
    
    StaticEvents staticEvents;
    List<StaticEvent> currentEvents;

	// Use this for initialization
	void Start () {
		staticEvents = GetComponent<Movement>().grid.GetComponent<StaticEvents>();
		currentEvents = new List<StaticEvent>();
	}

	public void RunEvent(int x, int y)
    {
		currentEvents = staticEvents.GetEvent(x, y);
		Debug.Log(staticEvents.GetEvent(1, 2).Count);
		CallEvent();
    }

	public void CallEvent()
	{      
        for (int i = 0; i < currentEvents.Count; i++)
        {
            currentEvents[i].OnEvent(gameObject);
        }
		// currentEvents.Clear();
	}

	public void AddEvent(StaticEvent addEvent)
	{
		currentEvents.Add(addEvent);
	}
}
