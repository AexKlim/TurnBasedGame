using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour 
{

	//public GameObject finishPref;
	private LevelManager levelManager;
	private PlayerControl playerControl;

	void Start()
	{
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		playerControl = GameObject.Find ("Player").GetComponent<PlayerControl> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			if (playerControl.allMotherlandsGot) {
				levelManager.gameOver = true;
				levelManager.Pause ();
			}
		}
	}
}
