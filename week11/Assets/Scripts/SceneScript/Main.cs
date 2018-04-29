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

    public GameObject ChatPanel, TextObject;
    public InputField input;

    public ScrollRect scroll;

    public bool scrollPressed;
    bool canAutoScroll;

	// Use this for initialization
	void Start () {
        Services.GameManager.gameFound = false;
        canAutoScroll = false;

	}
	
	// Update is called once per frame
	void Update () {
        if(Services.GameManager.gameFound){
            
        }
        if (input.text.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {

               
                // SendMessageToChat("pressed space");

                SendMessageToChat(input.text);
                input.text = "";
                input.ActivateInputField();
                input.Select();
                //canAutoScroll = true;
                // Canvas.ForceUpdateCanvases();

                scroll.verticalNormalizedPosition = 0;
                StartCoroutine(Why());
            }
        }

        if(input.isFocused){
            Services.GameManager.typing = true;

        }
       /* Color col = new Color(0, 0, 0);
        ColorUtility.TryParseHtmlString("#C8C8C8", out col);
        if (scroll.verticalScrollbar.handleRect.GetComponent<Image>().color != scroll.verticalScrollbar.colors.pressedColor)
        {
            //Debug.Log("what");
            scroll.verticalNormalizedPosition = 0;
        }
       /* if (scroll.verticalScrollbar.handleRect.GetComponent<Image>().color == col)
        {

            //scroll.verticalNormalizedPosition = 0;
            scrollPressed = true;
        }

        if(!scrollPressed && canAutoScroll){

            scroll.verticalNormalizedPosition = 0;
        }*/
	}

    IEnumerator Why(){
        yield return new WaitForSeconds(0.01f);
        scroll.verticalNormalizedPosition = 0;
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


    public void ScrollPressed(int press){
        if(press == 0){

            scrollPressed = true;
        } else{
            scrollPressed = false;
        }
    }

    public void Restart(){
        Services.SceneStackManager.PopScene();
        Services.SceneStackManager.PushScene<Main>();
    }

    public void MainMenu(){

        Services.SceneStackManager.Swap<TitleScreen>();

    }

    public void Accept(){
        // Services.GameManager.gameFound = true;
        Debug.Log("yay");
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
                GameObject newText = Instantiate(TextObject, ChatPanel.transform);
                //newText.GetComponent<Text>().
                Message newMessage = new Message(text, newText.GetComponent<Text>());
                messageList1.Add(newMessage);
                break;
            case 2:
                break;
            case 3:
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

}
