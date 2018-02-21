using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicButton : MonoBehaviour {

    Button button;
    Text text;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        text = button.GetComponentInChildren<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(
            text.GetComponent<RectTransform>().sizeDelta.x,
            button.GetComponent<RectTransform>().sizeDelta.y);
	}
}
