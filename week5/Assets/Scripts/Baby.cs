using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour {

    public GameObject head, lefthand, righthand, leg, body, leftfoot, rightfoot, bodyoverlay,neck;
    public int canadaScore;
    public int usaScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Services.Main.gameStarted){
            GetComponentInChildren<Animator>().enabled = true;

        }
	}

    public int Evaluate(){ //0 = tie, -1 = usa won, 1 = canada won
        GameObject[] eval = { head, lefthand, righthand, leg, body, leftfoot, rightfoot, bodyoverlay, neck };
        for (int i = 0; i < eval.Length; ++i)
        {
            if (eval[i] != null)
            {
                if (eval[i].tag == "Canada")
                {
                    canadaScore++;
                }
                else
                {//USA
                    usaScore++;
                }
            }
        }
        if(canadaScore > usaScore){
            return 1;
        } else if(canadaScore < usaScore){
            return -1;
        }
        return 0;

    }
}
