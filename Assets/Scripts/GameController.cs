using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	[SerializeField]
	public List<GameObject> hazardSpawn = new List<GameObject>();
	public EndLevelPanel endLevel;

	public Vector3 spawnValues;

	public GameObject player;
	public GameObject pauseText;

	public Transform playerSpawnPoint;

	public Text scoreText;

	public int hazardCount;
	public int waveWait;
	public int maxWaves;
	public int nextLevelUnlocked;
	public int curLevel;

	public float spawnWait;
	public float startWait;

	public GameObject speed_icon;
	public GameObject firerate_icon;
	public GameObject shield_icon;

	private bool _isLevelEnd;
	private bool _gameOver;

	private int _score;
	private int _maximumLevelScore;

	private AudioSource _audio;

	private float difficultyModifier;

	void Start () {
		Screen.SetResolution (600,900,false);
		Instantiate (player, playerSpawnPoint);
		_gameOver = false;
		_isLevelEnd = false;
		_score = 0;
		_maximumLevelScore = 0;
		_audio = GetComponent<AudioSource> ();
		Time.timeScale = 1.0f;

		//Не знаю как по другому с боссами разбираться, да я знаю что это жопа
		if ((SceneManager.GetActiveScene ().name != "InfinityMode") && (int.Parse (SceneManager.GetActiveScene ().name.Substring (6)) % 5) == 0) {
			difficultyModifier = 1.0f;
		} else {
			difficultyModifier = PlayerPrefs.GetFloat ("DifficultyModifier");
		}

		pauseText.SetActive (false);

		speed_icon.SetActive (false);
		firerate_icon.SetActive (false);
		shield_icon.SetActive (false);

		if (PlayerPrefs.GetInt("GlobalAudioMute") == 0) {
			_audio.mute = true;
		}
		else
		if (PlayerPrefs.GetInt("GlobalAudioMute") == 1){
			_audio.mute = false;
		}
			
		UpdateScore ();
		if (SceneManager.GetActiveScene ().name == "InfinityMode") {
			StartCoroutine (SpawnInfiniteWaves ());
		} else {
			StartCoroutine (SpawnWaves ());
		}
	}

	void Update () {
		if (_isLevelEnd) {
			if (GameObject.FindGameObjectWithTag ("Hazard") == null) {
				_isLevelEnd = false;
				GiveReward ();
			}
		}

		if (Input.GetKeyUp(KeyCode.M)) {
			if (_audio.mute && PlayerPrefs.GetInt("GlobalAudioMute") == 0) {
				_audio.mute = false;
				PlayerPrefs.SetInt ("GlobalAudioMute", 1);
			}
			else 
			if (!_audio.mute && PlayerPrefs.GetInt("GlobalAudioMute") == 1) {
				_audio.mute = true;
				PlayerPrefs.SetInt ("GlobalAudioMute", 0);
			}
		}

		if (Input.GetKeyUp(KeyCode.Escape)) {
			if (Time.timeScale != 0.0f) {
				Time.timeScale = 0.0f;
				pauseText.SetActive (true);
			} else {
				Time.timeScale = 1.0f;
				pauseText.SetActive (false);
			}
		}
	}

	//Для стори мода
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);

		for (int j = 0; !_gameOver; j++) {			
			int hazardNumber = Random.Range (0, hazardSpawn.Count);
			//Подсчитываю максимально допустимое количество очков, которые можно заработать за уровень
			DestroyOnContact doc = hazardSpawn [hazardNumber].GetComponent<DestroyOnContact>();
			if (doc != null) {
				_maximumLevelScore += (doc.scoreValue * hazardCount);
			}
			for (int i = 0; i < hazardCount * difficultyModifier; i++) {
				Vector3 spawnPosition;
				if (! hazardSpawn [hazardNumber].name.Contains ("Boss")) {
					spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				} else {
					spawnPosition = new Vector3 (0, spawnValues.y, spawnValues.z);
				}
				Quaternion spawnRotation = Quaternion.identity;
				GameObject go = Instantiate (hazardSpawn[hazardNumber], spawnPosition, spawnRotation) as GameObject;
				go.GetComponent<Mover> ().Move (hazardSpawn[hazardNumber].transform.forward);
				yield return new WaitForSeconds (spawnWait);
			}

			if (j >= maxWaves * difficultyModifier) {
				break;
			}

			yield return new WaitForSeconds (waveWait / difficultyModifier);
		}
		_isLevelEnd = true;
	}

	//Для бесконечного мода
	IEnumerator SpawnInfiniteWaves () {
		yield return new WaitForSeconds (startWait);
		for (;!_gameOver;) {			
			int hazardNumber = Random.Range (0, hazardSpawn.Count);
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);;
				Quaternion spawnRotation = Quaternion.identity;
				GameObject go = Instantiate (hazardSpawn[hazardNumber], spawnPosition, spawnRotation) as GameObject;
				go.GetComponent<Mover> ().Move (hazardSpawn[hazardNumber].transform.forward);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
		endLevel.GameOverScreen ();
	}

	public void AddScore (int value) {
		_score += value;
		UpdateScore ();
	}

	public void GameOver () {
		endLevel.GameOverScreen ();
		_gameOver = true;
	}

	void UpdateScore () {
		scoreText.text = "score: " + _score;
	}

	void GiveReward () {
		if (!_gameOver) {
			if (_score >= (_maximumLevelScore * 0.5)) {
				PlayerPrefs.SetInt ("Level_" + curLevel.ToString() + "_star1", 1);
			}
			if (_score >= (_maximumLevelScore * 0.75f)) {
				PlayerPrefs.SetInt ("Level_" + curLevel.ToString() + "_star2", 1);
			}
			if (_score >= (_maximumLevelScore * 0.9f)) {
				PlayerPrefs.SetInt ("Level_" + curLevel.ToString() + "_star3", 1);
			}
			endLevel.CompleteLevelScreen ();
		}
	}

	public void  ActivateBuffIcon (string buffName) {
		switch (buffName) {
			case "speed": {
					speed_icon.SetActive (true);
					break;
				}
			case "firerate": {
					firerate_icon.SetActive (true);
					break;
				}
			case "shield": {
					shield_icon.SetActive (true);
					break;
				}
		}
	}

	public void  DeactivateBuffIcon (string buffName) {
		switch (buffName) {
		case "speed": {
				speed_icon.SetActive (false);
				break;
			}
		case "firerate": {
				firerate_icon.SetActive (false);
				break;
			}
		case "shield": {
				shield_icon.SetActive (false);
				break;
			}
		}
	}

	public int Score {
		get { 
			return _score;
		}
	}

	public bool IsGameOver {
		get { 
			return _gameOver;
		}
	}

}
