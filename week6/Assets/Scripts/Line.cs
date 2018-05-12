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

    private bool changePitch;

    private bool holdingDown1;
    private bool holdingDown2;



    private enum Traffic
    {
        Green, Yellow, Red
    }

    private Traffic currentTraffic;
    private bool startTraffic = false;


	void Start () {
        holdingDown1 = false;
        holdingDown2 = false;
        cameraShaking = false;
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
        changePitch = false;

    }

    public void LineUpdate(){
        if (!gameover)
        {
            switch (currentTraffic)
            {
                case Traffic.Green:
                    if (changePitch)
                    {
                        changePitch = false;
                        Services.GameManager.audioController.bgm.DOPitch(2f, 0.5f);
                    }
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
                    if (changePitch)
                    {
                        changePitch = false;
                        Services.GameManager.audioController.bgm.DOPitch(1f, 0.5f);
                    }
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
                    if (changePitch)
                    {
                        changePitch = false;
                        Services.GameManager.audioController.bgm.DOPitch(0.5f, 0.5f);
                    }
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

                if (Input.GetAxis("P1_Vertical") > threshold && !player1Die && !holdingDown1)
                {
                    if (player1 < player1goal)
                    {

                        if (currentTraffic == Traffic.Red && redHold)
                        {

                            CameraShake();
                            player1Die = true;

                            if (!Services.GameManager.audioController.player1death.isPlaying)
                            {
                                Services.GameManager.audioController.player1death.Play();
                            }
                            StartCoroutine(RespawnPlayer(player1Die, player1, player2goal));
                        } else if(currentTraffic == Traffic.Red){
                           /* Debug.Log("player1 red");
                            if (!Services.GameManager.audioController.player1death.isPlaying)
                            {
                                Services.GameManager.audioController.player1death.Play();
                            }
                            CameraShake();*/
                        }
                        else if(currentTraffic != Traffic.Red)
                        {
                            if (EnemyPresence[player1])
                            {
                                Services.GameManager.audioController.enemy1Run[Random.Range(0, 5)].Play();
                            }
                            Material currentMat = MainLine[player1].material;
                            MainLine[player1].material = Services.GameManager.mats[0];
                            player1 += 1;
                            MainLine[player1].material = currentMat;
                        }
                    }
                } else if(Input.GetAxis("P1_Vertical") <=0.05f && !player1Die){ //not moving up, hasn't died yet
                    holdingDown1 = false;
                    if(EnemyPresence[player1]){
                        player1Die = true;

                        Services.GameManager.audioController.player1death.Play();
                        CameraShake();
                        StartCoroutine(RespawnPlayer(player1Die, player1, player2goal));
                    }
                }


                if (Input.GetAxis("P2_Vertical") > threshold && !player2Die && !holdingDown2)
                {
                    if (player2 > player2goal)
                    {
                        if (currentTraffic == Traffic.Red && redHold)
                        {
                            player2Die = true;

                            if (!Services.GameManager.audioController.player2death.isPlaying)
                            {
                                Services.GameManager.audioController.player2death.Play();
                            }

                            CameraShake();
                            StartCoroutine(RespawnPlayer(player2Die, player2, player1goal));

                        } else if(currentTraffic == Traffic.Red){

                            if (!Services.GameManager.audioController.player2death.isPlaying)
                            {
                                Services.GameManager.audioController.player2death.Play();
                            }

                            CameraShake();
                        }
                        else if (currentTraffic != Traffic.Red)
                        {

                            if (EnemyPresence[player2])
                            {
                                Services.GameManager.audioController.enemy2Run[Random.Range(0, 5)].Play();
                            }
                            Material currentMat = MainLine[player2].material;
                            MainLine[player2].material = Services.GameManager.mats[0];
                            player2 -= 1;
                            MainLine[player2].material = currentMat;
                        }


                    }

                }else if (Input.GetAxis("P2_Vertical") <= 0.05f && !player2Die)
                { //not moving up, hasn't died yet
                    holdingDown2 = false;
                    if (EnemyPresence[player2])
                    {
                        player2Die = true;

                        Services.GameManager.audioController.player2death.Play();

                        CameraShake();
                        StartCoroutine(RespawnPlayer(player2Die, player2, player1goal));
                    }
                }


            }
        }
    }

    private void CameraShake(){
        if (!cameraShaking)
        {
            Camera.main.DOShakePosition(0.2f, 3, 20, 90, false).OnStart(() => cameraShaking = true).OnComplete(() => cameraShaking = false);
        }
        /*if (!cameraShaking)
        {
            cameraShaking = true;
            Services.GameManager.currentCamera.DOShakePosition(0.1f,10f, 3, 90, true).OnComplete(()=>cameraShaking = false);
        }*/
        //introduce static?
    }
    IEnumerator WaitRandomTraffic(){
        yield return new WaitForSeconds(Random.Range(0.6f, 2.5f));

        changePitch = true;
        currentTraffic = Traffic.Yellow;
        yield return new WaitForSeconds(1f);

        changePitch = true;
        currentTraffic = Traffic.Red;

        yield return new WaitForSeconds(0.1f);
        redHold = true;

        yield return new WaitForSeconds(Random.Range(1f, 3f));


        changePitch = true;
        redHold = false;

        currentTraffic = Traffic.Green;

        startTraffic = true;
    }

    IEnumerator RespawnPlayer(bool playerDie, int playerN, int originalIndex){
        if(playerN == player1){
            holdingDown1 = true;
        } else if(playerN == player2){
            holdingDown2 = true;
        }

        //Camera.main.DOShakePosition(0.2f,3,20,90,false);
        MainLine[playerN].material = Services.GameManager.mats[0];

        if (originalIndex == player1goal)
        {
            player2 = originalIndex;
        }
        else if (originalIndex == player2goal)
        {
            player1 = originalIndex;
        }
        yield return new WaitForSeconds(0.3f);
       // MainLine[originalIndex].material = Services.Main.mats[]
        if(originalIndex == player1goal){
            player2Die = false;
        } else if(originalIndex == player2goal){
            player1Die = false;
        }

    }

    IEnumerator VictoryAnimation(bool player1Win){
        Services.GameManager.audioController.bgm.Stop();
        float timeAnim = 0.01f;
        if(!player1Win){ //player 2 wins
            Services.GameManager.audioController.player2win.Play();
            for (int i = 0; i < MainLine.Length; ++i){
                if (i != player2)
                {
                    MainLine[i].material = Services.GameManager.mats[5];
                    yield return new WaitForSeconds(timeAnim);

                }

            }

            Services.Main.player1WinText.SetActive(true); //swapped b/c of careless ordering lol
        } else{ //player 1 wins

            Services.GameManager.audioController.player1win.Play();
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

