using UnityEngine;
using System.Collections;

public class EnviromentCtr : MonoBehaviour {
	public float speed;
	public Transform[] tiles ;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform t in tiles) {
			transform.Translate (Vector3.forward * Time.deltaTime * speed);
		}
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		if (!GeometryUtility.TestPlanesAABB (planes, tiles [0].GetComponentInChildren<MeshRenderer> ().bounds)) {
			tiles[0].position = tiles[1].position + Vector3.forward * -500;
			int dirIdx = Random.Range(0,3);
			if(dirIdx==1) 
				tiles[0].Rotate( new Vector3(0,90,0) );
			else if(dirIdx==2) 
				tiles[0].Rotate( new Vector3(0,-90,0) );
		}

		if (!GeometryUtility.TestPlanesAABB (planes, tiles [1].GetComponentInChildren<MeshRenderer> ().bounds)) {
			tiles[1].position = tiles[0].position + Vector3.forward * -500;
			int dirIdx = Random.Range(0,3);
			if(dirIdx==1) 
				tiles[1].Rotate( new Vector3(0,90,0) );
			else if(dirIdx==2) 
				tiles[1].Rotate( new Vector3(0,-90,0) );
		}
		

	}
}
