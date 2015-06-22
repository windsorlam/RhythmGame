using UnityEngine;
using System.Collections;

public class DeleteSelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBecameInvisible() {

		StartCoroutine (DestroySelf ());

	}


	IEnumerator DestroySelf() {
		yield return new WaitForSeconds (2);
		if( GetComponent<MeshRenderer>().isVisible == false ) {
			if( transform.parent!=null) 
				Destroy (transform.parent.gameObject);
			else 
				Destroy (transform.gameObject);
		}
	}
}
