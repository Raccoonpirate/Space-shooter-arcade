using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {

	public AudioClip audioClip;

	public GameObject explosion;
	public GameObject explosionPlayer;

	public int scoreValue;

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
		//Debug.Log ("Destroy on contact");
		//Debug.Log (other.tag + "  " + other.name);
		if (other.tag == "Boundary" || other.tag == "Hazard" || other.tag == "Bonus") {
			return;
		}
		AudioSource.PlayClipAtPoint (audioClip, other.transform.position, 1.0f);
		Instantiate (explosion, transform.position, explosion.transform.rotation);
		Destroy (gameObject);
		_gameController.AddScore (scoreValue);

		if (other.tag == "Player_Shot") {
			Destroy (other.gameObject);
		}
		if (other.tag == "Player") {
			Instantiate (explosionPlayer, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
			_gameController.GameOver ();
		}

	}

}
