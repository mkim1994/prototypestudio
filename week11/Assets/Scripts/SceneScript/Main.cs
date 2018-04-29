using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : Scene<TransitionData> {

    public GameObject popup;

    public List<Message> messageList1 = new List<Message>();
    public List<Message> messageList2 = new List<Message>();
    public List<Message> messageList3 = new List<Message>();

    public int maxMessages = 25;

    public GameObject ChatPanel1, ChatPanel2,ChatPanel3, TextObject;

    public GameObject ChatMain1, ChatMain2, ChatMain3;
    public InputField input1, input2, input3;

    public ScrollRect scroll1, scroll2, scroll3;


    public Text queueButtonTxt;

    public GameObject timeDisplay;

    //public 

	// Use this for initialization
	void Start () {
        Services.GameManager.gameFound = false;

	}
	
	// Update is called once per frame
	void Update () {
        if(Services.GameManager.gameFound){
            
        }
        if (ChatMain1.activeSelf)
        {
            if (input1.text.Length > 0)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {


                    SendMessageToChat(1, input1.text);
                    input1.text = "";
                    input1.ActivateInputField();
                    input1.Select();

                    scroll1.verticalNormalizedPosition = 0;
                    StartCoroutine(Why(1));
                }
            }

            if (input1.isFocused)
            {
                Services.GameManager.typing = true;

            }
        } else if(ChatMain2.activeSelf){
            if (input2.text.Length > 0)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {


                    SendMessageToChat(2, input2.text);
                    input2.text = "";
                    input2.ActivateInputField();
                    input2.Select();

                    scroll2.verticalNormalizedPosition = 0;
                    StartCoroutine(Why(2));
                }
            }

            if (input2.isFocused)
            {
                Services.GameManager.typing = true;

            }
        } else if(ChatMain3.activeSelf){
            if (input3.text.Length > 0)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {


                    SendMessageToChat(3, input3.text);
                    input3.text = "";
                    input3.ActivateInputField();
                    input3.Select();

                    scroll3.verticalNormalizedPosition = 0;
                    StartCoroutine(Why(3));
                }
            }

            if (input3.isFocused)
            {
                Services.GameManager.typing = true;

            }
        }


	}

    IEnumerator Why(int i){
        yield return new WaitForSeconds(0.01f);
        switch(i){
            case 1:
                scroll1.verticalNormalizedPosition = 0;
                break;
            case 2:
                scroll2.verticalNormalizedPosition = 0;
                break;
            case 3: 
                scroll3.verticalNormalizedPosition = 0;
                break;
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

    public void Accept(){
        Services.SceneStackManager.Swap<EndScreen>();
    }

    public void Decline(){

        popup.SetActive(false);
        Services.GameManager.timeToQueue = Services.GameManager.timeToQueue + Random.Range(Services.GameManager.minTimeQueue, Services.GameManager.maxTimeQueue)
            + FindObjectOfType<QueueTime>().elapsedTime;
        Services.GameManager.gameFound = false;
    }

    public void SendMessageToChat(int friend, string text){

        switch(friend){
            case 1:
                if (messageList1.Count > maxMessages)
                {
                    Destroy(messageList1[0].textObj.gameObject);
                    messageList1.RemoveAt(0);
                }
                GameObject newText = Instantiate(TextObject, ChatPanel1.transform);
                //newText.GetComponent<Text>().
                Message newMessage = new Message(text, newText.GetComponent<Text>());
                messageList1.Add(newMessage);
                break;
            case 2:
                if (messageList2.Count > maxMessages)
                {
                    Destroy(messageList2[0].textObj.gameObject);
                    messageList2.RemoveAt(0);
                }
                GameObject newText2 = Instantiate(TextObject, ChatPanel2.transform);
                //newText.GetComponent<Text>().
                Message newMessage2 = new Message(text, newText2.GetComponent<Text>());
                messageList1.Add(newMessage2);
                break;
            case 3:
                if (messageList3.Count > maxMessages)
                {
                    Destroy(messageList3[0].textObj.gameObject);
                    messageList3.RemoveAt(0);
                }
                GameObject newText3 = Instantiate(TextObject, ChatPanel3.transform);
                //newText.GetComponent<Text>().
                Message newMessage3 = new Message(text, newText3.GetComponent<Text>());
                messageList1.Add(newMessage3);
                break;
        }

    }


    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObj;
        public Message(string txt, Text txtObj){
            text = txt;
            textObj = txtObj;
            textObj.text = text;
        }
    }

    public void ChatOn(int ch){
        switch(ch){
            case 1:
                ChatMain1.SetActive(true);
                ChatMain2.SetActive(false);
                ChatMain3.SetActive(false);
                break;
            case 2:

                ChatMain1.SetActive(false);
                ChatMain2.SetActive(true);
                ChatMain3.SetActive(false);
                break;
            case 3:

                ChatMain1.SetActive(false);
                ChatMain2.SetActive(false);
                ChatMain3.SetActive(true);
                break;
        }
    }

    public void Queue(){
        
        if(queueButtonTxt.text == "QUEUE"){
            queueButtonTxt.text = "STOP";
            timeDisplay.SetActive(true);
        } else if(queueButtonTxt.text == "STOP"){
            queueButtonTxt.text = "QUEUE";

            timeDisplay.SetActive(false);
        }
    }

}
