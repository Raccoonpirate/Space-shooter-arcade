using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public AudioClip audioClip;

	public GameObject shot;

	public Transform shotSpawn;

	public float fireRate;

	private float _nextFire;

	void Update () {
		if (Time.time > _nextFire) {
			_nextFire = Time.time + fireRate;
			GameObject go = Instantiate (shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
			go.GetComponent<Mover> ().Move (shot.transform.forward);
			//AudioSource.PlayClipAtPoint (audioClip, transform.position, 0.5f);
		}
	}
}
