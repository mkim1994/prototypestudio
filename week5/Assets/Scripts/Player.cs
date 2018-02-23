using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string playername;
    private float speed = 50f;
    private bool grabbing;

    private GameObject grabbed;
    // Use this for initialization
    void Start()
    {
        grabbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Services.SceneStackManager.CurrentScene != Services.TitleScreen)
        {
            if (Services.Main.gameStarted)
            {
                Move();
                Grab();

            }
        }
        else
        {
            Move();
            Grab();

        }
    }

    void Move()
    {
        float x = Input.GetAxis(playername + "_Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis(playername + "_Vertical") * Time.deltaTime * speed;
        transform.Translate(x, y, 0);



        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Grab()
    {
        if (Input.GetAxis(playername + "_Grab") > 0)
        {
            GetComponent<SpriteRenderer>().sprite = Services.Sprites.HandgrabSprite;
            grabbing = true;
        }
        else
        {

            GetComponent<SpriteRenderer>().sprite = Services.Sprites.HandSprite;
            grabbing = false;
            if (grabbed != null)
            { // DROP WHAT YOU'RE HOLDING
                if (grabbed.GetComponent<Clothes>().collidingWithBaby) //putting it on the baby
                {
                    string layer = grabbed.GetComponent<Clothes>().originalLayerName;
                    switch (layer)
                    {
                        case "Head":
                            if (Services.Main.Baby.head == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.headPos.position;
                                Services.Main.Baby.head = grabbed;
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                        case "Neck":
                            if (Services.Main.Baby.neck == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.neckPos.position;
                                Services.Main.Baby.neck = grabbed;
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                        case "LeftHand":
                            if (Services.Main.Baby.lefthand == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.lefthandPos.position;
                                Services.Main.Baby.lefthand = grabbed;
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                        case "RightHand":
                            if (Services.Main.Baby.righthand == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.righthandPos.position;
                                Services.Main.Baby.righthand = grabbed;
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                        case "Leg":
                            if (Services.Main.Baby.leg == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.legPos.position;
                                Services.Main.Baby.leg = grabbed;
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                        case "Body":
                            if (Services.Main.Baby.body == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.bodyPos.position;
                                Services.Main.Baby.body = grabbed;
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                        case "BodyOverlay":
                            if (Services.Main.Baby.bodyoverlay == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.bodyoverlayPos.position;
                                Services.Main.Baby.bodyoverlay = grabbed;
                                if (grabbed.tag == "Canada")
                                {
                                    grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.JacketCanadaSprites[1];
                                }
                                else
                                {
                                    grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.JacketUSASprites[1];
                                }
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                        case "LeftFoot":
                            if (Services.Main.Baby.leftfoot == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.leftfootPos.position;
                                Services.Main.Baby.leftfoot = grabbed;
                                if (grabbed.tag == "Canada")
                                {
                                    grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockLCanadaSprites[1];
                                }
                                else
                                {
                                    grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockLUSASprites[1];
                                }
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                        case "RightFoot":
                            if (Services.Main.Baby.rightfoot == null) //if it is empty
                            {
                                grabbed.transform.position = Services.Main.rightfootPos.position;
                                Services.Main.Baby.rightfoot = grabbed;
                                if (grabbed.tag == "Canada")
                                {
                                    grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockRCanadaSprites[1];
                                }
                                else
                                {
                                    grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockRUSASprites[1];
                                }
                                PutGrabbedOn();
                            }
                            else
                            {
                                TakeGrabbedOff();
                            }
                            break;
                    }
                }
                else //just dropping it / disrobing the baby
                {
                    string layer = grabbed.GetComponent<Clothes>().originalLayerName;
                    switch (layer)
                    {
                        case "Head":
                            if (Services.Main.Baby.head == grabbed)
                            {
                                Services.Main.Baby.head = null;
                            }
                            break;
                        case "Neck":
                            if (Services.Main.Baby.neck == grabbed)
                            {
                                Services.Main.Baby.neck = null;
                            }
                            break;
                        case "LeftHand":
                            if (Services.Main.Baby.lefthand == grabbed)
                            {
                                Services.Main.Baby.lefthand = null;
                            }
                            break;
                        case "RightHand":
                            if (Services.Main.Baby.righthand == grabbed)
                            {
                                Services.Main.Baby.righthand = null;
                            }
                            break;
                        case "Leg":
                            if (Services.Main.Baby.leg == grabbed)
                            {
                                Services.Main.Baby.leg = null;
                            }
                            break;
                        case "Body":
                            if (Services.Main.Baby.body == grabbed)
                            {
                                Services.Main.Baby.body = null;
                            }
                            break;
                        case "BodyOverlay":
                            if (grabbed.tag == "Canada")
                            {
                                grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.JacketCanadaSprites[0];
                            }
                            else
                            {
                                grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.JacketUSASprites[0];
                            }
                            if (Services.Main.Baby.bodyoverlay == grabbed)
                            {
                                Services.Main.Baby.bodyoverlay = null;
                            }
                            break;
                        case "LeftFoot":
                            if (grabbed.tag == "Canada")
                            {
                                grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockLCanadaSprites[0];
                            }
                            else
                            {
                                grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockLUSASprites[0];
                            }
                            if (Services.Main.Baby.leftfoot == grabbed)
                            {
                                Services.Main.Baby.leftfoot = null;
                            }
                            break;
                        case "RightFoot":
                            if (grabbed.tag == "Canada")
                            {
                                grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockRCanadaSprites[0];
                            }
                            else
                            {
                                grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockRUSASprites[0];
                            }
                            if (Services.Main.Baby.rightfoot == grabbed)
                            {
                                Services.Main.Baby.rightfoot = null;
                            }
                            break;
                    }
                    TakeGrabbedOff();

                }
            }
        }
    }

    void PutGrabbedOn()
    {
        grabbed.transform.SetParent(Services.Main.transform);
        grabbed.GetComponent<Clothes>().onTheBaby = true;
        grabbed.GetComponent<SpriteRenderer>().sortingLayerName = grabbed.GetComponent<Clothes>().originalLayerName;
        grabbed = null;
    }

    void TakeGrabbedOff()
    {
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
        grabbed.GetComponent<SpriteRenderer>().sortingLayerName = grabbed.GetComponent<Clothes>().originalLayerName;
        grabbed = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Baby")
        {
            /* if(grabbing && grabbed != null){
                grabbed.transform.position = grabbedOriginalPos;
                 grabbed.transform.localScale = new Vector3(3, 3, 1);
                 grabbed = null;
             }*/
        }
        else
        {
            /*if ((playername == "P1" && collision.tag == "Canada") ||
               (playername == "P2" && collision.tag == "USA"))
            {*/
            if (grabbing && grabbed == null)
            {
                grabbed = collision.gameObject;
                grabbed.transform.localScale = new Vector3(6, 6, 1);

                // grabbed.GetComponent<SpriteRenderer>().sortingOrder = 100;
                grabbed.GetComponent<SpriteRenderer>().sortingLayerName = "Grabbed";

                grabbed.transform.SetParent(transform);

                string layer = grabbed.GetComponent<Clothes>().originalLayerName;
                switch (layer)
                {
                    case "Head":
                        if(Services.Main.Baby.head == grabbed){
                            Services.Main.Baby.head = null;
                        }
                        break;
                    case "Neck":
                        if (Services.Main.Baby.neck == grabbed)
                        {
                            Services.Main.Baby.neck = null;
                        }
                        break;
                    case "LeftHand":
                        if (Services.Main.Baby.lefthand == grabbed)
                        {
                            Services.Main.Baby.lefthand = null;
                        }
                        break;
                    case "RightHand":
                        if (Services.Main.Baby.righthand == grabbed)
                        {
                            Services.Main.Baby.righthand = null;
                        }
                        break;
                    case "Leg":
                        if (Services.Main.Baby.leg == grabbed)
                        {
                            Services.Main.Baby.leg = null;
                        }
                        break;
                    case "Body":
                        if (Services.Main.Baby.body == grabbed)
                        {
                            Services.Main.Baby.body = null;
                        }
                        break;
                    case "BodyOverlay":
                        if (grabbed.tag == "Canada")
                        {
                            grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.JacketCanadaSprites[0];
                        }
                        else
                        {
                            grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.JacketUSASprites[0];
                        }
                        if (Services.Main.Baby.bodyoverlay == grabbed)
                        {
                            Services.Main.Baby.bodyoverlay = null;
                        }
                        break;
                    case "LeftFoot":
                        if (grabbed.tag == "Canada")
                        {
                            grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockLCanadaSprites[0];
                        }
                        else
                        {
                            grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockLUSASprites[0];
                        }
                        if (Services.Main.Baby.leftfoot == grabbed)
                        {
                            Services.Main.Baby.leftfoot = null;
                        }
                        break;
                    case "RightFoot":
                        if (grabbed.tag == "Canada")
                        {
                            grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockRCanadaSprites[0];
                        }
                        else
                        {
                            grabbed.GetComponent<SpriteRenderer>().sprite = Services.Sprites.SockRUSASprites[0];
                        }
                        if (Services.Main.Baby.rightfoot == grabbed)
                        {
                            Services.Main.Baby.rightfoot = null;
                        }
                        break;
                }
            }
            //}
        }
    }
}
