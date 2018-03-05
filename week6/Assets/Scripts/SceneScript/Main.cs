using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : Scene<TransitionData> {


    //public Line line;


    public GameObject player1WinText, player2WinText;

	// Use this for initialization
	void Start () {

        //line.CreateSpaces();

        Services.GameManager.audioController.bgm.Play();
	}
	
	// Update is called once per frame
    void Update(){
        Services.GameManager.GetComponentInChildren<Line>().LineUpdate();
    }
	void FixedUpdate () {
        //line.LineUpdate();
        Services.GameManager.GetComponentInChildren<Line>().LineFixedUpdate();
	}

	void InitializeServices()
	{
		Services.Main = this;
	}

	internal override void OnEnter(TransitionData data)
	{
		InitializeServices();
		Services.GameManager.currentCamera = GetComponentInChildren<Camera>();

	}


    public void Restart(){
        Services.SceneStackManager.PopScene();
        Services.SceneStackManager.PushScene<Main>();
    }

    public void MainMenu(){

        Services.SceneStackManager.Swap<TitleScreen>();

    }

}
