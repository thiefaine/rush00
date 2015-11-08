using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject Player;
	public GameObject EnemyManager;
	public GameObject RoomManager;
	public GameObject  UI_end;
	private List <enemy> enemy_list;
	public bool		game_win = false;

	// Use this for initialization
	void Start () {
		enemy_list = EnemyManager.GetComponent<EnemyManager> ().get_updated_list ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.GetComponent<hero> ().isAlive == false) {
			UI_end.GetComponent<RectTransform> ().localPosition = Vector2.zero;
		}
		enemy_list = EnemyManager.GetComponent<EnemyManager> ().get_current_list ();
		int enemy_nb = enemy_list.Count ();
		Debug.Log ("nb ennemy: " + enemy_nb);
		if (enemy_nb == 0) {
			UI_end.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			UI_end.transform.Find("main_text").GetComponent<Text> ().text = "Level Clear";
			Color32 new_color;
			new_color.r = 228;
			new_color.g = 190;
			new_color.b = 79;
			new_color.a = 100;
			UI_end.GetComponent<Image> ().color = new_color;
		}
	}
}
