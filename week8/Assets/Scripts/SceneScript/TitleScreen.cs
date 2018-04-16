using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : Scene<TransitionData> {

    bool starting;
	void Start()
	{
        starting = false;
	}

	// Update is called once per frame
	void Update()
	{
        if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f){
            if (!starting)
            {
                starting = true;
                Services.GameManager.fadeCanvas.Fade(false, 1f);
            }
        }

        if(starting){
            if(Services.GameManager.fadeCanvas.FadeOutComplete()){
                StartGame();
            }
        }

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
