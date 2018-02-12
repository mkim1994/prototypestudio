using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class FPSAdditional : MonoBehaviour {

	// Use this for initialization
	void Start () {
     //   DOTween.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal"){
            GameObject.FindWithTag("Fade").GetComponent<Image>().DOFade(1f, 1f).OnComplete(() =>
            SceneManager.LoadScene("fallend"));
        }
    }
}
