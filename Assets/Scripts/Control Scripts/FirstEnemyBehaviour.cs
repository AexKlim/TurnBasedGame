using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemyBehaviour : EnemyControl {

	public override void TryToDetectPlayer(){
		if (!seeAPlayer) {
			if (Mathf.Abs (player.xCell - xCell) <= 2 && player.zCell - zCell == 0 || Mathf.Abs (player.zCell - zCell) <= 2 && player.xCell - xCell == 0) {
				RaycastHit hit;
				int x = 0, z = 0;
				if (player.xCell - xCell == 2) {
					x = 1;
				}
				if (player.xCell - xCell == -2) {
					x = -1;
				}
				if (player.zCell - zCell == 2) {
					z = 1;
				}
				if (player.zCell - zCell == -2) {
					z = -1;
				}
				if (Physics.Raycast (new Ray (transform.position + new Vector3(x,1f,z), Vector3.down), out hit, 10f, cellLayerMask)) {
					seeAPlayer = true;
					levelManager.somebodySawAPlayer = true;
					levelManager.playersTurn = false;
				}
			}
		}
	}

	public override void Move()
	{
		if (!isMoving) {
			if (seeAPlayer) {
				if ((player.xCell - xCell == 2 || player.xCell - xCell == 1) && player.zCell - zCell == 0)
					StartMoveToCellRight ();
				else if ((player.xCell - xCell == -2 || player.xCell - xCell == -1) && player.zCell - zCell == 0)
					StartMoveToCellLeft ();
				else if ((player.zCell - zCell == 2 || player.zCell - zCell == 1) && player.xCell - xCell == 0)
					StartMoveToCellForward ();
				else if ((player.zCell - zCell == -2 || player.zCell - zCell == -1) && player.xCell - xCell == 0)
					StartMoveToCellBack ();
				else if (Mathf.Abs (player.xCell - xCell) >= 1 && Mathf.Abs (player.zCell - zCell) >= 1) {
					if (player.lastXCell - xCell == 1)
						StartMoveToCellRight ();
					else if (player.lastXCell - xCell == -1)
						StartMoveToCellLeft ();
					else if (player.lastZCell - zCell == 1)
						StartMoveToCellForward ();
					else if (player.lastZCell - zCell == -1)
						StartMoveToCellBack ();
				} else
					DontMove ();
			}
		} else
			MoveToCell ();
	}
}
