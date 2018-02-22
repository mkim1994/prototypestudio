using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Main : Scene<TransitionData> {

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winnerText;

    public GameObject CountDown;

    [HideInInspector]
    public bool gameStarted;

    public PlayManager playManager;
    public GameObject CanadaClothes, USAClothes;

    public float canadaThreshold, usaThreshold;

    public Transform headPos, lefthandPos, righthandPos, legPos, bodyPos, leftfootPos, rightfootPos, neckPos;

	// Use this for initialization
	void Start () {
        gameStarted = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void StartGame(){
        
        timerText.gameObject.SetActive(true);
        playManager.StartGame();
        gameStarted = true;
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
