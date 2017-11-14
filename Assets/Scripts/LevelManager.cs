using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
	[HideInInspector] public MenuManager menuManager;
	[HideInInspector] public PlayerControl player;
	[HideInInspector] public List<EnemyControl> enemies;
	[HideInInspector] public bool playersTurn = true;
	[HideInInspector] public bool somebodySawAPlayer;
	UnityEvent onLevelComplete;
	UnityEvent onPlayerDeath;
	[HideInInspector] public bool paused;
	[HideInInspector] public bool gameOver;
	public float time;
	[HideInInspector] public float timer;
	private GameObject timerText;


	void Awake(){
		menuManager = GameObject.Find ("MenuManager").GetComponent<MenuManager> ();
		player = GameObject.Find ("Player").GetComponent<PlayerControl>();
		timerText = GameObject.Find ("TimerText");
		timer = time;
		if(onLevelComplete == null){
			onLevelComplete = new UnityEvent(); 
		}
		onLevelComplete.AddListener (Pause);

		if(onPlayerDeath == null){
			onPlayerDeath = new UnityEvent();
		}
		onPlayerDeath.AddListener (Pause);
	}

	void Update()
	{
		if (timer >= 0) {
			timer -= Time.deltaTime;
			timerText.GetComponent<UnityEngine.UI.Text> ().text = "Time left: " + Mathf.RoundToInt(timer);
			if (playersTurn) {
				player.Move ();
				foreach (EnemyControl enemy in enemies)
					enemy.TryToDetectPlayer ();
			} else {
				foreach (EnemyControl enemy in enemies) {
					enemy.Move ();
				}
			}

			if (Input.GetKeyDown (KeyCode.Escape)){
				if (!paused)
					Pause ();
				else if (!gameOver)
					ContinueGame ();
			}
		} else {
			if (!paused) {
				gameOver = true;
				Pause ();
			}
		}
	}

	public bool AllEnemiesMoved(){
		int count = 0;
		foreach (EnemyControl enemy in enemies) {
			if (enemy.isMoving == false)
				count++;
		}
		if (count == enemies.Count)
			return true;
		else
			return false;
	}  

	public void CheckSomebodySeeAPlayer(){
		int count = 0;
		foreach (EnemyControl enemy in enemies) {
			if (enemy.seeAPlayer == false)
				count++;
		}
		if (count == enemies.Count) {
			somebodySawAPlayer = false;
		}
	}

	public void Pause(){
		Time.timeScale = 0;
		menuManager.ShowLevelMenu ();
		paused = true;
	}

	public void ContinueGame(){
		timer = time;
		Time.timeScale = 1;
		menuManager.HideLevelMenu ();
		paused = false;
	}
}
