using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : Scene<TransitionData> {

    // List<GameObject> pixels;
    GameObject[,] pixelgrid;
    public GameObject PixelGrid;
    int numCols = 100;
    int numRows = 100;

	// Use this for initialization
	void Start () {
        Services.GameManager.fadeCanvas.Fade(true, 2f);
        pixelgrid = new GameObject[numCols, numRows];
        //Instantiate(Services.Prefabs.Pixel,

        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                Vector2 location = new Vector2(i - numCols / 2 + 0.5f, j - numRows / 2 + 0.5f);
                GameObject boardSpace = Instantiate(Services.Prefabs.Pixel, location, Quaternion.identity) as GameObject;
                boardSpace.transform.SetParent(PixelGrid.transform, false);
             }
        }

	}
	
	// Update is called once per frame
	void Update () {
        
	}

	void InitializeServices()
	{
		Services.Main = this;
	}

	internal override void OnEnter(TransitionData data)
	{
		InitializeServices();
		Services.GameManager.currentCamera = GetComponentInChildren<Camera>();

	}


    public void Restart(){
        Services.SceneStackManager.PopScene();
        Services.SceneStackManager.PushScene<Main>();
    }

    public void MainMenu(){

        Services.SceneStackManager.Swap<TitleScreen>();

    }

}
