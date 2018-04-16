using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Player : MonoBehaviour {

    private float speed = 1f;
    AudioSource aud;
	// Use this for initialization
	void Start () {
        DOTween.Init();
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}


    void Move()
    {

        if(Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f){
            if (aud.volume > 0.99999f)
            {
                aud.DOFade(0f, 0.5f);
            }
        } else{
           /* if((0 < Input.GetAxis("Horizontal") && 0 > Input.GetAxis("Horizontal"))
           || (0< Input.GetAxis("Vertical") && 0 > Input.GetAxis("Vertical"))){*/
            if (aud.volume < 0.001f)
            {
                aud.DOFade(1f, 0.5f);
            }
        }
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x, y, 0);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }



}
