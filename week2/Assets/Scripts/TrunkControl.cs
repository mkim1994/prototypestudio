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
        transform.position = mousePosition;
        //mousePosition -= lastMousePos;
	}
}
