using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	//public Text scoretext;

	public Text scoretext;

	public int score;

	// Use this for initialization
	void Start () {
		score = 0;

	}
	
	// Update is called once per frame
	void Update () {
		scoretext.text = "SCORE: " + score.ToString ();
	}

	public void EnemyTouched(){
		score -= 10;
	}

	public void EnemyHurt(){
		score -= 30;
	}

	public void AllyTouched(){
		score += 20;
	}

	public void AllyHurt(){
		//score -= 10;
		score += 0;
	}

	public void AllyGround(){
		//score -= 10;
		score += 0;
	}
}
