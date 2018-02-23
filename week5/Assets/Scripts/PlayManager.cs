using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayManager : MonoBehaviour {

    public float timeAlotted;
    private float startTime;
    private float remainingTime;
    private float initializationTime;

    public AudioClip canadawin, usawin, tie;
	// Use this for initialization
	void Start () {
        DOTween.Init();
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
                        Services.Main.winnerText.GetComponent<AudioSource>().clip = usawin;
                        break;
                    case 0:
                        Services.Main.winnerText.text = "BOTH MOMS WIN!!";
                        Services.Main.winnerText.GetComponent<AudioSource>().clip = tie;
                        break;
                    case 1:
                        Services.Main.winnerText.text = "CANADA MOM WINS!!";
                        Services.Main.winnerText.GetComponent<AudioSource>().clip = canadawin;
                        break;
                }
                Services.Main.winnerText.gameObject.SetActive(true);

                Player[] players = FindObjectsOfType<Player>();
                for (int i = 0; i < players.Length; ++i){
                    players[i].enabled = false;
                }
                Services.Main.timerText.GetComponent<AudioSource>().DOFade(0f, 1f);

            }
        } 
	}

    public void StartGame(){
        startTime = timeAlotted;
        remainingTime = startTime;
        initializationTime = Time.time;
    }
}
