using UnityEngine;
using System.Collections;

public class ZigZagMovement : MonoBehaviour {

	public float zigZagSpeed;

	private float _constPosSpeed;

	void Start () {
		//т.к. zigZagSpeed указывает направление начала куда лететь положительное - влево, отрицательное - вправо,
		//то надо как-то было сделать чтобы я мог начинать в любую сторону, но дальше все шло своим чередом
		_constPosSpeed = Mathf.Abs (zigZagSpeed);
		GetComponent<Mover> ().Move (new Vector3(zigZagSpeed, 0, _constPosSpeed));
		zigZagSpeed = Mathf.Abs (zigZagSpeed);
	}
	
	void FixedUpdate () {
		if (transform.position.x <= -6f) {
			//Debug.Log ("Move right");
			GetComponent<Mover> ().Move(new Vector3(-zigZagSpeed, 0, _constPosSpeed));
		}
		if (transform.position.x >= 6f) {
			//Debug.Log ("Move left");
			GetComponent<Mover> ().Move(new Vector3(zigZagSpeed, 0, _constPosSpeed));
		}
	}
}
