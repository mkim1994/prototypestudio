using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour {

    float waterLevel;
    //public bool isUnderwater;
    public Color normalColor;
    public Color underwaterColor;

    public float currentFogDensity;
    private float maxFogDensity = 0.0030f; //0.0024f was a good amount before though
	// Use this for initialization
	void Start () {

        RenderSettings.fogColor = underwaterColor;
        currentFogDensity = 0f;

       // Services.Main.ParticleSystem.SetActive(false);
        Services.Main.ParticleSystem.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {
        if (!Services.Main.outlineMode)
        {
            /*if (isUnderwater)
            {
               // Services.Main.StopFloor(true);
                Services.Main.ParticleSystem.SetActive(true);
                SetUnderWater();

            }
            else
            {


                Services.Main.ParticleSystem.SetActive(false);
                SetNormal();
            }*/
        }

        if (currentFogDensity < maxFogDensity)
        {
            currentFogDensity += 0.0001f*Time.deltaTime;
          //  Debug.Log(currentFogDensity);
            RenderSettings.fogDensity = currentFogDensity;

        } else{
            if (!Services.Main.win.activeSelf)
            {
                Services.Main.lose.SetActive(true);

            }
        }

	}

    void SetNormal(){
        if (currentFogDensity > 0f)
        {
            //RenderSettings.fogDensity = 0f;
            currentFogDensity -= 0.00001f;
            RenderSettings.fogDensity = currentFogDensity;
        } else{
            //Services.Main.StopFloor(false);
        }
    }

    void SetUnderWater(){
        if (currentFogDensity < 0.0024f)
        {
            currentFogDensity += 0.00001f;

            Debug.Log(currentFogDensity);
            RenderSettings.fogDensity = currentFogDensity;
        } else{

        }
    }

}
