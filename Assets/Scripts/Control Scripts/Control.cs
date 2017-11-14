using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Control : MonoBehaviour 
{
	public float moveSpeed;
	[HideInInspector] public int xCell;
	[HideInInspector] public int zCell; 
	[HideInInspector] public int xCellMov; 
	[HideInInspector] public int zCellMov; 
	[HideInInspector] public int lastXCell;
	[HideInInspector] public int lastZCell;
	[HideInInspector] public bool isMoving;
	[HideInInspector] public LevelManager levelManager;
	public LayerMask cellLayerMask;


	void Awake(){
		RaycastHit hit;
		if (Physics.Raycast (new Ray (transform.position + Vector3.up, Vector3.down), out hit, 10f, cellLayerMask)) {
			int x = hit.collider.gameObject.GetComponent<Cell> ().xCell;
			int z = hit.collider.gameObject.GetComponent<Cell> ().zCell;
			xCell = x;
			zCell = z;
		}
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
	}

	public void StartMoveToCell(int x, int z){
		isMoving = true;
		xCellMov = x;
		zCellMov = z;
	}

	public void StartMoveToCellForward(){
		isMoving = true;
		xCellMov = xCell;
		zCellMov = zCell + 1;
	}
	public void StartMoveToCellBack(){
		isMoving = true;
		xCellMov = xCell;
		zCellMov = zCell - 1;
	}
	public void StartMoveToCellLeft(){
		isMoving = true;
		xCellMov = xCell - 1;
		zCellMov = zCell;
	}
	public void StartMoveToCellRight(){
		isMoving = true;
		xCellMov = xCell + 1;
		zCellMov = zCell;
	}
	public abstract void MoveToCell ();
	public abstract void Move ();
}
