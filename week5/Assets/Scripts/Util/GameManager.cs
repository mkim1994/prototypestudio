using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject sceneRoot;
    public Camera currentCamera;
    public GameObject TestingScenes;
    public FadeCanvas fadeCavnas;

	void Awake()
	{
        TestingScenes.SetActive(false);
		InitializeServices();
	}

	// Use this for initialization
	void Start()
	{
		//Services.EventManager.Register<Reset>(Reset);
        fadeCavnas.Fade(true,2f);
		//Services.SceneStackManager.PushScene<TitleScreen>();
        Services.SceneStackManager.PushScene<Intro>();
	}

	// Update is called once per frame
	void Update()
	{
		Services.TaskManager.Update();

        if(Input.GetKeyUp(KeyCode.Escape)){
            Application.Quit();
        }

        if(Input.GetKeyUp(KeyCode.R)){
           // Services.SceneStackManager.Swap<TitleScreen>();
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
        Services.Sprites = Resources.Load<SpriteDB>("Sprites/Sprites");
		Services.SceneStackManager = new SceneStackManager<TransitionData>(sceneRoot, Services.Prefabs.Scenes);
		Services.InputManager = new InputManager();


	}

	void Reset(Reset e)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    //UI buttons

}
