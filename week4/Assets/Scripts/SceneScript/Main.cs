using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Main : Scene<TransitionData> {

    public GameObject ScrollingObj;
    public ChatManager chatManager;
    public DialogueRunner dialogue;

    public ScoreManager score;
    public GameObject result;

    public GameObject panel;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
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
