using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkControl : MonoBehaviour{
    private Vector3 mousePosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        /*mousePosition = new Vector3(Mathf.Clamp(mousePosition.x, -10f, 10f),
                                    Mathf.Clamp(mousePosition.y, -10f, 10f),
                                    Mathf.Clamp(mousePosition.z, -10f, 10f));*/
        transform.position = mousePosition;
        //mousePosition -= lastMousePos;
	}
}
