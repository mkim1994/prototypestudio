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



    public void StartGame(int n){
        if(n == 0){
            Services.GameManager.dudeBathroom = true;
        } else{
            Services.GameManager.dudeBathroom = false;
        }
        Services.SceneStackManager.Swap<Main>();
    }
}
