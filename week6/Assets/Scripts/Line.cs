using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Line : MonoBehaviour {

    public int nSegments = 90;
    public float enemyProbability = 0.5f;
    public float threshold = 0f;
    //private float timeToGo1, timeToGo2;
   // public float speed = 0.5f;

    [HideInInspector]
    public LineRenderer[] MainLine;
    private bool[] EnemyPresence;

    /*
     * players: the red/yellow/green is on the player
     * line: gray when you're not on it
     * line: purple for the enemy
     * 
     * */
    //player indices
    private int player1;
    private int player2;
    private int player1goal;
    private int player2goal;

    private bool player1Die;
    private bool player2Die;

    private bool gameover;
    private bool redHold;

    private bool cameraShaking;


    private enum Traffic
    {
        Green, Yellow, Red
    }

    private Traffic currentTraffic;
    private bool startTraffic = false;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateFake(){
        // StartCoroutine(CreateFakeAnim());
        StartCoroutine(CreateSpaces());
 
    }

    IEnumerator CreateSpaces(){
        gameover = false;
        player1Die = false;
        player2Die = false;
        int angle = 360 / nSegments;
        MainLine = new LineRenderer[nSegments];
        EnemyPresence = new bool[nSegments];
        player1 = 1;
        player2 = nSegments - 1;
        player1goal = player2;
        player2goal = player1;
        for (int i = 0; i < nSegments/2; ++i)
        {
            MainLine[i] = Instantiate(Services.Prefabs.CurvedLine,
                                      Services.Prefabs.CurvedLine.transform.position,
                                      Services.Prefabs.CurvedLine.transform.rotation).GetComponent<LineRenderer>();
            MainLine[i].gameObject.SetActive(true);
            MainLine[i].transform.SetParent(Services.GameManager.GetComponentInChildren<Line>().transform);
            MainLine[i].transform.localEulerAngles = new Vector3(0, 0, i * angle);

            MainLine[nSegments-1-i] = Instantiate(Services.Prefabs.CurvedLine,
                          Services.Prefabs.CurvedLine.transform.position,
                          Services.Prefabs.CurvedLine.transform.rotation).GetComponent<LineRenderer>();
            MainLine[nSegments-1-i].gameObject.SetActive(true);
            MainLine[nSegments - 1 - i].transform.SetParent(Services.GameManager.GetComponentInChildren<Line>().transform);
            MainLine[nSegments - 1 - i].transform.localEulerAngles = new Vector3(0, 0, (nSegments - 1 - i) * angle);

            MainLine[i].material = Services.GameManager.mats[0];
            MainLine[nSegments - 1 - i].material = Services.GameManager.mats[0];

            float r = Random.Range(0f, 1f);
            if(i == 0){ //nSegments-1-i = 89
                MainLine[i].material = Services.GameManager.mats[5];

            }else
            {
                if ((5 < i && nSegments - 1 - i < nSegments - 5) && (r > enemyProbability)) //spawning enemy
                {
                    MainLine[i].material = Services.GameManager.mats[4];
                    MainLine[nSegments - 1 - i].material = Services.GameManager.mats[4];
                    EnemyPresence[i] = true;
                    EnemyPresence[nSegments - 1 - i] = true;
                }
            }
            Services.GameManager.GetComponentInChildren<Line>().transform.localEulerAngles = new Vector3(0, 0,88f);
            yield return new WaitForSeconds(0.003f);

        }
        currentTraffic = Traffic.Green;
        startTraffic = true;
        redHold = false;
       // timeToGo1 = Time.fixedTime + speed;
       // timeToGo2 = Time.fixedTime + speed;

        DOTween.Init();
        cameraShaking = false;

    }

    public void LineUpdate(){
        if (!gameover)
        {
            switch (currentTraffic)
            {
                case Traffic.Green:
                    if (!player1Die)
                    {
                        MainLine[player1].material = Services.GameManager.mats[1];
                    }
                    if (!player2Die)
                    {
                        MainLine[player2].material = Services.GameManager.mats[1];
                    }
                    break;
                case Traffic.Yellow:
                    if (!player1Die)
                    {
                        MainLine[player1].material = Services.GameManager.mats[2];
                    }
                    if (!player2Die)
                    {
                        MainLine[player2].material = Services.GameManager.mats[2];
                    }
                    break;
                case Traffic.Red:
                    if (!player1Die)
                    {
                        MainLine[player1].material = Services.GameManager.mats[3];
                    }
                    if (!player2Die)
                    {
                        MainLine[player2].material = Services.GameManager.mats[3];
                    }
                    break;
            }

            //enemy spots
            for (int i = 0; i < EnemyPresence.Length; ++i)
            {
                if ((i != player1 && i != player2) &&
                    EnemyPresence[i])
                {
                    MainLine[i].material = Services.GameManager.mats[4];
                }
            }
        }
    }

    public void LineFixedUpdate(){

        if (!gameover)
        {
            if (player1 == player1goal)
            { //player 1 wins!
                gameover = true;
                StartCoroutine(VictoryAnimation(true));
            }
            else if (player2 == player2goal)
            { //player 2 wins!
                gameover = true;
                StartCoroutine(VictoryAnimation(false));
            }
            else
            {
                if (startTraffic)
                {
                    startTraffic = false;
                    StartCoroutine(WaitRandomTraffic());
                }

                if (Input.GetAxis("P1_Vertical") > threshold && !player1Die)
                {
                    if (player1 < player1goal)
                    {
                        if (currentTraffic == Traffic.Red && redHold)
                        {
                            player1Die = true;
                            StartCoroutine(RespawnPlayer(player1Die, player1, player2goal));
                        } else if(currentTraffic == Traffic.Red){
                            CameraShake();
                        }
                        else if(currentTraffic != Traffic.Red)
                        {
                            Material currentMat = MainLine[player1].material;
                            MainLine[player1].material = Services.GameManager.mats[0];
                            player1 += 1;
                            MainLine[player1].material = currentMat;
                        }
                    }
                } else if(Input.GetAxis("P1_Vertical") <=0.05f && !player1Die){ //not moving up, hasn't died yet
                    if(EnemyPresence[player1]){
                        player1Die = true;
                        CameraShake();
                        StartCoroutine(RespawnPlayer(player1Die, player1, player2goal));
                    }
                }


                if (Input.GetAxis("P2_Vertical") > threshold && !player2Die)
                {
                    if (player2 > player2goal)
                    {
                        if (currentTraffic == Traffic.Red && redHold)
                        {
                            player2Die = true;
                            StartCoroutine(RespawnPlayer(player2Die, player2, player1goal));
                        } else if(currentTraffic == Traffic.Red){
                            CameraShake();
                        }
                        else if (currentTraffic != Traffic.Red)
                        {
                            Material currentMat = MainLine[player2].material;
                            MainLine[player2].material = Services.GameManager.mats[0];
                            player2 -= 1;
                            MainLine[player2].material = currentMat;
                        }

                    }

                }else if (Input.GetAxis("P2_Vertical") <= 0.05f && !player2Die)
                { //not moving up, hasn't died yet
                    if (EnemyPresence[player2])
                    {
                        player2Die = true;

                        CameraShake();
                        StartCoroutine(RespawnPlayer(player2Die, player2, player1goal));
                    }
                }


            }
        }
    }

    private void CameraShake(){
        /*if (!cameraShaking)
        {
            cameraShaking = true;
            Services.GameManager.currentCamera.DOShakePosition(0.1f,10f, 3, 90, true).OnComplete(()=>cameraShaking = false);
        }*/
        //introduce static?
    }
    IEnumerator WaitRandomTraffic(){
        yield return new WaitForSeconds(Random.Range(0.6f, 2.5f));

        currentTraffic = Traffic.Yellow;
        yield return new WaitForSeconds(1f);

        currentTraffic = Traffic.Red;
        yield return new WaitForSeconds(0.5f);
        redHold = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        redHold = false;

        currentTraffic = Traffic.Green;

        startTraffic = true;
    }

    IEnumerator RespawnPlayer(bool playerDie, int playerN, int originalIndex){
        MainLine[playerN].material = Services.GameManager.mats[0];
        if (originalIndex == player1goal)
        {
            player2 = originalIndex;
        }
        else if (originalIndex == player2goal)
        {
            player1 = originalIndex;
        }
        yield return new WaitForSeconds(1f);
       // MainLine[originalIndex].material = Services.Main.mats[]
        if(originalIndex == player1goal){
            player2Die = false;
        } else if(originalIndex == player2goal){
            player1Die = false;
        }

    }

    IEnumerator VictoryAnimation(bool player1Win){
        float timeAnim = 0.01f;
        if(!player1Win){
            for (int i = 0; i < MainLine.Length; ++i){
                if (i != player2)
                {
                    MainLine[i].material = Services.GameManager.mats[5];
                    yield return new WaitForSeconds(timeAnim);

                }

            }

            Services.Main.player1WinText.SetActive(true); //swapped b/c of careless ordering lol
        } else{
            for (int i = MainLine.Length - 1; i >= 0; --i)
            {
                if (i != player1)
                {
                    MainLine[i].material = Services.GameManager.mats[5];
                    yield return new WaitForSeconds(timeAnim);
                }
            }

            Services.Main.player2WinText.SetActive(true);
        }
    }

}

