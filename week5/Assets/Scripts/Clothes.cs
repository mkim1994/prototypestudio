using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : MonoBehaviour {

    public bool collidingWithBaby;
    public bool onTheBaby;

    public Vector2 originalPos;

	// Use this for initialization
	void Start () {
        collidingWithBaby = false;
        onTheBaby = false;
        originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Baby")
        {
            collidingWithBaby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Baby"){
            collidingWithBaby = false;
        }
    }
}
