using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraControl : MonoBehaviour {

    private FirstPersonController fps;
    public LayerMask everything;
    public LayerMask other;
    private Camera cam;

	// Use this for initialization
	void Start () {
        fps = GetComponentInParent<FirstPersonController>();
        cam = GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Tab)){ //animate eyes closing
            if(cam.cullingMask == everything){
                cam.cullingMask = other;
            } else{
                cam.cullingMask = everything;
            }
        }
        else if(Input.GetKey(KeyCode.Tab)){
            fps.ChangeWalkSpeed(0f);
            cam.cullingMask = other;
        } else if(Input.GetKeyUp(KeyCode.Tab)){
            fps.ChangeWalkSpeed(5f);
            cam.cullingMask = everything;
        }
	}
}
