using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyControl : Control 
{
	[HideInInspector] public PlayerControl player;
	 public bool seeAPlayer;

	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerControl> ();
		levelManager.enemies.Add (this);
	}

	public virtual void DontMove(){
		if (levelManager.AllEnemiesMoved ())
			levelManager.playersTurn = true;
	}

	public override void MoveToCell ()
	{
		if (Vector3.Distance (transform.position, new Vector3 (xCellMov, transform.position.y, zCellMov)) > 0.0f) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (xCellMov, transform.position.y, zCellMov), moveSpeed * Time.deltaTime);
		} else {
			isMoving = false;
			lastXCell = xCell;
			lastZCell = zCell;
			xCell = xCellMov;
			zCell = zCellMov;
			if (xCell == player.xCell && zCell == player.zCell) {
				levelManager.gameOver = true;
				levelManager.Pause ();
			}
			if (levelManager.AllEnemiesMoved())
				levelManager.playersTurn = true;
		}
	}

	public abstract void TryToDetectPlayer();
}
