using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.AI;
using Shuffler;

public class GameManager : MonoBehaviour {

    public float radius = 13f;
    public List<string> words = new List<string>{
        "FAMILY","FRIENDS","WORK","RENT","HOBBIES","ONLINE","PERSONA","POLITICS","BOOK","WEEKEND",
        "SEXUALITY","GENDER","BOOK","SCHEDULE","DINNER","ETHNICITY","LAUNDRY","GROCERIES",
        "SLEEP","ILLNESS","LOVE","PAST","FUTURE","BODY","SKILL","TALENT"
    };
    public Camera currentCamera;

    public Transform intrusivePos;
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

        int intrusive = Random.Range(0, words.Count);

        /* spawn thoughts and stuff */
        for (int i = 0; i < words.Count; ++i){ //save one for the intrusive thought

            if (i != intrusive)
            {
                Vector3 randomDirection = Random.insideUnitSphere * radius;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas);
                Vector3 finalPosition = hit.position;

                GameObject thought = Instantiate(Services.Prefabs.Thought, finalPosition, Quaternion.identity);

                thought.GetComponentInChildren<CurveWord>().word = words[i];
                thought.GetComponentInChildren<CurveWord>().InitializeWord();
            } else{
                GameObject hidden = Instantiate(Services.Prefabs.HiddenThought, intrusivePos.position, Quaternion.identity);
                hidden.GetComponentsInChildren<CurveWord>()[0].word = words[i];
                hidden.GetComponentsInChildren<CurveWord>()[0].InitializeWord();
                hidden.GetComponentsInChildren<CurveWord>()[1].InitializeWord();
            }
        }


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
            SceneManager.LoadScene("titlescreen");
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
