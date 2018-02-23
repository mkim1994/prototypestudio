using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string playername;
    private float speed = 50f;
    private bool grabbing;

    private GameObject grabbed;
	// Use this for initialization
	void Start () {
        grabbing = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (Services.Main.gameStarted)
        {
            Move();
            Grab();

            /*if(!grabbing && grabbed != null){

            }*/
        }
    }

    void Move(){
        float x = Input.GetAxis(playername + "_Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis(playername + "_Vertical") * Time.deltaTime * speed;
        transform.Translate(x, y, 0);



        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Grab(){
        if(Input.GetAxis(playername+"_Grab") > 0){
            GetComponent<SpriteRenderer>().sprite = Services.Sprites.HandgrabSprite;
            grabbing = true;
        } else{ 

            GetComponent<SpriteRenderer>().sprite = Services.Sprites.HandSprite;
            grabbing = false;
            if(grabbed != null){ // DROP WHAT YOU'RE HOLDING
                if (grabbed.GetComponent<Clothes>().collidingWithBaby) //putting it on the baby
                {
                    string layer = grabbed.GetComponent<SpriteRenderer>().sortingLayerName;
                    switch (layer)
                    {
                        case "Body":
                            if (Services.Main.Baby.body == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.bodyPos.position;
                                Services.Main.Baby.body = grabbed;
                                PutGrabbedOn();
                            } else{
                                TakeGrabbedOff();
                            }
                            break;
                    }
                }
                else //just dropping it / disrobing the baby
                {
                    string layer = grabbed.GetComponent<SpriteRenderer>().sortingLayerName;
                    switch (layer)
                    {
                        case "Body":
                            if (Services.Main.Baby.body == grabbed)
                            {
                                Services.Main.Baby.body = null;
                            }
                            break;
                    }
                    TakeGrabbedOff();

                }
            }
        }
    }

    void PutGrabbedOn(){
        grabbed.transform.SetParent(Services.Main.transform);
        grabbed.GetComponent<Clothes>().onTheBaby = true;
        grabbed.GetComponent<SpriteRenderer>().sortingOrder = 0;
        grabbed = null;
    }

    void TakeGrabbedOff(){
        if (grabbed.tag == "Canada")
        {
            grabbed.transform.SetParent(Services.Main.CanadaClothes.transform);
        }
        else if (grabbed.tag == "USA")
        {
            grabbed.transform.SetParent(Services.Main.USAClothes.transform);
        }
        grabbed.transform.position = grabbed.GetComponent<Clothes>().originalPos;
        grabbed.transform.localScale = new Vector3(3, 3, 1);
        grabbed.GetComponent<SpriteRenderer>().sortingOrder = 0;
        grabbed = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Baby"){
           /* if(grabbing && grabbed != null){
               grabbed.transform.position = grabbedOriginalPos;
                grabbed.transform.localScale = new Vector3(3, 3, 1);
                grabbed = null;
            }*/
        } else{
            /*if ((playername == "P1" && collision.tag == "Canada") ||
               (playername == "P2" && collision.tag == "USA"))
            {*/
            if (grabbing && grabbed == null)
            {
                grabbed = collision.gameObject;
                grabbed.transform.localScale = new Vector3(6, 6, 1);

                grabbed.GetComponent<SpriteRenderer>().sortingOrder = 100;

                grabbed.transform.SetParent(transform);

                string layer = grabbed.GetComponent<SpriteRenderer>().sortingLayerName;
                switch (layer)
                {
                    case "Body":
                        if (Services.Main.Baby.body == grabbed)
                        {
                            Services.Main.Baby.body = null;
                        }
                        break;
                }
            }
            //}
        }
    }
}
