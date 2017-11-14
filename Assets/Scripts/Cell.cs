using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour 
{
	public int xCell;
	public int zCell;
	private PlayerControl playerControl;
	private LevelManager levelManager;
	void Awake()
	{
		playerControl = GameObject.Find ("Player").GetComponent<PlayerControl> ();
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
	}

	public void PaintGreen()
	{
		GetComponent<MeshRenderer> ().material.color = Color.green;
	}

	public void PaintWhite()
	{
		GetComponent<MeshRenderer> ().material.color = Color.white;
	}
}
