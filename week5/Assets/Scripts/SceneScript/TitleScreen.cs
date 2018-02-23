using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : Scene<TransitionData> {

    bool P1Ready;
    bool P2Ready;

    public GameObject P1readytext;
    public GameObject P2readytext;

    bool ready;

	void Start()
	{
        ready = false;
        P1Ready = false;
        P2Ready = false;
        StartCoroutine(WaitForIt());
	}

	// Update is called once per frame
	void Update()
	{
        if (ready)
        {
            if (Input.GetAxis("P1_Grab") > 0)
            {
                if (!P1Ready)
                {
                    P1Ready = true;
                    P2readytext.SetActive(true);
                }
            }
            if (Input.GetAxis("P2_Grab") > 0)
            {
                if (!P2Ready)
                {
                    P2Ready = true;
                    P1readytext.SetActive(true);
                }
            }

            if (P1Ready && P2Ready)
            {
                StartCoroutine(WaitToStartTheGame());
            }
        }

    }
    IEnumerator WaitForIt(){
        yield return new WaitForSeconds(0.5f);
        ready = true;

    }

    IEnumerator WaitToStartTheGame(){
        yield return new WaitForSeconds(2f);
        StartGame();
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
