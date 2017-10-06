using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {

	public float tumble;

	Rigidbody rigBody;

	void Awake () {
		rigBody = GetComponent <Rigidbody> ();
	}

	void Start () {
		rigBody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
