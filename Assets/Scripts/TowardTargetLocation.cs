using UnityEngine;
using System.Collections;

public class TowardTargetLocation : MonoBehaviour {

	public float speed;

	private GameObject _player;

	private Vector3 _direction;

	private bool _targetSet;

	void Start () {
		_targetSet = false;
	}

	void Update () {
		if (!_targetSet) {
			_player = GameObject.FindGameObjectWithTag ("Player");
			if (_player != null) {
				transform.LookAt (_player.transform);
				_direction = _player.transform.position - transform.position;
				_direction.Normalize ();
				GetComponent<Mover> ().Move (_direction * speed);
				_targetSet = true;
			}
		}
	}

}
