using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TileTrigger : MonoBehaviour {
	public RunnerCtr runCtr;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.LeftArrow) && runCtr.inTurnZone ) {
	}

	void OnTriggerEnter(Collider other) {
		runCtr.inTurnZone = true;
		transform.parent.DORotate( new Vector3(0,0,0), 0.5f);
	}


}
