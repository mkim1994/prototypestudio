using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    private float startTime;
    private float remainingTime;
    private float initializationTime;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Services.Main.gameStarted)
        {
            if (remainingTime > 0)
            {
                float timeSinceInitialization = Time.time - initializationTime;
                remainingTime = startTime - timeSinceInitialization;
                int remain = (int)remainingTime;
                Services.Main.timerText.text = "" + remain;
            } else{
                int evaluated = Services.Main.Baby.Evaluate();
                switch(evaluated){
                    case -1:
                        Services.Main.winnerText.text = "USA MOM WINS!!";
                        break;
                    case 0:
                        Services.Main.winnerText.text = "BOTH MOMS WIN!!";
                        break;
                    case 1:
                        Services.Main.winnerText.text = "CANADA MOM WINS!!";
                        break;
                }
                Services.Main.winnerText.gameObject.SetActive(true);
            }
        } 
	}

    public void StartGame(){
        startTime = 30f;
        remainingTime = startTime;
        initializationTime = Time.time;
    }
}
