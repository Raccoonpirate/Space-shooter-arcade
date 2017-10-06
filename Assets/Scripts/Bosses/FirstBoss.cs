using UnityEngine;
using System.Collections;

public class FirstBoss : MonoBehaviour {

	public GameObject shot;

	public Transform backGun1_1;
	public Transform backGun1_2;
	public Transform frontGun2_1;
	public Transform frontGun2_2;

	public float backFireRate;
	public float frontFireRate;

	private float _backNextFire;
	private float _frontNextFire;

	private bool _onPosition;

//	Rigidbody rigBody;

	void Awake () {
//		rigBody = GetComponent<Rigidbody> ();
		_onPosition = false;
	}
		
	void Update () {
		if (_onPosition) {
			if (Time.time > _backNextFire) {
				_backNextFire = Time.time + backFireRate;
				GameObject go = Instantiate (shot, backGun1_1.position, backGun1_1.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);

				go = Instantiate (shot, backGun1_2.position, backGun1_2.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);
				//AudioSource.PlayClipAtPoint (audioClip, transform.position, 0.5f);
			} else if (Time.time > _frontNextFire) {
				_frontNextFire = Time.time + frontFireRate;
				GameObject go = Instantiate (shot, frontGun2_1.position, frontGun2_1.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);

				go = Instantiate (shot, frontGun2_2.position, frontGun2_2.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);
			}
		}
	}

	void FixedUpdate () {
		if (transform.position.z <= 9.15f && !_onPosition) {
			_onPosition = true;
			GetComponent<Mover> ().Move (transform.right);
		}
		if (transform.position.x <= -4.9f) {
			//Debug.Log ("Move right");
			GetComponent<Mover> ().Move (transform.right * -1);
		}
		else if (transform.position.x >= 4.9f) {
			//Debug.Log ("Move left");
			GetComponent<Mover> ().Move (transform.right);
		}
		
	}

}
