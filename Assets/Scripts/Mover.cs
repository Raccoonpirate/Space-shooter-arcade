using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;

	private Vector3 _direction;

	Rigidbody rigBody;

	void Awake () {
		rigBody = GetComponent<Rigidbody> ();
		//rigBody.collisionDetectionMode detectCollisions = false;

	}

	void Update ()  {
		//transform.Translate(speed * Time.deltaTime * _direction);
	}

	public void Move (Vector3 direction) {
		rigBody.velocity = direction * speed;
		//transform.position = direction * speed;
		//_direction = direction;
		//transform.position = new Vector3 (Mathf.Clamp(transform.position.x, 0.0f, 0.0f), 0.0f, Mathf.Clamp(transform.position.z, 0.0f, 0.0f));
	}

	public Vector3 GetPrevVelocity() {
		return rigBody.velocity;
	}

}
