using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	public GameObject bonus;

	public AudioClip powerUpAudio;

	public Vector3 placeOnPlayer;

	void OnTriggerEnter (Collider other) {
		if (other.tag != "Player") {
			return;
		} else if (other.tag == "Player") {
			GameObject ept = GameObject.Find("Bonuses");
			GameObject go = Instantiate (bonus, ept.transform.position - placeOnPlayer, Quaternion.identity) as GameObject;
			go.transform.SetParent (ept.transform);
			AudioSource.PlayClipAtPoint (powerUpAudio, other.transform.position, 1.0f);
			Destroy (gameObject);
		}
	}

}
