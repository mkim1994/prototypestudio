using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Main4 : Scene<TransitionData> {

	// Use this for initialization
	void Start () {

        GameObject.FindWithTag("Fade").GetComponent<Image>().DOFade(0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void InitializeServices()
	{
		Services.Main4 = this;
	}

	internal override void OnEnter(TransitionData data)
	{
		InitializeServices();
		Services.GameManager.currentCamera = GetComponentInChildren<Camera>();

	}


    public void Restart(){
        Services.SceneStackManager.PopScene();
        Services.SceneStackManager.PushScene<Main2>();
    }

    public void MainMenu(){

        Services.SceneStackManager.Swap<TitleScreen>();

    }

}
