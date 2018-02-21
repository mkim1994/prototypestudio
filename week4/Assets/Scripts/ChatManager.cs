using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {

    public GameObject[] AshChatbox;

    private float fullBarPosY;
    private float hiddenBarPosY;

	// Use this for initialization
	void Start () {
        fullBarPosY = AshChatbox[1].GetComponent<RectTransform>().position.y;
        hiddenBarPosY = 10f;

        RevealChatbox();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RevealChatbox(){
        if (AshChatbox[0].GetComponent<RectTransform>().localScale.x < 1f)
        {
            AshChatbox[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            AshChatbox[1].GetComponent<RectTransform>().position = new Vector3(
                AshChatbox[1].GetComponent<RectTransform>().position.x,
                fullBarPosY,
                AshChatbox[1].GetComponent<RectTransform>().position.z);
        } else {
            AshChatbox[0].GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
            AshChatbox[1].GetComponent<RectTransform>().position = new Vector3(
                AshChatbox[1].GetComponent<RectTransform>().position.x,
                hiddenBarPosY,
                AshChatbox[1].GetComponent<RectTransform>().position.z);
        }
    }
}
