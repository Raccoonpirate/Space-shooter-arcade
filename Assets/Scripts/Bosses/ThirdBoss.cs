using UnityEngine;
using System.Collections;

public class ThirdBoss : MonoBehaviour {

	//******* швырялка астероидов *********
	public GameObject leftArm;
	public GameObject rightArm;
	public GameObject asteroid;

	public Transform astThrowPlaceLeft;
	public Transform astThrowPlaceRight;

	public float astThrowRateLeft;
	public float astThrowRateRight;
	public float astThrowingStrength;

	private float _astThrowNextLeft;
	private float _astThrowNextRight;

	private GameObject _player;

	private Vector3 _direction;
	//************************************

	//*********** рокетки ***************
	public GameObject rocket;
	//public GameObject targetRocket;

	public Transform rocketLauncher_1;
	public Transform rocketLauncher_2;

	public int rocketsNumber;

	public float rocketLauncherFireRate;
	//public float targetLauncherFireRate;
	public float nextRocket;

	private float _rocketLauncherNext;
	//private float _targetLauncherNext;

	private bool _isRocketBarrage;
	//************************************

	//*********** обычное ***************
	public GameObject shot;

	public Transform gun_1;
	public Transform gun_2;
	public Transform gun_3;
	public Transform gun_4;

	public float gunsFireRate;

	private float _gunNext;

	//************************************


	private bool _onPosition;

	void Start () {
		_onPosition = false;
		_isRocketBarrage = false;
		_rocketLauncherNext = rocketLauncherFireRate;
		//_targetLauncherNext = targetLauncherFireRate;
	}
	
	void Update () {
		if (_onPosition) {
			if (Time.time > _astThrowNextLeft) {
				_player = GameObject.FindGameObjectWithTag ("Player");
				if (_player != null) {
					_astThrowNextLeft = Time.time + astThrowRateLeft;
					leftArm.GetComponent<Animator> ().Play ("Boss_3_LeftArm");
					GameObject go = Instantiate (asteroid, astThrowPlaceLeft.position, astThrowPlaceLeft.rotation) as GameObject;
					_direction = _player.transform.position - astThrowPlaceLeft.position;
					_direction.Normalize ();
					go.GetComponent<Mover> ().Move (-_direction * astThrowingStrength);
				}
			}
			if (Time.time > _astThrowNextRight) {
				_player = GameObject.FindGameObjectWithTag ("Player");
				if (_player != null) {
					_astThrowNextRight = Time.time + astThrowRateRight;
					rightArm.GetComponent<Animator> ().Play ("Boss_3_LeftArm");
					GameObject go = Instantiate (asteroid, astThrowPlaceRight.position, astThrowPlaceRight.rotation) as GameObject;
					_direction = _player.transform.position - astThrowPlaceRight.position;
					_direction.Normalize ();
					go.GetComponent<Mover> ().Move (-_direction * astThrowingStrength);
				}
			}
			if (Time.time > _rocketLauncherNext && !_isRocketBarrage) {
				/*if (Time.time > _rocketLauncherNext){
					StartCoroutine (RocketBarrageSkill (rocket));
					_rocketLauncherNext = Time.time + rocketLauncherFireRate;
					_targetLauncherNext += 7.0f;
				}*/
				//if (Time.time > _targetLauncherNext){
				StartCoroutine (RocketBarrageSkill (rocket));
				_rocketLauncherNext = Time.time + rocketLauncherFireRate;
				//}
			}
			if (Time.time > _gunNext) {
				_gunNext = Time.time + gunsFireRate;

				GameObject go = Instantiate (shot, gun_1.position, gun_1.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);

				go = Instantiate (shot, gun_2.position, gun_2.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);

				go = Instantiate (shot, gun_3.position, gun_3.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);

				go = Instantiate (shot, gun_4.position, gun_4.rotation) as GameObject;
				go.GetComponent<Mover> ().Move (shot.transform.forward);
			}
		}

	}

	void FixedUpdate () {
		if (transform.position.z <= 9.15f && !_onPosition) {
			_onPosition = true;
			GetComponent<Mover> ().Move (transform.right);
		}
		if (transform.position.x <= -4.4f) {
			//вправо
			GetComponent<Mover> ().Move (transform.right * -1);
		}
		else if (transform.position.x >= 4.4f) {
			//влево
			GetComponent<Mover> ().Move (transform.right);
		}

	}

	IEnumerator RocketBarrageSkill(GameObject missile) {
		_isRocketBarrage = true;
		for (int i = 0; i < rocketsNumber; i++) {
			GameObject go = Instantiate (missile, rocketLauncher_1.position, rocketLauncher_1.rotation) as GameObject;
			go.GetComponent<Mover> ().Move (missile.transform.forward);

			go = Instantiate (missile, rocketLauncher_2.position, rocketLauncher_2.rotation) as GameObject;
			go.GetComponent<Mover> ().Move (missile.transform.forward);
			yield return new WaitForSeconds (nextRocket);
		}
		_isRocketBarrage = false;
	}


}
