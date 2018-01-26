using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : Scene<TransitionData> {

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

    }
    void InitializeServices()
    {
        Services.EndScreen = this;
    }

    internal override void OnEnter(TransitionData data)
    {
        InitializeServices();
        Services.GameManager.currentCamera = GetComponentInChildren<Camera>();

    }



    public void StartGame(){
       // Services.SceneStackManager.Swap<TitleScreen>();
        SceneManager.LoadScene("main");
    }
}
