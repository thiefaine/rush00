using UnityEngine;
using System.Collections;

public class character : MonoBehaviour {

	public GameObject	head;
	public GameObject	body;
	public GameObject	foot;
	//public Weapon		currentWeapon;

	public Animator			animLegs;
	protected Rigidbody2D	rb;

	public float maxSpeed = 2.0f;
	public float moveX = 0f;
	public float moveY = 0f;

	// Use this for initialization
	void Start () {
		animLegs  = this.foot.GetComponent<Animator>();
		rb = this.GetComponent<Rigidbody2D>();
	}


	void FixedUpdate()
	{
	}

	// Update is called once per frame
	void Update () {

	}
}
