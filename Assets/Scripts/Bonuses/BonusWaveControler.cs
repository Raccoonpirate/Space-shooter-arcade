using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusWaveControler : MonoBehaviour {

	public List<GameObject> bonusSpawn = new List<GameObject>();

	public float spawnWait;
	public float startWait;
	public float bonusChance;

	private Vector3 spawnValues;

	private GameController _gameController;

	void Awake () {
		GameObject go = GameObject.FindWithTag ("GameController");
		if (go != null) {
			_gameController = go.GetComponent <GameController> ();
		} else {
			Debug.Log ("Game Controller didn't found");
		}
	}

	void Start () {
		StartCoroutine (SpawnBonuses ());
		spawnValues = new Vector3 (6.2f, 0, 11);
	}

	IEnumerator SpawnBonuses () {
		yield return new WaitForSeconds (startWait);
		for (;!_gameController.IsGameOver;) {			
			if (Random.value < bonusChance) {
				int bonusNumber = Random.Range (0, bonusSpawn.Count);
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				GameObject go = Instantiate (bonusSpawn[bonusNumber], spawnPosition, bonusSpawn[bonusNumber].transform.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (bonusSpawn[bonusNumber].transform.up);
			}
			yield return new WaitForSeconds (spawnWait);
		}
	}

}
