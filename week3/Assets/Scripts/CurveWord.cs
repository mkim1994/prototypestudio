using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CurveWord : MonoBehaviour {

    public string word;
    public bool enableOther;

    public bool rotateWords;

	// Use this for initialization
	void Start () {

	}

    public void InitializeWord(){
        for (int i = 0; i < word.Length; ++i)
        {
            GameObject ch = null;
            if (enableOther)
            {
                ch = Instantiate(Services.Prefabs.OtherLetter, transform.position, Quaternion.identity);
            }
            else
            {
                ch = Instantiate(Services.Prefabs.Letter, transform.position, Quaternion.identity);
            }
            TMP_Text comp = ch.GetComponentInChildren<TMP_Text>();
            comp.text = word[i].ToString();
            comp.color = new Color(comp.color.r, comp.color.g, comp.color.b,
                                   MapValue(0f, 1f, 0.2f, 1f, (1f / word.Length) * (word.Length - i)));
            ch.transform.Rotate(new Vector3(0f, -i * (360f / word.Length), 0f));
            ch.transform.parent = gameObject.transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (rotateWords)
        {
            transform.Rotate(new Vector3(0, 2f, 0));
        }
	}

    /* color alpha: 0f - 1.0f
     * gradient: 0.5f - 1.0f
     * sample: want to convert (1f/word.Length)*(word.Length - i)
     * MapValue(0f,1f,0.5f,1f,(1f/word.Length)*(word.Length - i))
     * 
     * 
     * */
    private float MapValue(float a0, float a1, float b0, float b1, float a)
    {
        return b0 + (b1 - b0) * ((a - a0) / (a1 - a0));
    }
}
