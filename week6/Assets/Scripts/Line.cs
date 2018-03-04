using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public int nSegments = 90;

    [HideInInspector]
    public GameObject[] MainLine;

    /*
     * players: the red/yellow/green is on the player
     * line: gray when you're not on it
     * line: purple for the enemy
     * 
     * */
    //player indices
    private int player1;
    private int player2;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateSpaces(){

        int angle = 360 / nSegments;
        MainLine = new GameObject[nSegments];
        player1 = 0;
        player2 = nSegments - 1;
        for (int i = 0; i < nSegments; ++i)
        {
            MainLine[i] = Instantiate(Services.Prefabs.CurvedLine,
                                      Services.Prefabs.CurvedLine.transform.position,
                                      Services.Prefabs.CurvedLine.transform.rotation);
            //MainLine[i].GetComponent<CurvedLineRenderer>().points
            MainLine[i].gameObject.SetActive(true);
            MainLine[i].transform.SetParent(Services.Main.transform);
            MainLine[i].transform.localEulerAngles = new Vector3(0, 0, i * angle);

            if (i == player1 || i == player2)
            {
                MainLine[i].GetComponent<LineRenderer>().material = MainLine[i].GetComponent<LineRenderer>().materials[1];
            }
            else
            {
                MainLine[i].GetComponent<LineRenderer>().material = MainLine[i].GetComponent<LineRenderer>().materials[0];
            }

        }


    }
}

