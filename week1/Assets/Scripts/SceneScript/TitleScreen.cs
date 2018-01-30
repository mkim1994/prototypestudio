using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : Scene<TransitionData> {

    public Sprite[] yuncomics;
    public Sprite[] noacomics;
    public GameObject startButton;
    public GameObject instruction;

    public Image yun;
    public Image noa;

    private int spaceCount;

	void Start()
	{
        Cursor.visible = true;
        spaceCount = 0;
	}

	// Update is called once per frame
	void Update()
	{
        if(Input.GetKeyDown(KeyCode.Space)){
            switch(spaceCount){
                case 0:
                    yun.gameObject.SetActive(true);
                    break;
                case 1:
                    yun.color = new Color(1f,1f,1f, 0.3f);
                    noa.gameObject.SetActive(true);
                    break;
                case 2:
                    noa.color = new Color(1f, 1f, 1f, 0.3f);
                    yun.color = new Color(1f, 1f, 1f, 1f);
                    yun.sprite = yuncomics[1];
                    break;
                case 3:
                    yun.color = new Color(1f, 1f, 1f, 0.3f);
                    noa.color = new Color(1f, 1f, 1f, 1f);
                    noa.sprite = noacomics[1];
                    break;
                case 4:
                    noa.color = new Color(1f, 1f, 1f, 0.3f);
                    yun.color = new Color(1f, 1f, 1f, 1f);
                    yun.sprite = yuncomics[2];
                    break;
                case 5:
                    yun.color = new Color(1f, 1f, 1f, 0.3f);
                    noa.color = new Color(1f, 1f, 1f, 1f);
                    noa.sprite = noacomics[2];
                    instruction.gameObject.SetActive(true);
                    startButton.gameObject.SetActive(true);
                    break;

            }

            spaceCount++;

        }

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
