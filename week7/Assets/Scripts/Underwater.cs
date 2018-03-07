using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour {

    float waterLevel;
    public bool isUnderwater;
    public Color normalColor;
    public Color underwaterColor;

    private float currentFogDensity;
	// Use this for initialization
	void Start () {

        RenderSettings.fogColor = underwaterColor;
        currentFogDensity = 0f;

        Services.Main.ParticleSystem.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!Services.Main.outlineMode)
        {
            if (isUnderwater)
            {
               // Services.Main.StopFloor(true);
                Services.Main.ParticleSystem.SetActive(true);
                SetUnderWater();

            }
            else
            {


                Services.Main.ParticleSystem.SetActive(false);
                SetNormal();
            }
        }
	}

    void SetNormal(){
        if (currentFogDensity > 0f)
        {
            //RenderSettings.fogDensity = 0f;
            currentFogDensity -= 0.0001f;
            RenderSettings.fogDensity = currentFogDensity;
        } else{
            //Services.Main.StopFloor(false);
        }
    }

    void SetUnderWater(){
        if (currentFogDensity < 0.0024f)
        {
            currentFogDensity += 0.0001f;
            RenderSettings.fogDensity = currentFogDensity;
        } else{

        }
    }

}
