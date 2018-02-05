using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

	public GameObject sceneRoot;
    public Camera currentCamera;
    public GameObject TestingScenes;
	void Awake()
	{
        TestingScenes.SetActive(false);
		InitializeServices();
	}

	// Use this for initialization
	void Start()
	{
        DOTween.Init();
        Cursor.lockState = CursorLockMode.Locked;
		//Services.EventManager.Register<Reset>(Reset);
		Services.SceneStackManager.PushScene<TitleScreen>();
        GameObject.FindWithTag("Fade").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GameObject.FindWithTag("Fade").GetComponent<Image>().DOFade(0f, 1f);
	}

	// Update is called once per frame
	void Update()
	{
		Services.TaskManager.Update();

        if(Input.GetKeyUp(KeyCode.Escape)){
            Application.Quit();
        }

        if(Input.GetKeyUp(KeyCode.R)){
            Services.SceneStackManager.Swap<TitleScreen>();
        }
	}

	void InitializeServices()
	{
		Services.GameManager = this;
		Services.EventManager = new EventManager();
		Services.TaskManager = new TaskManager();
		Services.Prefabs = Resources.Load<PrefabDB>("Prefabs/Prefabs");
        Services.Materials = Resources.Load<MaterialDB>("Art/Materials");
		Services.SceneStackManager = new SceneStackManager<TransitionData>(sceneRoot, Services.Prefabs.Scenes);
		Services.InputManager = new InputManager();


	}

	void Reset(Reset e)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    //UI buttons

}
