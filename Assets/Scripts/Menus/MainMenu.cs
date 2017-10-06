using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Start () {
		Screen.SetResolution (600,900,false);
	}

	public void StartMainGame  () {
		SceneManager.LoadScene ("Level Choose");
	}

	public void InfinityModeMenu  () {
		SceneManager.LoadScene ("InfinityModeMenu");
	}

	public void StartInfinityMode  () {
		SceneManager.LoadScene ("InfinityMode");
	}

	public void ExitGame () {
		Application.Quit ();
	}

	public void BackToMenu () {
		SceneManager.LoadScene ("MainMenu");
	}

	public void BackToModeChoose () {
		SceneManager.LoadScene ("PlayModeChoose");
	}

	public void ShowCredits () {
		SceneManager.LoadScene ("Credits");
	}

	public void ShowControlls () {
		SceneManager.LoadScene ("ControllsMenu");
	}

	public void ResetAllPlayerPrefs() {
		PlayerPrefs.SetInt ("MaximumLevelReached", 1);
		for (int i = 0; i < 18; i++) {			
			PlayerPrefs.SetInt ("Level_" + i.ToString() + "_star1",  0);
			PlayerPrefs.SetInt ("Level_" + i.ToString() + "_star2",  0);
			PlayerPrefs.SetInt ("Level_" + i.ToString() + "_star3",  0);
		}
	}
}
