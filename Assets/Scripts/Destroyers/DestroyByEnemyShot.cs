using UnityEngine;
using System.Collections;

public class DestroyByEnemyShot : MonoBehaviour {

	public GameObject explosionPlayer;

	private GameController _gameController;

	void Start () {
		GameObject go = GameObject.FindWithTag ("GameController");
		if (go != null) {
			_gameController = go.GetComponent <GameController> ();
		} else {
			Debug.Log ("Game Controller didn't found");
		}
	}

	void OnTriggerEnter (Collider other) {
		//Debug.Log ("Destroy by  enemy bullet");
		if (other.tag == "Boundary" || other.name == "Boundary") {
			return;
		}
		if (other.tag == "Hazard") {
			return;
		}
		if (other.tag == "Player") {
			Instantiate (explosionPlayer, other.transform.position, other.transform.rotation);
			_gameController.GameOver ();
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
		if (gameObject.name.Contains("Missile") && (other.tag == "Player_shot")){//.name.Contains("Bolt")) {
			//Debug.Log ("contact with players bullet");
			Destroy (gameObject);
		}
	}

}
