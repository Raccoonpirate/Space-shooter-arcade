using UnityEngine;
using System.Collections;

public class SecondBoss : MonoBehaviour {

	public GameObject shot;
	public GameObject megaCanonShot;
	public GameObject homingMissile;
	public GameObject megaCanonChargeEffect;

	public Transform rocketLauncher_1;
	public Transform rocketLauncher_2;
	public Transform stadnartGun;

	public int rocketsNumber;

	public float rocketBarrageRate;
	public float stadnartShotRate;
	public float megaCanonRate;
	public float nextRocket;
	public float megaCanonChargeTime;
	public float megaCanonTime;

	private float _rocketBarrageNextFire;
	private float _stadnartShotNextFire;
	private float _megaCanonNextFire;
	private float _prevSpeed;

	private bool _onPosition;
	private bool _isRocketBarrage;
	private bool _isMegaCanonCharging;

	void Start () {
		_prevSpeed = GetComponent<Mover> ().speed;
		_onPosition = false;
		_isRocketBarrage = false;
		_rocketBarrageNextFire = rocketBarrageRate;
		_megaCanonNextFire = megaCanonRate / 2;
		_isMegaCanonCharging = false;
		megaCanonChargeEffect.SetActive (false);
	}
	
	void Update () {
		if (_onPosition) {
			if (Time.time > _stadnartShotNextFire && !_isRocketBarrage && !_isMegaCanonCharging) {
				_stadnartShotNextFire = Time.time + stadnartShotRate;
				GameObject go = Instantiate (shot, stadnartGun.position, stadnartGun.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);
			}
			if (!_isRocketBarrage && !_isMegaCanonCharging) {
				if (Time.time > _rocketBarrageNextFire) {
					StartCoroutine (RocketBarrageSkill (homingMissile));
					_rocketBarrageNextFire = Time.time + rocketBarrageRate;
				}
			}
			if (Time.time > _megaCanonNextFire && !_isMegaCanonCharging && !_isRocketBarrage) {
				_isMegaCanonCharging = true;
				StartCoroutine (MegaCanon ());
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

	IEnumerator RocketBarrageSkill(GameObject missile) {
		_isRocketBarrage = true;
		for (int i = 0; i < rocketsNumber; i++) {
			GameObject go = Instantiate (missile, rocketLauncher_1.position, rocketLauncher_1.rotation) as GameObject;
			go.GetComponent<Mover> ().Move (missile.transform.forward * 5.0f);

			go = Instantiate (missile, rocketLauncher_2.position, rocketLauncher_2.rotation) as GameObject;
			go.GetComponent<Mover> ().Move (missile.transform.forward * 5.0f);
			yield return new WaitForSeconds (nextRocket);
		}
		_isRocketBarrage = false;
	}

	IEnumerator MegaCanon() {
		Vector3 prevVel = GetComponent<Mover> ().GetPrevVelocity();
		GetComponent<Mover> ().speed = 0;
		GetComponent<Mover> ().Move(Vector3.zero);
		megaCanonChargeEffect.SetActive (true);
		//Debug.Log ("Chargning!");
		yield return new WaitForSeconds (megaCanonChargeTime);

		GetComponent<Mover> ().speed = 0.4f;
		GetComponent<Mover> ().Move (prevVel);
		megaCanonShot.SetActive (true);
		//Debug.Log ("Shooting!");
		yield return new WaitForSeconds (megaCanonTime);

		//Debug.Log ("At ease!");
		megaCanonShot.SetActive (false);
		GetComponent<Mover> ().speed = _prevSpeed;
		GetComponent<Mover> ().Move (prevVel * -1);
		megaCanonChargeEffect.SetActive (false);
		_megaCanonNextFire = Time.time + megaCanonRate;
		_isMegaCanonCharging = false;
		//_rocketBarrageNextFire = rocketBarrageRate / 2;
	}

}
