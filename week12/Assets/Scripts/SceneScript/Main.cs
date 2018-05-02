using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : Scene<TransitionData> {

    public int numChars;
    public List<NPC> npcs;

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

    public List<string> MessagePool;

    private string[] words;

	// Use this for initialization
	private void Awake()
    {
	}
	void Start () {

        npcs = new List<NPC>();
        currChar = 0;

        StartCoroutine(ReadFileAsync("https://gist.githubusercontent.com/mkim1994/c64302fdeac21771c97c952b2f55eb69/raw/884c1a0e01a0c7c2dc7bea906180e840893055aa/names.txt"));

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
        if(currChar == numChars){
            if (nextButton.activeSelf)
            {
                nextButton.SetActive(false);
            }
            if(!buttons[0].activeSelf){
                foreach(GameObject b in buttons){
                    b.SetActive(true);
                }
            }
        } else{
            if(!nextButton.activeSelf){
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
            message += npcs[currChar].charName + " "+ MessagePool[Random.Range(0, MessagePool.Count)]
                                     + " " + npcs[currChar].targets[Random.Range(0, npcs[currChar].targets.Length)].charName;


            currChar++;
        } else{ //your turn --> currChar == numChars
        }
        SendMessageToLog(message);

    }

    public void ActionDoNothing(){
        string message = "You did nothing";
        SendMessageToLog(message);
        currChar = 0;
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
