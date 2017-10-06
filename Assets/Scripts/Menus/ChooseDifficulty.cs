using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseDifficulty : MonoBehaviour {

	public GameObject chooseDifficultyPanel;

	public Sprite easySprite;
	public Sprite normalSprite;
	public Sprite hardSprite;

	void Start () {
		if (PlayerPrefs.GetFloat ("DifficultyModifier") == 0) {
			PlayerPrefs.SetFloat ("DifficultyModifier", 1.0f);
		}
		chooseDifficultyPanel.SetActive (false);
		ChangeBTNSprite (PlayerPrefs.GetFloat ("DifficultyModifier"));
	}

	public void DifficultyChoosen(float difficultyModifier){
		PlayerPrefs.SetFloat ("DifficultyModifier", difficultyModifier);
		chooseDifficultyPanel.SetActive (false);
		ChangeBTNSprite (difficultyModifier);
	}

	public void ShowChooseDifficultyPanel () {
		chooseDifficultyPanel.SetActive (true);
	}

	private void ChangeBTNSprite(float mod){
		if (mod == 1.0f) {
			GetComponent<Image> ().sprite = easySprite;
		}
		else 
		if (mod == 1.4f) {
			GetComponent<Image> ().sprite = normalSprite;
		}
		else 
		if (mod == 1.8f) {
			GetComponent<Image> ().sprite = hardSprite;
		}
	}

}
