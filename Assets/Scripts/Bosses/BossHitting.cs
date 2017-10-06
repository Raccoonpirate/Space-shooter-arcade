using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHitting : MonoBehaviour {

	public GameObject explosion;
	public GameObject explosionPlayer;

	public Slider bossHealthBar;

	public AudioClip getHitSound;

	public int scoreValue;
	public int bossHP;

	private GameController _gameController;

	void Start () {
		GameObject go = GameObject.FindWithTag ("GameController");
		if (go != null) {
			_gameController = go.GetComponent <GameController> ();
		} else {
			Debug.Log ("Game Controller didn't found");
		}
		bossHealthBar = GameObject.Find ("Boss_healthBar").GetComponent<Slider>();
		bossHealthBar.minValue = 0;
		bossHealthBar.value = bossHealthBar.maxValue = bossHP;
		bossHealthBar.wholeNumbers = true;
	}

	void OnTriggerEnter (Collider other) {
		//Debug.Log ("Boss was hit");
		if (other.tag == "Boundary" || other.name == "Boundary" || other.tag == "Hazard") {
			return;
		}
		if (other.tag == "Player") {
			Instantiate (explosionPlayer, other.transform.position, other.transform.rotation);
			_gameController.GameOver ();
		}
		if (other.tag == "Player_shot") {
			Destroy (other.gameObject);
			bossHP -= 10;
			AudioSource.PlayClipAtPoint (getHitSound, transform.position, 1.0f);
			bossHealthBar.value = bossHP ;
			//Debug.Log ("Boss HP: " + bossHP.ToString());
			if (bossHP <= 0) {
				_gameController.AddScore (scoreValue);
				Instantiate (explosion, transform.position, explosion.transform.rotation);
				Destroy (gameObject);
			}
		}
	}
}
