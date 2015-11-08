using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class weapon : MonoBehaviour {

	public GameObject			spriteInHand;
	public GameObject			bullet;
	protected float				fireRate;
	protected int				nbAmos;
	protected int				speedDrop = 10;
	public Rigidbody2D			rb;
	private List<GameObject> 	listBullet;
	private bool				canShoot = true;

	public bool					isWhiteWeapon;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void slide() {
		rb.AddForce(-transform.up * speedDrop, ForceMode2D.Impulse);
	}

	public void shoot() {
		if (nbAmos > 0 && canShoot)
		{
			nbAmos -= 1;

			StartCoroutine(canShootCoroutine());
			GameObject b = (GameObject)Instantiate(bullet.gameObject, new Vector2(transform.position.x, transform.position.y + 0.5f), bullet.gameObject.transform.localRotation);

			b.GetComponent<bullet>().transform.parent = gameObject.transform;
			
			b.GetComponent<bullet>().transform.position = transform.position;
			b.GetComponent<bullet>().transform.localRotation = Quaternion.Euler(0, 0, -90);
			b.GetComponent<bullet>().transform.localPosition = new Vector2(b.GetComponent<bullet>().transform.localPosition.x + 0.1f, b.GetComponent<bullet>().transform.localPosition.y - 0.4f);
			b.GetComponent<bullet>().transform.localScale = new Vector3 (1f, 1f, 1f);
			b.GetComponent<bullet>().transform.parent = null;

			StartCoroutine(goBullet(b));
		} else if (nbAmos == -1 && canShoot)
		{
			StartCoroutine(canShootCoroutine());
			GameObject b = (GameObject)Instantiate(bullet.gameObject, transform.position, bullet.gameObject.transform.localRotation);

			b.GetComponent<bullet>().transform.parent = gameObject.transform;
			
			b.GetComponent<bullet>().transform.position = transform.position;
			b.GetComponent<bullet>().transform.localRotation = Quaternion.Euler(0, 0, -90);
			b.GetComponent<bullet>().transform.localPosition = new Vector2(b.GetComponent<bullet>().transform.localPosition.x + 0.1f, b.GetComponent<bullet>().transform.localPosition.y - 0.4f);
			b.GetComponent<bullet>().transform.localScale = new Vector3 (1f, 1f, 1f);
			b.GetComponent<bullet>().transform.parent = null;

			StartCoroutine(goNotABullet(b));
		}
	}

	IEnumerator goNotABullet(GameObject b)
	{
		Rigidbody2D r = b.GetComponent<Rigidbody2D>();
		b.GetComponent<SpriteRenderer>().enabled = true;
		r.AddForce(-transform.up * 0, ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.1f);
		GameObject.Destroy(b);
	}

	IEnumerator goBullet(GameObject b)
	{
		Rigidbody2D r = b.GetComponent<Rigidbody2D>();
		b.GetComponent<SpriteRenderer>().enabled = true;
		r.AddForce(-transform.up * b.GetComponent<bullet>().speed, ForceMode2D.Impulse);
		yield return new WaitForSeconds(2.0f);
		GameObject.Destroy(b);
	}

	IEnumerator canShootCoroutine()
	{
		canShoot = false;
		yield return new WaitForSeconds(fireRate);
		canShoot = true;
	}
}
