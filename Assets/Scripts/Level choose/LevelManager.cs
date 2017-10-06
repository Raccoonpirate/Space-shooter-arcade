using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public List<Level> levels = new List<Level>();

	public GameObject levelButton;

	public Transform levelChoosePanel;

	//private int _maximumLevelReached;

	void Start () {
		if (PlayerPrefs.GetInt ("MaximumLevelReached") <= 1) {
			PlayerPrefs.SetInt ("MaximumLevelReached", 1);
		}
		AddLevelButtonsToLevelChoosePanel ();
	}

	void AddLevelButtonsToLevelChoosePanel () {
		int i = 1;
		foreach (var level in levels) {
			GameObject newButton = Instantiate (levelButton, levelChoosePanel) as GameObject;
			LevelButton button = newButton.GetComponent<LevelButton> ();
			button.levelNumber.text = level.levelNumber;
			button.GetComponent<Button> ().onClick.AddListener (() => LoadGameLevel("Level_" + button.levelNumber.text));
			if (i > PlayerPrefs.GetInt ("MaximumLevelReached")) {
				button.GetComponent<Button> ().interactable = false;
			}
			UnlockStars (button);
			i++;
			//newButton.transform.SetParent (levelChoosePanel);
		}
	}

	void UnlockStars (LevelButton button) {		
		if (PlayerPrefs.GetInt ("Level_" + button.levelNumber.text + "_star1") == 1) {
			button.stars [0].SetActive (true);
		}
		if (PlayerPrefs.GetInt ("Level_" + button.levelNumber.text + "_star2") == 1) {
			button.stars [1].SetActive (true);
		}
		if (PlayerPrefs.GetInt ("Level_" + button.levelNumber.text + "_star3") == 1) {
			button.stars [2].SetActive (true);
		}
	}

	void LoadGameLevel (string level){
		SceneManager.LoadScene (level);
	}
}
