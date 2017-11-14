using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motherboard : MonoBehaviour 
{
	
	private PlayerControl playerControl;


	void Awake () 
	{
		playerControl = GameObject.Find ("Player").GetComponent<PlayerControl> ();
		playerControl.motherlandsNeed++;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			Destroy (gameObject);
			playerControl.motherlandsCount++;
			playerControl.motherLandsCountText.GetComponent<UnityEngine.UI.Text> ().text = "Motherlands: " + playerControl.motherlandsCount + "/" + playerControl.motherlandsNeed;
			if (playerControl.motherlandsCount == playerControl.motherlandsNeed) {
				playerControl.allMotherlandsGot = true;
			}
		}
	}
}
