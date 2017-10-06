using UnityEngine;
//using UnityEngine.Networking;
using System.Collections;

//[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
//public class PlayerController : NetworkBehaviour {
		
	public AudioClip audioClip;

	public float speed;
	public float tilt;
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;
	public float fireRate;

	private float _nextFire = 0.0f;

	public GameObject shot;
	public Transform shotSpawn;

	Rigidbody rigBody;

	void Awake () {
		rigBody = GetComponent<Rigidbody> ();
	}

	void Update () {
		/*if (!isLocalPlayer) {
			return;
		}*/

		if (Time.time > _nextFire) {
			//_nextFire = Time.time + fireRate;
			//CmdFire ();
			_nextFire = Time.time + fireRate;
			GameObject go = Instantiate (shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
			go.GetComponent<Mover> ().Move (shot.transform.forward);
			AudioSource.PlayClipAtPoint (audioClip, transform.position, 0.6f);
		}

		//Vector3 movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0.0f );

		//transform.Translate(speed * Time.deltaTime * movement);
		//transform.position = new Vector3 (Mathf.Clamp(transform.position.x, xMin, xMax), 0.0f, Mathf.Clamp(transform.position.z, zMin, zMax));
	}

	/*[Command]
	void CmdFire(){
		GameObject go = Instantiate (shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
		go.GetComponent<Mover> ().Move (shot.transform.forward);
		AudioSource.PlayClipAtPoint (audioClip, transform.position, 0.9f);
		NetworkServer.Spawn (go);
	}*/

	void FixedUpdate() {
		/*if (!isLocalPlayer) {
			return;
		}*/

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigBody.velocity = movement * speed;

		rigBody.position = new Vector3 (Mathf.Clamp(rigBody.position.x, xMin, xMax), 0.0f, Mathf.Clamp(rigBody.position.z, zMin, zMax));

		rigBody.rotation = Quaternion.Euler (90f + (rigBody.velocity.x * -tilt) , -90.0f, -90.0f);
	}

	/*public override void OnStartLocalPlayer () {
		//GetComponent<MeshRenderer> ().material.color = Color.blue;
		GetComponentInChildren<TextMesh>().text = "Me";
		//transform.rotation = Quaternion.Euler (180.0f, 180.0f, 180.0f);
	}*/
}
