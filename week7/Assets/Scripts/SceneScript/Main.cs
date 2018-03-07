using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : Scene<TransitionData> {

    public bool outlineMode;
    public GameObject ParticleSystem;
    public GameObject Floor;
    private Vector3 initialFloorAngles;
	// Use this for initialization
	void Start () {
        initialFloorAngles = Floor.transform.localEulerAngles;

	}
	
	// Update is called once per frame
	void Update () {
        if(outlineMode){
            if(!Services.GameManager.currentCamera.GetComponent<ReplacementShaderEffect>().enabled){
                Services.GameManager.currentCamera.GetComponent<ReplacementShaderEffect>().enabled = true;
                Services.GameManager.currentCamera.GetComponent<QuickGlow>().enabled = true;
                //Floor.GetComponent<Animator>().StopPlayback();
                StopFloor(true);
            }
        } else{
            if (Services.GameManager.currentCamera.GetComponent<ReplacementShaderEffect>().enabled)
            {
                Services.GameManager.currentCamera.GetComponent<ReplacementShaderEffect>().enabled = false;
                Services.GameManager.currentCamera.GetComponent<QuickGlow>().enabled = false;
                StopFloor(false);

            }
        }
	}

	public void StopFloor(bool stop)
	{
        if(stop){

            Floor.GetComponent<Animator>().enabled = false;
            Floor.transform.localEulerAngles = initialFloorAngles;
        } else{

            Floor.GetComponent<Animator>().enabled = true;
        }
	}

	void InitializeServices()
	{
		Services.Main = this;
	}

	internal override void OnEnter(TransitionData data)
	{
		InitializeServices();
		Services.GameManager.currentCamera = GetComponentInChildren<Camera>();

        Services.GameManager.fadeCavnas.Fade(true, 2f);

	}


    public void Restart(){
        Services.SceneStackManager.PopScene();
        Services.SceneStackManager.PushScene<Main>();
    }

    public void MainMenu(){

        Services.SceneStackManager.Swap<TitleScreen>();

    }

}
