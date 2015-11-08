using UnityEngine;
using System.Collections;

public class hero : character {

	public Camera	cam;
	private bool pickup = false;

	// Use this for initialization
	void Start () {

		currentWeapon = null;
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
			pickup = false;

		// REPLACE THE SPRITES AND THE CAMERA
		head.transform.position = transform.position;
		body.transform.position = transform.position;
		foot.transform.position = transform.position;

		cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

		if (currentWeapon != null) {
			currentWeapon.transform.position = transform.position;
			currentWeapon.GetComponent<weapon>().transform.localRotation = Quaternion.identity;
			currentWeapon.GetComponent<weapon>().transform.localPosition = new Vector2(currentWeapon.GetComponent<weapon>().spriteInHand.transform.localPosition.x + 0.1f, currentWeapon.GetComponent<weapon>().spriteInHand.transform.localPosition.y - 0.4f);
		}

		// ANIMATION LEGS
		if (moveX != 0|| moveY != 0)
			animLegs.SetBool("walking", true);
		else
			animLegs.SetBool("walking", false);

		// GET ANGLE FROM MOUSE POSITION
		rotateBodyToMouse();

		if (Input.GetKey("e") || Input.GetMouseButton(2))
			pickup = true;
		if (Input.GetKeyUp("e"))
			pickup = false;
		if (Input.GetMouseButton(1) && currentWeapon != null)
			releaseWeapon();
		if (Input.GetMouseButton(0) && currentWeapon != null && !currentWeapon.GetComponent<weapon>().isWhiteWeapon) // fire weapon
			currentWeapon.GetComponent<weapon>().shoot();
		if (Input.GetMouseButtonDown(0) && currentWeapon != null && currentWeapon.GetComponent<weapon>().isWhiteWeapon) // white weapon
			currentWeapon.GetComponent<weapon>().shoot();
	}

	void releaseWeapon()
	{
		currentWeapon.GetComponent<weapon>().spriteInHand.GetComponent<SpriteRenderer>().enabled = false;
		currentWeapon.GetComponent<SpriteRenderer>().enabled = true;

		currentWeapon.GetComponent<weapon>().transform.parent = null;
		currentWeapon.GetComponent<weapon>().slide();
		//GameObject.Instantiate(currentWeapon, transform.position, Quaternion.identity);
		currentWeapon = null;
	}

	void rotateBodyToMouse()
	{
		Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition);
		float angleToMouse = Mathf.Atan2 (direction.x - rb.position.x, direction.y - rb.position.y);
		angleToMouse = angleToMouse * 180 / Mathf.PI;

		this.transform.rotation = Quaternion.Euler(0, 0, -angleToMouse + 180);
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.tag == "weapon" && pickup && currentWeapon == null) {
			currentWeapon = col.gameObject;

			currentWeapon.GetComponent<weapon>().spriteInHand.GetComponent<SpriteRenderer>().enabled = true;
			currentWeapon.GetComponent<SpriteRenderer>().enabled = false;

			currentWeapon.GetComponent<weapon>().transform.parent = gameObject.transform;

			currentWeapon.GetComponent<weapon>().transform.position = transform.position;
			currentWeapon.GetComponent<weapon>().transform.localRotation = Quaternion.identity;
			currentWeapon.GetComponent<weapon>().transform.localPosition = new Vector2(currentWeapon.GetComponent<weapon>().spriteInHand.transform.localPosition.x + 0.1f, currentWeapon.GetComponent<weapon>().spriteInHand.transform.localPosition.y - 0.4f);
			currentWeapon.GetComponent<weapon>().transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}
}
