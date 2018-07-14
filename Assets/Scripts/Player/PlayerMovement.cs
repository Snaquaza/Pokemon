using UnityEngine;
using System.Collections;

// Brainstorm Games youtube

public class PlayerMovement : MonoBehaviour
{ 
	// Normal Movements Variables
    private float walkSpeed;
    private float curSpeed;
    private float maxSpeed;
	private Rigidbody2D rb;

    void Start()
    {      
        walkSpeed = 5;
		rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        // Move senteces
        rb.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
    }
}