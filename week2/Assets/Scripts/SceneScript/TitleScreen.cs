using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleScreen : Scene<TransitionData> {

	void Start()
	{

        GameObject.FindWithTag("Fade").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GameObject.FindWithTag("Fade").GetComponent<Image>().DOFade(0f, 1f);
	}

	// Update is called once per frame
	void Update()
	{
        if(Input.GetMouseButtonDown(0)){
            StartGame();
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
