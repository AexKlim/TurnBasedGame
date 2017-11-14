using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject levelMenu;

	public void LoadLevel(int lvl){
		Time.timeScale = 1;
		SceneManager.LoadScene (lvl);
	}
	public void RestartLevel(){
		Time.timeScale = 1;
		SceneManager.LoadScene (Application.loadedLevel);
	}
	public void GoToMainMenu(){
		SceneManager.LoadScene (0);
	}
	public void ShowLevelMenu(){
		levelMenu.SetActive (true);
	}
	public void HideLevelMenu(){
		levelMenu.SetActive (false);
	}
}
