using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RunnerCtr : MonoBehaviour {
	public float speed;
	public Vector3 direction = Vector3.forward;
	public float offsetXTime;
	public float offsetYTime;
	public bool inTurnZone = false;
	public GameObject nextTile;
	public GameObject currentTile;
	public GameObject modelY;
	float midValue = 0;
	float turnTimer = 0;
	float jumpTimer = 0;
	float turnAngle = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		turnTimer -= Time.deltaTime;
		jumpTimer -= Time.deltaTime;
			

		transform.Translate (transform.forward.normalized  * Time.deltaTime * speed,Space.World);

		if ( currentTile.name == "Forward" || currentTile.name == "Back") {
			if( transform.position.x - currentTile.transform.position.x > 0.2f ) {
				transform.position = transform.position - Vector3.right * Time.deltaTime * speed/6;
			}
			else
				transform.position = transform.position + Vector3.right * Time.deltaTime * speed/6;

		}
		else if (currentTile.name == "Left" || currentTile.name == "Right") {
			if( transform.position.z - currentTile.transform.position.z > 0.2f ) {
				transform.position = transform.position - Vector3.forward * Time.deltaTime * speed/6;
			}
			else
				transform.position = transform.position + Vector3.forward * Time.deltaTime * speed/6;
		}

		if( Input.GetKeyDown(KeyCode.UpArrow) && jumpTimer <= 0) {
			Sequence seq = DOTween.Sequence();
			seq.Append( modelY.transform.DOLocalMoveY(8 , offsetYTime ).SetEase(Ease.Linear) );
			seq.Append( modelY.transform.DOLocalMoveY(0 , offsetYTime ).SetEase(Ease.Linear) );
			jumpTimer = offsetYTime * 2;
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow) && !inTurnZone && turnTimer<=0) {
			transform.DOMove( ( transform.position + transform.right * -5 ) + transform.forward  * offsetXTime * speed , offsetXTime).SetEase(Ease.Linear); 
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow) && !inTurnZone && turnTimer<=0) {
			transform.DOMove( ( transform.position + transform.right * 5 ) + transform.forward  * offsetXTime * speed , offsetXTime).SetEase(Ease.Linear); 
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow) && inTurnZone && turnTimer<=0) {

			transform.DORotate( transform.eulerAngles + new Vector3(0,-90,0),0.05f  );

			turnTimer = Vector3.Distance( nextTile.transform.FindChild("Mid").position,transform.position ) / speed;
			if( Mathf.Approximately(turnAngle,90) )
				transform.DOMove( nextTile.transform.FindChild("Mid").position, turnTimer ).SetEase(Ease.Linear); 
			inTurnZone = false;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow) && inTurnZone && turnTimer<=0) {
			transform.DORotate( transform.eulerAngles + new Vector3(0,90,0),0.05f );
			turnTimer = Vector3.Distance( nextTile.transform.FindChild("Mid").position,transform.position ) / speed;
			if( Mathf.Approximately(turnAngle,-90) )
				transform.DOMove( nextTile.transform.FindChild("Mid").position, turnTimer ).SetEase(Ease.Linear); 
			inTurnZone = false;
		}

	}

	public void ChangeTile() {
		Vector3 v1 = nextTile.transform.FindChild ("Mid").position    - nextTile.transform.position;
		Vector3 v2 = currentTile.transform.FindChild ("Mid").position - currentTile.transform.position;
		turnAngle = Vector3.Angle ( v1, v2 ) * Mathf.Sign(Vector3.Cross(v1, v2).y) ;
		currentTile = nextTile;
	}
}
