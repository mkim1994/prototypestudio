using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour {

    float speed = 0.02f;
   // float speed = 0.1f;

    SpriteRenderer sp;
    bool isNotTouching;
    float timeSinceLastTouched;
    float elapsedTime;
   // Color originalColor;
    Color currentColor;
    Color nextColor;
    public Color noon, afternoon, evening, night, rlynight, dawn, morning;
    //hang onto night a bit
    float startTime;

    bool canAccess;

	// Use this for initialization
	void Start () {
        sp = GetComponent<SpriteRenderer>();
        //noon = sp.color;
        startTime = Time.time;
        currentColor = noon;
        nextColor = afternoon;
        canAccess = true;

	}
	
	// Update is called once per frame
	void Update () {
        ChangeColor();
        Color tmp = new Color(sp.color.r, sp.color.g, sp.color.b, 0f);
        Color tmp2 = new Color(nextColor.r, nextColor.g, nextColor.b, 0f);
        Color tmp3 = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);

        if (tmp == tmp2)
        {
            startTime = Time.time;
            if (nextColor == afternoon)
            {
                currentColor = afternoon;
                nextColor = evening;
            }
            else if (nextColor == evening)
            {
                currentColor = evening;
                nextColor = night;
            }
            else if (nextColor == night)
            {
                currentColor = night;
                nextColor = rlynight;
            } else if(nextColor == rlynight){
                currentColor = rlynight;
                nextColor = dawn;
            }
            else if (nextColor == dawn)
            {
                currentColor = dawn;
                nextColor = morning;
            }
            else if (nextColor == morning)
            {
                currentColor = morning;
                nextColor = noon;
            }
            else if (nextColor == noon)
            {
                currentColor = noon;
                nextColor = afternoon;
            }
        }



        if(isNotTouching){
            elapsedTime = Time.time - timeSinceLastTouched;
            if(elapsedTime > 400f){ //after 5 ish mins
                if (sp.color.a < 1f)
                {
                    sp.color = new Color(sp.color.r, sp.color.g, sp.color.b,
                                         sp.color.a+0.01f);
                }
            }
        }


        
	}

    void ChangeColor(){
        //change color
        //lerp between the currentcolor and the nextcolor.
        float t = (Time.time - startTime) * speed;
        sp.color = Color.Lerp(
            new Color(currentColor.r,
                      currentColor.g,
                      currentColor.b,
                      sp.color.a),
            new Color(nextColor.r,
                      nextColor.g,
                      nextColor.b,
                      sp.color.a), t);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{

        if(collision.tag=="Player"){
            isNotTouching = false;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 
                                 Mathf.Clamp(sp.color.a-0.05f,0f,1f));
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{

        if (collision.tag == "Player")
        {
            timeSinceLastTouched = Time.time;
            elapsedTime = timeSinceLastTouched;
            isNotTouching = true;
        }
	}
}
