using UnityEngine;
using System.Collections;

public class TurnZone : MonoBehaviour {
	public bool turnable;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		other.gameObject.GetComponent<RunnerCtr> ().ChangeTile ();
		if(turnable)
			other.gameObject.GetComponent<RunnerCtr> ().inTurnZone = true;
	}
}
