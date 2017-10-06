using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

	public float coolDown;

	void Start () {
		coolDown += Time.time;  
	}
	
	void Update () {
		if (coolDown == 0) {
			return;
		}
		if (Time.time < coolDown){
			
		}
	}
}
