using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : Scene<TransitionData> {

    public int numChars;
    public List<NPC> npcs;

    bool gameEnd;

    public NPC.Relationship[] relationshipStates = {NPC.Relationship.Acquaintance, NPC.Relationship.FriendsWith,
        NPC.Relationship.Likes,NPC.Relationship.Dislikes,NPC.Relationship.Strangers};

    public List<Message> messageList = new List<Message>();

    public GameObject MessageContent;
    public ScrollRect scroll;
    // public GameObject 

    public int currChar;

    public Transform spawnPoint1, spawnPoint2;

    public List<Sprite> sprites;

    public GameObject nextButton;
    public GameObject[] buttons;
    public Text episodeNumText;
    public int episodeNum;

    public List<string> MessagePool;

    public Dictionary<NPC.Relationship,string[]> ReactionPoolTalk = new Dictionary<NPC.Relationship,string[]>();
    public Dictionary<NPC.Relationship, string[]> ReactionPoolLook = new Dictionary<NPC.Relationship, string[]>();
    //public Dictionary<NPC.Relationship,string> ReactionPool

    private string[] words;

	// Use this for initialization
	private void Awake()
    {
	}
	void Start () {

        gameEnd = false;
        npcs = new List<NPC>();
        currChar = 0;
        episodeNum = 1;

        StartCoroutine(ReadFileAsync("https://gist.githubusercontent.com/mkim1994/c64302fdeac21771c97c952b2f55eb69/raw/884c1a0e01a0c7c2dc7bea906180e840893055aa/names.txt"));

        string[] talk1 = { "replies with disinterest to","replies politely to","replies with some interest to"};
        string[] talk2 = { "ignores", "replies half-heartedly to ","replies with some interest to"};
        string[] talk3 = { "is weirded out by", "responds to", "excitedly responds to" };
        string[] talk4 = { "is weirded out by", "responds to", "ecstatically responds to" };
        string[] talk5 = { "ignores","replies cautiously to","replies with some interest to"};

        ReactionPoolTalk.Add(NPC.Relationship.Acquaintance, talk1);
        ReactionPoolTalk.Add(NPC.Relationship.Dislikes, talk2);
        ReactionPoolTalk.Add(NPC.Relationship.FriendsWith, talk3);
        ReactionPoolTalk.Add(NPC.Relationship.Likes, talk4);
        ReactionPoolTalk.Add(NPC.Relationship.Strangers, talk5);

        string[] look1 = { "casts a weird look at", "looks back at", "looks back with interest at" };
        string[] look2 = { "is creeped out by", "ignores", "looks back at" };
        string[] look3 = { "is weirded out by", "looks back at", "nods back with approval at" };
        string[] look4 = { "is weirded out by", "checks out", "shyly checks out" };
        string[] look5 = { "is creeped out by", "furtively glances back at", "glances back with interest at" };
        ReactionPoolLook.Add(NPC.Relationship.Acquaintance, look1);
        ReactionPoolLook.Add(NPC.Relationship.Dislikes, look2);
        ReactionPoolLook.Add(NPC.Relationship.FriendsWith, look3);
        ReactionPoolLook.Add(NPC.Relationship.Likes,look4);
        ReactionPoolLook.Add(NPC.Relationship.Strangers, look5);

	}

    void Setup(){
        ProceduralNameGenerator names = new ProceduralNameGenerator(words);
        //numChars = Random.Range(2, 4);
        numChars = 3;
        for (int i = 0; i < numChars; ++i)
        {
            Vector3 spawnPoint;
            if (numChars < 3)
            {
                spawnPoint = spawnPoint1.GetChild(i).transform.position;
            }
            else
            {
                spawnPoint = spawnPoint2.GetChild(i).transform.position;
            }
            GameObject npcObj = Instantiate(Services.Prefabs.NPC, spawnPoint, Quaternion.identity,
                                            this.transform) as GameObject;
            int spriteRand = Random.Range(0, sprites.Count);
            npcObj.GetComponent<SpriteRenderer>().sprite = sprites[spriteRand];
            sprites.RemoveAt(spriteRand);
            npcObj.GetComponent<NPC>().InitializeNPC(names.GenerateRandomWord(5));
            npcs.Add(npcObj.GetComponent<NPC>());
        }

        for (int j = 0; j < numChars; ++j)
        {
            List<NPC> npcPrep = new List<NPC>();
            NPC.Relationship[] relationshipPrep = new NPC.Relationship[numChars - 1];
            for (int k = 0; k < numChars; ++k)
            {
                if (k != j)
                {
                    npcPrep.Add(npcs[k]);
                }
            }
            for (int l = 0; l < numChars - 1; ++l)
            {
                relationshipPrep[l] = relationshipStates[Random.Range(0, relationshipStates.Length)];
            }
            npcs[j].SetRelationships(npcPrep, relationshipPrep);
        }
    }

    IEnumerator ReadFileAsync(string fileURL)
    {
        WWW fileWWW = new WWW(fileURL);
        yield return fileWWW;

        // now you have your file's text, you can do whatever you want with it.
        string textFileContents = fileWWW.text;
        words = textFileContents.Split(new string[] { "\r", "\n" },System.StringSplitOptions.RemoveEmptyEntries);
        Setup();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameEnd)
        {
            if (currChar == numChars)
            {
                if (nextButton.activeSelf)
                {
                    nextButton.SetActive(false);
                }
                if (!buttons[0].activeSelf)
                {
                    foreach (GameObject b in buttons)
                    {
                        b.SetActive(true);
                    }
                }
            }
            else
            {
                if (!nextButton.activeSelf)
                {
                    nextButton.SetActive(true);
                }
                if (buttons[0].activeSelf)
                {
                    foreach (GameObject b in buttons)
                    {
                        b.SetActive(false);
                    }
                }
            }
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

	}


    public void Restart(){
        Services.SceneStackManager.PopScene();
        Services.SceneStackManager.PushScene<Main>();
    }

    public void MainMenu(){

        Services.SceneStackManager.Swap<TitleScreen>();

    }


    public void NextAction(){
        string message = "";
        if(currChar < numChars){
            int count = 0;
            foreach (KeyValuePair<NPC,string> entry in npcs[currChar].toRespondTo)
            {
                count++;
                // do something with entry.Value or entry.Key
                //message+=npcs[currChar].charName + " "+
                if (count > 1)
                {
                    message += ". " + npcs[currChar].charName + " ";
                } else{
                    message += npcs[currChar].charName + " ";
                }
                string reaction = "";

                /*
                 * if you're talked to/looked at by someone you dislike, 30-70 chance at -1
                 * 
                 * 
                 * */
                if(entry.Value.Contains("talk")){
                    for (int i = 0; i < npcs[currChar].targets.Length; ++i){
                        if(npcs[currChar].targets[i] == entry.Key){
                            switch(npcs[currChar].relationshipStates[i]){
                                case NPC.Relationship.Likes:
                                    string[] strs1;
                                    int index1;
                                    float odds1 = Random.Range(0f, 100f);
                                    if(odds1 > 1f){
                                        npcs[currChar].points[i]++;
                                        npcs[currChar].targets[i].points[i]++;
                                        index1 = 2;
                                    } else{
                                        npcs[currChar].points[i]--;
                                        index1 = 0;
                                    }
                                    ReactionPoolTalk.TryGetValue(NPC.Relationship.Likes, out strs1);
                                    reaction = strs1[index1];
                                    break;
                                case NPC.Relationship.Acquaintance:
                                    string[] strs2;
                                    int index2;
                                    float odds2 = Random.Range(0f, 100f);
                                    if (odds2 > 10f)
                                    {
                                        npcs[currChar].points[i]++;
                                        npcs[currChar].targets[i].points[i]++;
                                        index2 = 2;
                                    }else
                                    {
                                        npcs[currChar].points[i]--;
                                        index2 = 0;
                                    }
                                    ReactionPoolTalk.TryGetValue(NPC.Relationship.Acquaintance, out strs2);
                                    reaction = strs2[index2];
                                    break;
                                case NPC.Relationship.Dislikes:
                                    string[] strs3;
                                    int index3;
                                    float odds3 = Random.Range(0f, 100f);
                                    if (odds3 > 30f)
                                    {
                                        npcs[currChar].points[i]++;

                                        npcs[currChar].targets[i].points[i]++;
                                        index3 = 2;
                                    } else
                                    {
                                        npcs[currChar].points[i]--;
                                        index3 = 0;
                                    }
                                    ReactionPoolTalk.TryGetValue(NPC.Relationship.Dislikes, out strs3);
                                    reaction = strs3[index3];
                                    break;
                                case NPC.Relationship.Strangers:
                                    string[] strs4;
                                    int index4;
                                    float odds4 = Random.Range(0f, 100f);
                                    if (odds4 > 20f)
                                    {
                                        npcs[currChar].points[i]++;

                                        npcs[currChar].targets[i].points[i]++;
                                        index4 = 2;
                                    }else
                                    {
                                        npcs[currChar].points[i]--;
                                        index4 = 0;
                                    }
                                    ReactionPoolTalk.TryGetValue(NPC.Relationship.Strangers, out strs4);
                                    reaction = strs4[index4];
                                    break;
                                case NPC.Relationship.FriendsWith:
                                    string[] strs5;
                                    int index5;
                                    float odds5 = Random.Range(0f, 100f);
                                    if (odds5 > 5f)
                                    {
                                        npcs[currChar].points[i]++;

                                        npcs[currChar].targets[i].points[i]++;
                                        index5 = 2;
                                    }else
                                    {

                                        npcs[currChar].points[i]--;
                                        index5 = 0;
                                    }
                                    ReactionPoolTalk.TryGetValue(NPC.Relationship.FriendsWith, out strs5);
                                    reaction = strs5[index5];
                                    break;
                            }
                            npcs[currChar].EvalRelationship();

                            //npcs[currChar].toRespondTo.Remove(entry.Key);

                            //break;
                        }
                    }
                } else if(entry.Value.Contains("look")){
                    for (int i = 0; i < npcs[currChar].targets.Length; ++i)
                    {
                        if (npcs[currChar].targets[i] == entry.Key)
                        {
                            switch (npcs[currChar].relationshipStates[i])
                            {
                                case NPC.Relationship.Likes:
                                    string[] strs1;
                                    int index1;
                                    float odds1 = Random.Range(0f, 100f);
                                    if (odds1 > 5f)
                                    {
                                        npcs[currChar].points[i]++;

                                        npcs[currChar].targets[i].points[i]++;
                                        index1 = 2;
                                    }else
                                    {

                                        npcs[currChar].points[i]--;
                                        index1 = 0;
                                    }
                                    ReactionPoolLook.TryGetValue(NPC.Relationship.Likes, out strs1);
                                    reaction = strs1[index1];
                                    break;
                                case NPC.Relationship.Acquaintance:
                                    string[] strs2;
                                    int index2;
                                    float odds2 = Random.Range(0f, 100f);
                                    if (odds2 > 20f)
                                    {
                                        npcs[currChar].points[i]++;

                                        npcs[currChar].targets[i].points[i]++;
                                        index2 = 2;
                                    }else
                                    {
                                        npcs[currChar].points[i]--;
                                        index2 = 0;
                                    }
                                    ReactionPoolLook.TryGetValue(NPC.Relationship.Acquaintance, out strs2);
                                    reaction = strs2[index2];
                                    break;
                                case NPC.Relationship.Dislikes:
                                    string[] strs3;
                                    int index3;
                                    float odds3 = Random.Range(0f, 100f);
                                    if (odds3 > 60f)
                                    {
                                        npcs[currChar].points[i]++;

                                        npcs[currChar].targets[i].points[i]++;
                                        index3 = 2;
                                    } else
                                    {
                                        npcs[currChar].points[i]--;
                                        index3= 0;
                                    }
                                    ReactionPoolLook.TryGetValue(NPC.Relationship.Dislikes, out strs3);
                                    reaction = strs3[index3];
                                    break;
                                case NPC.Relationship.Strangers:
                                    string[] strs4;
                                    int index4;
                                    float odds4 = Random.Range(0f, 100f);
                                    if (odds4 > 50f)
                                    {
                                        npcs[currChar].points[i]++;

                                        npcs[currChar].targets[i].points[i]++;
                                        index4 = 2;
                                    }else
                                    {
                                        npcs[currChar].points[i]--;
                                        index4 = 0;
                                    }
                                    ReactionPoolLook.TryGetValue(NPC.Relationship.Strangers, out strs4);
                                    reaction = strs4[index4];
                                    break;
                                case NPC.Relationship.FriendsWith:
                                    string[] strs5;
                                    int index5;
                                    float odds5 = Random.Range(0f, 100f);
                                    if (odds5 > 10f)
                                    {
                                        npcs[currChar].points[i]++;

                                        npcs[currChar].targets[i].points[i]++;
                                        index5 = 2;
                                    }else
                                    {

                                        npcs[currChar].points[i]--;
                                        index5 = 0;
                                    }
                                    ReactionPoolLook.TryGetValue(NPC.Relationship.FriendsWith, out strs5);
                                    reaction = strs5[index5];
                                    break;
                            }

                            npcs[currChar].EvalRelationship();
                            //npcs[currChar].toRespondTo.Remove(entry.Key);
                            //break;
                        }
                    }
                }
                message += reaction;
                message += " " + entry.Key.charName;

               // break;
            }


            if (count < 1)
            {
                int m = Random.Range(0, MessagePool.Count);
                int t = Random.Range(0, npcs[currChar].targets.Length);
                message += npcs[currChar].charName + " " + MessagePool[m]
                                         + " " + npcs[currChar].targets[t].charName;
                npcs[currChar].targets[t].toRespondTo.Add(npcs[currChar], MessagePool[m]);

            } else{
                npcs[currChar].toRespondTo.Clear();
            }

            currChar++;
        } else{ //your turn --> currChar == numChars
        }
        SendMessageToLog(message);

    }

    public void ActionDoNothing(){
        string message = "Plot goes nowhere";
        episodeNum++;
        episodeNumText.text = ""+episodeNum;
        SendMessageToLog(message);
        currChar = 0;
    }

    public void ActionEveryoneDates(){
        string message = "The audience is fed up with the filler episodes, so the characters confess their feelings for each other.";

        episodeNum++;
        episodeNumText.text = "" + episodeNum;
        int counting = 0;
        for (int i = 0; i < npcs.Count; ++i){
            for (int r = 0; r < npcs[i].relationshipStates.Length;++r){
                if (npcs[i].relationshipStates[r] != NPC.Relationship.Likes && counting == 0)
                {
                    counting++;
                    message += " But they weren't all mutual. UNSATISFACTORY CONCLUSION. R to reboot series.";
                }
            }
        }

        if(!message.Contains("UNSATISFACTORY")){
            message += " And they were all mutual. SATISFACTORY CONCLUSION. R to reboot series.";

            episodeNum *= 10;
            episodeNumText.text = "" + episodeNum;
        }
        nextButton.SetActive(false);
        foreach (GameObject b in buttons)
        {
            b.SetActive(false);
        }

        gameEnd = true;

        SendMessageToLog(message);
        //currChar = 0;
    }

    public void SendMessageToLog(string msg){
        GameObject newText = Instantiate(Services.Prefabs.TextObj, MessageContent.transform);
        //newText.GetComponent<Text>().
        Message newMessage = new Message(msg, newText.GetComponent<Text>());
        messageList.Add(newMessage);
        StartCoroutine(WaitToScroll());
    }

    IEnumerator WaitToScroll()
    {
        scroll.verticalNormalizedPosition = 0;
        yield return new WaitForSeconds(0.01f);
        scroll.verticalNormalizedPosition = 0;
    }
    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObj;
        public Message(string txt, Text txtObj)
        {
            text = txt;
            textObj = txtObj;
            textObj.text = text;
        }
    }
}
