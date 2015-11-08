using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public Rigidbody2D	rb;
	private float		speed = 20f;
	public bool			isShot = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag != "weapon" && isShot)
		{
			Debug.Log("collision with : " + col.gameObject);
			Debug.Log("collision with name : " + col.gameObject.name);
			GameObject.Destroy(this.gameObject);
		}
	}

	public float getSpeed()
	{
		return speed;
	}

}
