using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedBonus : MonoBehaviour {

	public float workTime;
	public float speedBonus;

	private Text bonusTimer;

	private float bonusCurWorkTime = 0.0f;

	private GameObject _go;

	private PlayerController _pc;

	private bool _isBurst;

	private static SpeedBonus _speedBonus = null;

	private GameController _gc;

	void Awake () {
		if (_speedBonus == null) {
			_speedBonus = this;
		} else if (_speedBonus != this) {
			Destroy (_speedBonus.gameObject);
			_speedBonus = this;
		}
		_go = GameObject.FindGameObjectWithTag ("Bonus_Speed");
		bonusTimer = _go.GetComponent<Text> ();
		bonusTimer.text = string.Empty;
		_pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
		_isBurst = false;
		_gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		_gc.ActivateBuffIcon ("speed");
	}

	void Update() {
		if (workTime > bonusCurWorkTime) {
			string counter = (workTime - bonusCurWorkTime).ToString ();
			if (counter.Length > 3) {
				counter = counter.Substring(0,3);
			}
			bonusTimer.text = counter;
			bonusCurWorkTime += Time.deltaTime;
			if (!_isBurst && _pc.speed == 5.0f) {
				_pc.speed = _pc.speed + speedBonus;
				_isBurst = true;
			}
		} else {
			bonusTimer.text = string.Empty;
			_pc.speed = _pc.speed - speedBonus;
			_isBurst = false;
			_gc.DeactivateBuffIcon ("speed");
			Destroy (gameObject);
		}

	}
}
