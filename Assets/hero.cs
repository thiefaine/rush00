using UnityEngine;
using System.Collections;

public class hero : character {

	public Camera	cam;

	// Use this for initialization
	void Start () {
		maxSpeed = 5.0f;
	}

	void FixedUpdate()
	{	
		moveX = Input.GetAxis("Horizontal"); 
		moveY = Input.GetAxis("Vertical"); 
		rb.velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
	}

	// Update is called once per frame
	void Update () {


		head.transform.position = transform.position;
		body.transform.position = transform.position;
		foot.transform.position = transform.position;
		cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
	}
}
