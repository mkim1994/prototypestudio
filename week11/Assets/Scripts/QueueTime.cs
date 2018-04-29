using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QueueTime : MonoBehaviour {

    public float elapsedTime;
    float offsetTime;
    float additionalOffsetTime;

    float elapsedPermittedTime;
    float permittedTimeStart;

    public Image fill;
    //five seconds to select Accept
	// Use this for initialization
	void Start () {}

	private void OnEnable()
	{

        GetComponent<TextMeshProUGUI>().text = "0:00";
        elapsedTime = 0f;
        elapsedPermittedTime = 0f;
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

                fill.fillAmount = 0f;
                Services.Main.popup.SetActive(true);
                Services.GameManager.gameFound = true;
                permittedTimeStart = Time.timeSinceLevelLoad;
                elapsedPermittedTime = Time.timeSinceLevelLoad - permittedTimeStart;
            }

        } else{
            additionalOffsetTime = Time.timeSinceLevelLoad - offsetTime - elapsedTime;
            if (elapsedPermittedTime > 5f)
            {
                Services.Main.Decline();
            } else{
                float normal = Mathf.InverseLerp(0f, 5f, elapsedPermittedTime);
                float bValue = Mathf.Lerp(0f, 1f, normal);
                fill.fillAmount = normal;
                elapsedPermittedTime = Time.timeSinceLevelLoad - permittedTimeStart;
            }
        }
	}
}
