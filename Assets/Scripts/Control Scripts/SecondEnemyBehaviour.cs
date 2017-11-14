using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnemyBehaviour : EnemyControl {

	public override void TryToDetectPlayer(){
		if (!seeAPlayer) {
			if (player.zCell - zCell == 0 || player.xCell - xCell == 0) {
				seeAPlayer = true;
				levelManager.somebodySawAPlayer = true;
				levelManager.playersTurn = false;
			}
		}
	}

	public override void DontMove(){
		base.DontMove ();
		seeAPlayer = false;
	}

	public override void Move(){
		if (!isMoving) {
			if (seeAPlayer) {
				if ((player.xCell - xCell > 0) && player.zCell - zCell == 0)
					StartMoveToCellRight ();
				else if ((player.xCell - xCell < 0) && player.zCell - zCell == 0)
					StartMoveToCellLeft ();
				else if ((player.zCell - zCell > 0) && player.xCell - xCell == 0)
					StartMoveToCellForward ();
				else if ((player.zCell - zCell < 0) && player.xCell - xCell == 0)
					StartMoveToCellBack ();
				else 
					DontMove ();
			}
		} else
			MoveToCell ();
		
	}
}
