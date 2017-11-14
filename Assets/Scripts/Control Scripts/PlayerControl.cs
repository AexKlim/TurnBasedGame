using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Control 
{
	[HideInInspector] public int motherlandsCount = 0;
	[HideInInspector] public int motherlandsNeed = 0;
	[HideInInspector] public int weaponsGot;
	[HideInInspector] public bool allMotherlandsGot;
	[HideInInspector] public int xCellDest;
	[HideInInspector] public int zCellDest;
	[HideInInspector] public bool onDestCell = true;
	[HideInInspector] public GameObject weaponCountText;
	[HideInInspector] public GameObject motherLandsCountText;

	public void Start(){
		weaponCountText = GameObject.Find ("WeaponCountText");
		motherLandsCountText = GameObject.Find ("MotherlandsCountText");
		motherLandsCountText.GetComponent<UnityEngine.UI.Text> ().text = "Motherlands: " + "0/" + motherlandsNeed;
	}

	public override void MoveToCell ()
	{
		EnemyControl[] enemiesToDestroy = new EnemyControl[30];
		int i = 0;

		if (Vector3.Distance (transform.position, new Vector3 (xCellMov, transform.position.y, zCellMov)) > 0.0f) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (xCellMov, transform.position.y, zCellMov), moveSpeed * Time.deltaTime);
		} else {
			isMoving = false;
			lastXCell = xCell;
			lastZCell = zCell;
			xCell = Mathf.RoundToInt(xCellMov);
			zCell = Mathf.RoundToInt(zCellMov);

			foreach (EnemyControl enemy in levelManager.enemies) {
				if (xCell == enemy.xCell && zCell == enemy.zCell) {
					if (weaponsGot <= 0) {
						levelManager.gameOver = true;
						levelManager.Pause ();
					} else {
						enemiesToDestroy [i] = enemy;
						i++;
					}
				}
			}
			bool someEnemiesDestroyed = false;
			for (int j = 0; j < enemiesToDestroy.Length; j++) {
				if (enemiesToDestroy [j] != null) {
					someEnemiesDestroyed = true;
					levelManager.enemies.Remove (enemiesToDestroy [j]);
					Destroy (enemiesToDestroy [j].gameObject);
				}
			}
			if (someEnemiesDestroyed) {
				weaponsGot--;
				weaponCountText.GetComponent<UnityEngine.UI.Text> ().text = "Weapon: " + weaponsGot;
				levelManager.CheckSomebodySeeAPlayer ();
			}
			if (levelManager.somebodySawAPlayer)
				levelManager.playersTurn = false;
		}
		if (Vector3.Distance (transform.position, new Vector3 (xCellDest, transform.position.y, zCellDest)) <= 0.0f) {
			onDestCell = true;
		}
	}

	public override void Move()
	{
		if (onDestCell) {
			if (!isMoving) {
				if (Input.GetMouseButtonDown (0)) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					RaycastHit hit;
					int sum = 0;
					if (Physics.Raycast (ray, out hit, 1000f, cellLayerMask)) {
						xCellDest = hit.collider.gameObject.GetComponent<Cell> ().xCell;
						zCellDest = hit.collider.gameObject.GetComponent<Cell> ().zCell;

						if (xCell - xCellDest == 0 && zCell - zCellDest >= 1) {
							for(int i = zCell; i >= zCellDest; i--){
								if (Physics.Raycast (new Ray (new Vector3 (xCell, 1f, i), Vector3.down), out hit, 10f, cellLayerMask)) {
									sum++;			
								}
							}																																	 //проверка есть ли стены СЗАДИ от игрока
							if (sum == zCell - zCellDest + 1) {
								StartMoveToCellBack ();
								onDestCell = false;
							}
							return;
						}

						if (xCell - xCellDest == 0 && zCell - zCellDest <= -1) {
							for(int i = zCell; i <= zCellDest; i++){
								if (Physics.Raycast (new Ray (new Vector3 (xCell, 1f, i), Vector3.down), out hit, 10f, cellLayerMask)) {                         //проверка есть ли стены ВПЕРЕДИ от игрока
									sum++;			
								}
							}
							if (sum == zCellDest - zCell + 1) {
								StartMoveToCellForward ();
								onDestCell = false;
							}
							return;
						}

						if (xCell - xCellDest >= 1 && zCell - zCellDest == 0) {
							for(int i = xCell; i >= xCellDest; i--){
								if (Physics.Raycast (new Ray (new Vector3 (i, 1f, zCell), Vector3.down), out hit, 10f, cellLayerMask)) {						  //проверка есть ли стены СЛЕВА от игрока
									sum++;			
								}
							}
							if (sum == xCell - xCellDest + 1) {
								StartMoveToCellLeft ();
								onDestCell = false;
							}
							return;
						}

						if (xCell - xCellDest <= -1 && zCell - zCellDest == 0) {
							for(int i = xCell; i <= xCellDest; i++){
								if (Physics.Raycast (new Ray (new Vector3 (i, 1f, zCell), Vector3.down), out hit, 10f, cellLayerMask)) {						 //проверка есть ли стены СПРАВА от игрока
									sum++;			
								}
							}
							if (sum == xCellDest - xCell + 1) {
								StartMoveToCellRight ();
								onDestCell = false;
							}
							return;
						}

					}
				}
			} else
				MoveToCell ();
		} else {
			if (!isMoving) {
				if (xCell - xCellDest == 0 && zCell - zCellDest >= 1) {
					StartMoveToCellBack ();
				}
				if (xCell - xCellDest == 0 && zCell - zCellDest <= -1) {
					StartMoveToCellForward ();
				}
				if (xCell - xCellDest >= 1 && zCell - zCellDest == 0) {
					StartMoveToCellLeft ();
				}
				if (xCell - xCellDest <= -1 && zCell - zCellDest == 0) {
					StartMoveToCellRight ();
				}
			} else {
				MoveToCell ();
			}
		}
	}
}
