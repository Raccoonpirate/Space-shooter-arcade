using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireRateBonus : MonoBehaviour {

	public float workTime;
	public float fireRateBonus;

	private Text bonusTimer;

	private float bonusCurWorkTime = 0.0f;

	private GameObject _go;

	private PlayerController _pc;

	private bool _isBurst;

	private static FireRateBonus _fireRateBonus = null;

	private GameController _gc;

	void Awake () {
		if (_fireRateBonus == null) {
			_fireRateBonus = this;
		} else if (_fireRateBonus != this) {
			Destroy (_fireRateBonus.gameObject);
			_fireRateBonus = this;
		}
		_go = GameObject.FindGameObjectWithTag ("Bonus_FireRate");
		bonusTimer = _go.GetComponent<Text> ();
		bonusTimer.text = string.Empty;
		_pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController> ();
		_isBurst = false;
		_gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		_gc.ActivateBuffIcon ("firerate");
	}
	
	void Update () {
		if (workTime > bonusCurWorkTime) {
			string counter = (workTime - bonusCurWorkTime).ToString ();
			if (counter.Length > 3) {
				counter = counter.Substring(0,3);
			}
			bonusTimer.text = counter;
			bonusCurWorkTime += Time.deltaTime;
			if (!_isBurst && _pc.fireRate == 0.25f) {
				_pc.fireRate = _pc.fireRate + fireRateBonus;
				_isBurst = true;
			}
		} else {
			bonusTimer.text = string.Empty;
			_pc.fireRate = _pc.fireRate - fireRateBonus;
			_isBurst = false;
			_gc.DeactivateBuffIcon ("firerate");
			Destroy (gameObject);
		}
	}
}
