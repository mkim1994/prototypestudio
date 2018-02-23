using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : Scene<TransitionData> {
    
	void Start()
    {

	}

	// Update is called once per frame
	void Update()
	{
        if (((Input.GetAxis("P1_Grab") > 0 || Input.GetAxis("P2_Grab") > 0) ||
             Input.anyKeyDown) && !(Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape))){
            Services.SceneStackManager.Swap<TitleScreen>();
        }
    }

    void InitializeServices()
    {
        Services.Intro = this;
    }

    internal override void OnEnter(TransitionData data)
    {
        InitializeServices();
        Services.GameManager.currentCamera = GetComponentInChildren<Camera>();

    }



}
