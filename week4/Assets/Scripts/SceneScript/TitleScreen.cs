using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : Scene<TransitionData> {

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

    }
    void InitializeServices()
    {
        Services.TitleScreen = this;
    }

    internal override void OnEnter(TransitionData data)
    {
        InitializeServices();
        Services.GameManager.currentCamera = GetComponentInChildren<Camera>();

    }



    public void StartGame(){
        Services.SceneStackManager.Swap<Main>();
    }
}
