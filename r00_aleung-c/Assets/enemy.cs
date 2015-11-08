using UnityEngine;
using System.Collections;

public class enemy : character {
	public bool			standby = true;
	private bool		_player_in_collider = false;
	public bool			alerted = false;


	private Vector2		_last_known_pos = Vector2.zero;
	public bool			dead = false;
	public LayerMask 	 myLayerMask = 0;
	public float		speed = 0.2F;
	public float		attack_delay = 30.0f;
	public GameObject	player;

	//[HideInInspector]
	public GameObject room_obj;

	[HideInInspector]
	public GameObject	target_ennemy = null;

	private PolygonCollider2D my_collider_poly;
	private BoxCollider2D my_collider_box;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Hero") { // hero dans le champs de vision.
			_player_in_collider = true;
			Vector2 obj_pos = transform.position;
			Vector2 direction = new Vector2(collider.gameObject.transform.position.x - obj_pos.x, collider.gameObject.transform.position.y - transform.position.y);

			RaycastHit2D hit = Physics2D.Raycast(obj_pos, direction, Mathf.Infinity, myLayerMask );
			if (hit && hit.collider.gameObject.name == "Hero") { // aucun mur trouver
				Debug.Log ("enemy saw hero");
				_last_known_pos = player.transform.position;
				alerted = true;
			}
			else {
				Debug.Log ("enemy saw " + hit.collider.gameObject.name);
				Debug.Log ("at " + hit.collider.gameObject.transform.position.x + "x " + hit.collider.gameObject.transform.position.y + "y.");
			}
		}
	}

	// lorsque l'enemy entends un coup de feu. va le faire avancer a la pos du hero ou la porte correspondante.
	public void OnHeardSomething () {
		alerted = true;
		_last_known_pos = player.transform.position;
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.name == "Hero") {
			Vector2 obj_pos = transform.position;
			Vector2 direction = new Vector2(collider.gameObject.transform.position.x - obj_pos.x, collider.gameObject.transform.position.y - transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(obj_pos, direction, Mathf.Infinity, myLayerMask );
			if (hit && hit.collider.gameObject.name == "Hero") {
				Debug.Log ("enemy saw hero");
				alerted = true;
				_last_known_pos = player.transform.position;
			}
			else {
				Debug.Log ("enemy saw " + hit.collider.gameObject.name);
				Debug.Log ("at " + hit.collider.gameObject.transform.position.x + "x " + hit.collider.gameObject.transform.position.y + "y.");
			}
		}

	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.name == "Hero") {
			_player_in_collider = false;

		}
	}

	// coroutine ki attaque toutes les 3 sec si le hero est vu.
/*	IEnumerator delay_attack() { 
		while (alerted == true) {
			Debug.Log ("enemy attack !"); // fonction attack a faire;
			yield return new WaitForSeconds(attack_delay);
		}

	}*/

	public void move_to_door() {
		//rotate enemy toward player
		Vector2 target_pos;
		if (player.GetComponent<hero> ().room_obj.GetComponent<room>().room_id > room_obj.GetComponent<room>().room_id)
			target_pos = room_obj.GetComponent<room>().next_door_pos;
		else
			target_pos = room_obj.GetComponent<room>().prev_door_pos;
		float angle = Mathf.Atan2(target_pos.x - transform.position.x, target_pos.y - transform.position.y) * 180 / Mathf.PI;
		transform.rotation = Quaternion.Euler(0, 0, -angle + 180.0F);
		
		// move enemy toward door;
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target_pos, 3 * Time.deltaTime);
	}

	public void move_to(Vector2 _last_known_pos) {
		//rotate enemy toward player
		Vector2 target_pos = _last_known_pos;
		float angle = Mathf.Atan2(target_pos.x - transform.position.x, target_pos.y - transform.position.y) * 180 / Mathf.PI;
		transform.rotation = Quaternion.Euler(0, 0, -angle + 180.0F);

		// move enemy toward player;
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _last_known_pos, 3 * Time.deltaTime);
	}

	public void check_attack() {
		// StartCoroutine ("delay_attack");
	}

	// Use this for initialization
	void Start () {
		_last_known_pos = player.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (alerted == true) {
			if (player.GetComponent<hero> ().room_obj != this.room_obj) // si enemy et hero sont dans des salles differentes
				move_to_door();
			else {
				move_to(_last_known_pos);
			}
			check_attack();
		}
	}
}
