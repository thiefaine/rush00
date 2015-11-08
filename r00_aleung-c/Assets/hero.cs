using UnityEngine;
using System.Collections;

public class hero : character {

	public Camera	cam;
	//[HideInInspector]
	public GameObject room_obj;

	// Use this for initialization
	void Start () {
		animLegs  = this.foot.GetComponent<Animator>();
		rb = this.GetComponent<Rigidbody2D>();
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

		// REPLACE THE SPRITES AND THE CAMERA
		head.transform.position = transform.position;
		body.transform.position = transform.position;
		foot.transform.position = transform.position;
		cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

		// ANIMATION LEGS
		if (moveX != 0|| moveY != 0)
			animLegs.SetBool("walking", true);
		else
			animLegs.SetBool("walking", false);


		// GET ANGLE FROM MOUSE POSITION
		rotateBodyToMouse();
	}

	void rotateBodyToMouse()
	{
		Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition);
		float angleToMouse = Mathf.Atan2 (direction.x - rb.position.x, direction.y - rb.position.y);
		angleToMouse = angleToMouse * 180 / Mathf.PI;
		//Debug.Log ("Angle : " + angleToMouse);

		this.transform.rotation = Quaternion.Euler(0, 0, -angleToMouse + 180.0F);
	}
}
