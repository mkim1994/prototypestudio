using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour {

    [HideInInspector]
    public bool fadeOutComplete;

    private Image panel;
	// Use this for initialization
	void Awake () {
        fadeOutComplete = false;
        panel = GetComponentInChildren<Image>();
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fade(bool fadein, float duration){
        if (fadein)
        {
            panel.DOFade(0f,duration);
        } else{

            panel.DOFade(1f,duration).OnComplete(()=>fadeOutComplete = true);
        }
    }
    public bool FadeOutComplete(){
        if(fadeOutComplete){
            fadeOutComplete = false;
            return true;
        }
        return false;
    }
}
