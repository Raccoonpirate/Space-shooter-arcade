using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldBonus : MonoBehaviour {

	public float workTime;

	private Text bonusTimer;

	private float bonusCurWorkTime = 0.0f;

	private GameObject _go;

	private static ShieldBonus _shieldBonus = null;

	private GameController _gc;

	void Awake () {
		if (_shieldBonus == null) {
			_shieldBonus = this;
		} else if (_shieldBonus != this) {
			Destroy (_shieldBonus.gameObject);
			_shieldBonus = this;
		}
		_go = GameObject.FindGameObjectWithTag ("Bonus_Shield");
		bonusTimer = _go.GetComponent<Text> ();
		bonusTimer.text = string.Empty;
		_gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		_gc.ActivateBuffIcon ("shield");
	}
	
	void Update () {
		if (workTime > bonusCurWorkTime) {
			string counter = (workTime - bonusCurWorkTime).ToString ();
			if (counter.Length > 3) {
				counter = counter.Substring(0,3);
			}
			bonusTimer.text = counter;
			bonusCurWorkTime += Time.deltaTime;
		} else {
			bonusTimer.text = string.Empty;
			_gc.DeactivateBuffIcon ("shield");
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player" || other.tag == "Player_shot" || other.tag == "Bonus"|| other.tag == "Boundary") {
			return;
		}
		if (other.name == "MegaCanon_shot") {
			bonusTimer.text = string.Empty;
			_gc.DeactivateBuffIcon ("shield");
			Destroy (gameObject);
			return;
		}
		Destroy (other.gameObject);
	}


}
