using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QueueTime : MonoBehaviour {

    public float elapsedTime;
    float offsetTime;
    float additionalOffsetTime;
	// Use this for initialization
	void Start () {
        elapsedTime = 0f;
        offsetTime = Time.timeSinceLevelLoad;
        additionalOffsetTime = 0;
        Services.GameManager.timeToQueue = Random.Range(Services.GameManager.minTimeQueue, Services.GameManager.maxTimeQueue);
	}
	
	// Update is called once per frame
	void Update () {
        if (!Services.GameManager.gameFound)
        {
            elapsedTime = Time.timeSinceLevelLoad - offsetTime - additionalOffsetTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            GetComponent<TextMeshProUGUI>().text = niceTime;

            if (elapsedTime > Services.GameManager.timeToQueue){
                Services.Main.popup.SetActive(true);
                Services.GameManager.gameFound = true;
            }

        } else{
            additionalOffsetTime = Time.timeSinceLevelLoad - offsetTime - elapsedTime;
        }
	}
}
