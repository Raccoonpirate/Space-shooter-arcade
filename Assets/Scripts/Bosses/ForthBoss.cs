using UnityEngine;
using System.Collections;

public class ForthBoss : MonoBehaviour {

	public GameObject rocket;
	public GameObject intersepter;

	public Transform rocketLuncher_1;
	public Transform rocketLuncher_2;
	public Transform rocketLuncher_3;
	public Transform leftAngar;
	public Transform rightAngar;

	public float rocketLuncher_1_fireRate;
	public float rocketLuncher_2_fireRate;
	public float rocketLuncher_3_fireRate;
	public float leftAngarSpawnRate;
	public float rightAngarSpawnRate;

	private float rocketLuncher_1_NextFire;
	private float rocketLuncher_2_NextFire;
	private float rocketLuncher_3_NextFire;
	private float leftAngarNextSpawn;
	private float rightAngarNextSpawn;

	private bool _onPosition;

	void Start () {
		_onPosition = false;
		leftAngarNextSpawn = leftAngarSpawnRate;
		rightAngarNextSpawn = rightAngarSpawnRate;
	}
	
	void Update () {
		if (_onPosition) {
			if (Time.time > rocketLuncher_1_NextFire) {
				rocketLuncher_1_NextFire = Time.time + rocketLuncher_1_fireRate;
				SpawnObject (rocketLuncher_1, rocket, 1.0f);
			}
			if (Time.time > rocketLuncher_2_NextFire) {
				rocketLuncher_2_NextFire = Time.time + rocketLuncher_2_fireRate;
				SpawnObject (rocketLuncher_2, rocket, 1.0f);
			}
			if (Time.time > rocketLuncher_3_NextFire) {
				rocketLuncher_3_NextFire = Time.time + rocketLuncher_3_fireRate;
				SpawnObject (rocketLuncher_3, rocket, 1.0f);
			}
			if (Time.time > leftAngarNextSpawn) {
				leftAngarNextSpawn = Time.time + leftAngarSpawnRate;
				SpawnObject (leftAngar, intersepter, 1.0f);
			}
			if (Time.time > rightAngarNextSpawn) {
				rightAngarNextSpawn = Time.time + rightAngarSpawnRate;
				SpawnObject (rightAngar, intersepter);
			}
		}
	}

	void SpawnObject (Transform point, GameObject pojective, float dir){
		GameObject go = Instantiate (pojective, point.position, point.rotation) as GameObject;
		go.GetComponent<Mover> ().Move (pojective.transform.forward * dir);
	}

	void SpawnObject (Transform point, GameObject pojective){
		GameObject go = Instantiate (pojective, point.position, point.rotation) as GameObject;
		go.GetComponent<Mover> ().Move (pojective.transform.forward);
		go.GetComponent<ZigZagMovement> ().zigZagSpeed *= -1;
	}

	void FixedUpdate () {
		if (transform.position.z <= 8.5f && !_onPosition) {
			_onPosition = true;
			GetComponent<Mover> ().Move(Vector3.zero);
		}
	}

}
