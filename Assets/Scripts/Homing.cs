using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour {

	public float speed;
	public float rotationSpeed;

	public GameObject target;

	Rigidbody rigBody;

	void Start () {
		rigBody = GetComponent<Rigidbody> ();
		target = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if (target == null) {
			Destroy (gameObject);
		}
	}

	void FixedUpdate () {
		if (target != null) {
			rigBody.velocity = transform.forward * speed;
			Quaternion targetRotation = Quaternion.LookRotation (target.transform.position - transform.position);
			var angles = transform.rotation.eulerAngles;
			angles.x = 0;
			angles.z = 0;
			transform.rotation = Quaternion.Euler (angles);
			rigBody.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, rotationSpeed));
		}
	}

}
