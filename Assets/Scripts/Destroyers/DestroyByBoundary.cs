using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit (Collider other) {
		//Debug.Log ("object " + other.name + " destroyed by boundary");
		Destroy (other.gameObject);
	}
}