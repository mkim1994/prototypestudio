using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {

    public GameObject[] AshChatbox;

    private float fullBarPosY;
    private float hiddenBarPosY;

    public bool chatIsHidden;

    private Image namebar;

    public Color color1;
    public Color color2;

    public float endedNodeTime;
    private float startTime;
    private float elapsedTime;
    public int stage;
	// Use this for initialization
	void Start () {
        /* indices:
         * 0 - chatbox
         * 1 - bar
         * */
        startTime = Time.time;
        stage = 0;

        fullBarPosY = AshChatbox[1].GetComponent<RectTransform>().position.y;
        hiddenBarPosY = 10f;

        namebar = GameObject.FindWithTag("IvoryNameBar").GetComponent<Image>();
        RevealChatbox();
        chatIsHidden = true;

        Services.EventManager.Register<NewChatLineAdded>(NewChatLineAdded);
	}
	
	// Update is called once per frame
	void Update () {
        if(!Services.Main.dialogue.isDialogueRunning){
            if(Time.time - elapsedTime - endedNodeTime > 0){
                elapsedTime = Time.time - elapsedTime - endedNodeTime;
            } else{
                elapsedTime = Time.time - endedNodeTime;
            }
            if(stage < 3 && elapsedTime > Random.Range(5,10)){
                Services.Main.dialogue.StartDialogue("Start"+stage);
                stage++;

            } 
        }

        if(Services.Main.dialogue.variableStorage.GetValue("$gameover").AsBool && stage == 3){//(int)storage.GetValue("$busted").AsNumber == 5
            Services.Main.result.SetActive(true);
            if ((int)Services.Main.dialogue.variableStorage.GetValue("$positive").AsNumber < 1)
            {
                
                Services.Main.result.transform.GetChild(0).GetComponent<Text>().text =
                "You and your friend never talked to each other again.";
            } else if((int)Services.Main.dialogue.variableStorage.GetValue("$positive").AsNumber > 0 &&
                      (int)Services.Main.dialogue.variableStorage.GetValue("$positive").AsNumber < 5){
                Services.Main.result.transform.GetChild(0).GetComponent<Text>().text =
                            "Nothing changes between you and your friend, except, perhaps, a little more frigid.";
            } else{
                Services.Main.result.transform.GetChild(0).GetComponent<Text>().text =
                            "You still talk with your friend the subsequent days. Occasionally.";
 
            }

        }
	}

    public void RevealChatbox(){
        if (AshChatbox[1].GetComponent<RectTransform>().position.y < 11f) 
        { // reveal the chat
          // AshChatbox[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            AshChatbox[0].GetComponent<CanvasGroup>().alpha = 1f;
            AshChatbox[1].GetComponent<RectTransform>().position = new Vector3(
                AshChatbox[1].GetComponent<RectTransform>().position.x,
                fullBarPosY,
                AshChatbox[1].GetComponent<RectTransform>().position.z);
            chatIsHidden = false;
            namebar.color = color1;
            Services.Main.panel.SetActive(true);
        } else {
            // hide the chat
           // AshChatbox[0].GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
            AshChatbox[0].GetComponent<CanvasGroup>().alpha = 0f;
            AshChatbox[1].GetComponent<RectTransform>().position = new Vector3(
                AshChatbox[1].GetComponent<RectTransform>().position.x,
                hiddenBarPosY,
                AshChatbox[1].GetComponent<RectTransform>().position.z);
            chatIsHidden = true;

            Services.Main.panel.SetActive(false);

        }
    }


    void NewChatLineAdded(EventE e){
        if (chatIsHidden)
        {
            GetComponent<AudioSource>().Play();
            namebar.color = color2;
        }
    }
}
