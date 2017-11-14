using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {


	private PlayerControl playerControl;

	void Start () 
	{
		playerControl = GameObject.Find ("Player").GetComponent<PlayerControl> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			Destroy (gameObject);
			playerControl.weaponsGot++;
			playerControl.weaponCountText.GetComponent<UnityEngine.UI.Text> ().text = "Weapon: " + playerControl.weaponsGot;
		}
	}
}
