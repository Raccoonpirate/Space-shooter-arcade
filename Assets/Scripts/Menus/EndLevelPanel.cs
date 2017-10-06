using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLevelPanel : MonoBehaviour {

	public GameObject gameOverMenu;
	public GameObject levelCompleteMenu;

	public Text scoreReached;

	public GameController gameController;

	private int _nextLevel;

	void Start () {
		gameOverMenu.SetActive (false);
		levelCompleteMenu.SetActive (false);
		scoreReached.text = "";
	}

	public void Update () {
		if (Input.GetKeyUp(KeyCode.Space)) {
			if (gameOverMenu.activeSelf) {
				RestartCurrentLevel ();
			}
			if (levelCompleteMenu.activeSelf) {
				if (gameController.curLevel == 20) {
					BackToLevelChoose ();
				} else {
					GoToNextLevel ();
				}
			}
		}
	}

	public void GameOverScreen () {
		gameOverMenu.SetActive (true);
		scoreReached.text = "Your score: " + gameController.Score.ToString ();
		if (SceneManager.GetActiveScene().name == "InfinityMode") {
			if (PlayerPrefs.GetInt ("InfinityModeRecord") < gameController.Score) {
				PlayerPrefs.SetInt ("InfinityModeRecord", gameController.Score);
			}
		}
	}

	public void CompleteLevelScreen () {
		levelCompleteMenu.SetActive (true);
		scoreReached.text = "Your score: " + gameController.Score.ToString ();

		_nextLevel = gameController.nextLevelUnlocked;
		if (PlayerPrefs.GetInt("MaximumLevelReached") <= _nextLevel) {
			PlayerPrefs.SetInt ("MaximumLevelReached", _nextLevel);
			//Debug.Log ("Level " + nextLevel.ToString() + " unlocked!");
		}
	}

	public void BackToLevelChoose() {
		SceneManager.LoadScene ("Level Choose");
	}

	public void BackToInfinityModeMenu() {
		SceneManager.LoadScene ("InfinityModeMenu");
	}

	public void RestartCurrentLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GoToNextLevel () {
		SceneManager.LoadScene ("Level_" + _nextLevel.ToString());
	}

}
