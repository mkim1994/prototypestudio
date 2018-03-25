using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : Scene<TransitionData> {

    public bool outlineMode;
    public GameObject ParticleSystem;
    public GameObject Floor;
    private Vector3 initialFloorAngles;
    public LayerMask baitLayer;

    FishController fishController;

    public GameObject win;
    public GameObject lose;

    public List<Bait> baits;

    public int goodThoughts;
	// Use this for initialization
	void Start () {
        goodThoughts = 0;
        initialFloorAngles = Floor.transform.localEulerAngles;
        fishController = FindObjectOfType<FishController>();

        // spawn baits
        foreach(Bait bait in FindObjectsOfType<Bait>()){
            baits.Add(bait);
        }
	}
	
	// Update is called once per frame
	void Update () {
        /*if(Input.GetMouseButtonDown(1)){
            outlineMode = !outlineMode;
            if(outlineMode){ 
                foreach(Bait bait in baits){
                    bait.EnableMesh(true);
                }
            } else{ 
                foreach (Bait bait in baits)
                {
                    bait.EnableMesh(false);
                }
            }
        }*/

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


        if(goodThoughts > 0){
            
        } else{
            if (!lose.activeSelf)
            {
                outlineMode = true;

                foreach (Bait bait in baits)
                {
                    bait.EnableMesh(true);
                }
                win.SetActive(true);

            }
        }
        if(!lose.activeSelf && !win.activeSelf){
            MouseDetection();
        }
	}

    void MouseDetection(){

        Ray ray = Services.GameManager.currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, baitLayer)){
                
                fishController.MoveToLocation(hit.transform.position);
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
