using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : Scene<TransitionData> {

    public Text p1rdy, p2rdy;
    public Text directions, go;
    private bool p1isrdy, p2isrdy;
    private bool isStarting;

	void Start()
	{
        p1isrdy = false;
        p2isrdy = false;
        isStarting = false;
	}

	// Update is called once per frame
	void Update()
	{

        if (!isStarting && p1isrdy && p2isrdy)
        {
            isStarting = true;
            StartCoroutine(WaitToStartGame());
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!p1isrdy)
                {
                    Services.GameManager.audioController.enemy2Run[0].Play();
                    p1rdy.text = "READY";
                    p1isrdy = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!p2isrdy)
                {
                    Services.GameManager.audioController.enemy1Run[0].Play();
                    p2rdy.text = "READY";
                    p2isrdy = true;
                }
            }
        }


    }
    IEnumerator WaitToStartGame(){
        yield return new WaitForSeconds(1f);
        p1rdy.gameObject.SetActive(false);
        p2rdy.gameObject.SetActive(false);
        directions.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        Services.GameManager.audioController.starting.Play();
        Services.GameManager.GetComponentInChildren<Line>().CreateFake();
        yield return new WaitForSeconds(2f);
        directions.gameObject.SetActive(false);
        go.gameObject.SetActive(true);
        Services.GameManager.audioController.go.Play();
        yield return new WaitForSeconds(1f);
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
