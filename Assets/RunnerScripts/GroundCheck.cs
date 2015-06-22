using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GroundCheck : MonoBehaviour {
	bool isfalling = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if( !Physics.Linecast( transform.position + Vector3.up, transform.position ) && !isfalling )
		{
			isfalling = true;
			GetComponent<RunnerCtr>().enabled = false;
			transform.DORotate( new Vector3(0,180,0),2 ).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
			//GetComponent<Animator>().speed = 0;
			Camera.main.transform.DOLocalMove( new Vector3(0,100,0), 5 );
			Camera.main.transform.DORotate( new Vector3( 90, 0, 0 ),1);
		}
		if(isfalling) 
			transform.Translate (-transform.up.normalized  * Time.deltaTime * 10,Space.World);
	}
}
