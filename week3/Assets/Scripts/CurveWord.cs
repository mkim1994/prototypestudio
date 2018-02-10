using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CurveWord : MonoBehaviour {

    public string word;
    public bool enableOther;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < word.Length; ++i){
            GameObject ch = null;
            if (enableOther)
            {
                ch = Instantiate(Services.Prefabs.OtherLetter, transform.position, Quaternion.identity);
            } else{
                ch = Instantiate(Services.Prefabs.Letter, transform.position, Quaternion.identity);
            }
            ch.GetComponentInChildren<TMP_Text>().text = word[i].ToString();
            ch.transform.Rotate(new Vector3(0f, -i*(360f/word.Length), 0f));
            ch.transform.parent = gameObject.transform;
            //ch.
        }
        //Services.Prefabs.Letter
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
