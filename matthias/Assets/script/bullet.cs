using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public Rigidbody2D	rb;
	public float		speed = 30f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
