using UnityEngine;
using UnityEngine.UI;

public class RecordShow : MonoBehaviour {

	public Text record;

	void Start () {
		record.text = "Your record: " + PlayerPrefs.GetInt ("InfinityModeRecord").ToString ();
	}
}
