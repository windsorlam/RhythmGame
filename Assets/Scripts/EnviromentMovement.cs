using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

public class EnviromentMovement : MonoBehaviour {
	public float speed;
	public GameObject[] tiles;
	public Texture[] gestureTextures;
	public GameObject blockCube;
	public float distance;
	public Text scoreText;
	public AudioClip  explosion;
	int score;
	ExploderObject exploder;
	int currentTile = 0;
	float currentStart = 0;
	int currentGesture = 0;
	// Use this for initialization
	void Start () {
		CreateBlock ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		if(transform.position.z - currentStart > 490) {
			currentStart = transform.position.z;
			tiles[currentTile].transform.localPosition += new Vector3(0,0,-490*tiles.Length);
			currentTile++;
			if(currentTile==tiles.Length)
			currentTile = 0;
		}
	}

	public void OnGestureClick() {
		if (EventSystem.current.currentSelectedGameObject.name == (currentGesture + 1).ToString ()) {
			exploder.Explode();
			score ++;
			scoreText.text = "Score:" + score.ToString();
			Camera.main.GetComponent<AudioSource>().PlayOneShot(explosion);
			CreateBlock ();
		}
	}

	void CreateBlock() {
		blockCube = Instantiate(blockCube) as GameObject;
		blockCube.name = "Block";
		exploder = blockCube.GetComponent<ExploderObject>();
		blockCube.SetActive(true);
		blockCube.transform.position = new Vector3(0,4,-distance);
		blockCube.transform.parent = transform;
		blockCube.transform.DOLocalRotate (new Vector3 (0, 360, 0), 3).SetRelative ().SetLoops (-1, LoopType.Yoyo);
		currentGesture = Random.Range (0, gestureTextures.Length);
		blockCube.GetComponent<MeshRenderer> ().material.mainTexture = gestureTextures [currentGesture];
	}
	
}
