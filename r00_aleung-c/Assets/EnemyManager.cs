using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyManager : MonoBehaviour {
	public List <enemy> 	enemy_list = new List<enemy>();
	// Use this for initialization
	void Start () {
		enemy_list = FindObjectsOfType <enemy> ().ToList();
	}

	public List <enemy> get_updated_list() {
		enemy_list = FindObjectsOfType <enemy> ().ToList();
		return (enemy_list);
	}

	public void set_updated_list(List <enemy> new_list) {
		enemy_list = new_list;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
