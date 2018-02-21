using UnityEngine;
using System.Collections;

public class InnerGameManager : MonoBehaviour {

	public GameObject InnerPlayer;

	//prefabs innergame
	/*
	public GameObject ScoreManagerPrefab;
	public GameObject SpawnerPrefab;
	public GameObject WallsPrefab;
	public GameObject directionsPrefab;
	*/

	GameObject ScoreManager;
	GameObject Spawner;
	//GameObject Walls;



	// Use this for initialization
	void Start () {
        startGame();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void startGame(){
		InnerPlayer.GetComponent<SpriteRenderer> ().enabled = true;
        Debug.Log("hello");
		//InnerPlayer.transform.position = new Vector2(0f,InnerPlayer.transform.position.y);

        //Spawner = Instantiate (Services.Prefabs.SpawnerPrefab);
		/*Walls = Instantiate (Services.Prefabs.WallsPrefab, 
                             Services.Prefabs.WallsPrefab.transform.position,
                             Services.Prefabs.WallsPrefab.transform.rotation);*/

		
	}


}
