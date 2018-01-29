using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : Scene<TransitionData> {

    public GameObject DateFailText;
    public GameObject DateSuccessText;
	void Start()
	{
        if(Services.GameManager.dateSuccess){
            DateSuccessText.SetActive(true);
            DateFailText.SetActive(false);
        } else{
            DateSuccessText.SetActive(false);
            DateFailText.SetActive(true);
        }
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
