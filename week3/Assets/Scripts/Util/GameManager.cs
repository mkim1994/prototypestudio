using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    public Camera currentCamera;
	void Awake()
	{
		InitializeServices();
	}

	// Use this for initialization
	void Start()
	{
        //Services.EventManager.Register<Reset>(Reset);
        //Services.SceneStackManager.PushScene<TitleScreen>();
        DOTween.Init();
	}

	// Update is called once per frame
	void Update()
	{
		Services.TaskManager.Update();

        if(Input.GetKeyUp(KeyCode.Escape)){
            Application.Quit();
        }

        if(Input.GetKeyUp(KeyCode.R)){
          //  Services.SceneStackManager.Swap<TitleScreen>();
            SceneManager.LoadScene("main");
        }
	}

	void InitializeServices()
	{
		Services.GameManager = this;
		Services.EventManager = new EventManager();
		Services.TaskManager = new TaskManager();
		Services.Prefabs = Resources.Load<PrefabDB>("Prefabs/Prefabs");
        Services.Materials = Resources.Load<MaterialDB>("Art/Materials");
        Services.InputManager = new InputManager();


	}

	void Reset(Reset e)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    //UI buttons

}
