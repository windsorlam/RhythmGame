using UnityEngine;
using System.Collections;

public enum TileDir
{
	Center,
	Back,
	Forward,
	Left,
	Right
}

public class GenTiles : MonoBehaviour {
	public GameObject[] tiles;
	public TileDir parentDir;

	Vector3 runnerDir;
	RunnerCtr runner;
	// Use this for initialization
	void Start () {
		if(parentDir==TileDir.Back)
			runnerDir = - Vector3.forward;
		else if(parentDir==TileDir.Forward)
			runnerDir =  Vector3.forward;
		else if(parentDir==TileDir.Left)
			runnerDir =  Vector3.left;
		else if(parentDir==TileDir.Right)
			runnerDir = - Vector3.left;
		runner = GameObject.FindGameObjectWithTag ("Player").GetComponent<RunnerCtr>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter() {
		TileDir nextDir = (TileDir)Random.Range (1, 5);
		if(parentDir==TileDir.Back) {
			while(nextDir==TileDir.Forward)
				nextDir = (TileDir)Random.Range (1, 5);
		}
		else if(parentDir==TileDir.Forward) {
			while(nextDir==TileDir.Back)
				nextDir = (TileDir)Random.Range (1, 5);
		}
		else if(parentDir==TileDir.Left) {
			while(nextDir==TileDir.Right)
				nextDir = (TileDir)Random.Range (1, 5);
		}
		else if(parentDir==TileDir.Right) {
			while(nextDir==TileDir.Left)
				nextDir = (TileDir)Random.Range (1, 5);
		}

		GameObject centerTile = Instantiate(tiles[ (int)TileDir.Center ]) as GameObject;
		GameObject nextTile   = Instantiate(tiles[ (int)nextDir ]) as GameObject;
		if(nextDir==TileDir.Back) 
			nextTile.name = "Back";
		else if(nextDir==TileDir.Forward) 
			nextTile.name = "Forward";
		else if(nextDir==TileDir.Left) 
			nextTile.name = "Left";
		else if(nextDir==TileDir.Right) 
			nextTile.name = "Right";

		runner.nextTile = nextTile;
		centerTile.transform.position = transform.position + runnerDir * (125+7);
		if(nextDir==TileDir.Back) 
			nextTile.transform.position = centerTile.transform.position - Vector3.forward*7;
		else if(nextDir==TileDir.Forward) 
			nextTile.transform.position = centerTile.transform.position + Vector3.forward*7;
		else if(nextDir==TileDir.Left) 
			nextTile.transform.position = centerTile.transform.position + Vector3.left*7;
		else if(nextDir==TileDir.Right) 
			nextTile.transform.position = centerTile.transform.position - Vector3.left*7;

		if (nextDir != parentDir) {
			centerTile.GetComponentInChildren<TurnZone>().turnable = true;		
		}
	}
}
