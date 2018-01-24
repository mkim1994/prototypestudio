using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFollow : MonoBehaviour {

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public float minX, maxX, minY, maxY;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = new Vector3(Mathf.Clamp(mousePosition.x, minX, maxX),
                                    Mathf.Clamp(mousePosition.y, minY, maxY), 0);
        int r1 = Random.Range(0, 3);
        int r2 = Random.Range(0, 3);
        switch(r1){
            case 0:
                r1 = -1;
                break;
            case 1:
                r1 = 0;
                break;
            case 2:
                r1 = 1;
                break;
        }
        switch(r2){
            case 0:
                r2 = -1;
                break;
            case 1:
                r2 = 0;
                break;
            case 2:
                r2 = 1;
                break;
        }
        //Debug.Log(Mathf.PerlinNoise(mousePosition.x, mousePosition.y));
        // transform.position = mousePosition;
        transform.position = new Vector3(
            Mathf.PerlinNoise(mousePosition.x, mousePosition.y)*0.05f * r1 + mousePosition.x,
            Mathf.PerlinNoise(mousePosition.x, mousePosition.y)*0.05f * r2 + mousePosition.y,
            0);
        


        //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
       // Mathf.Clamp(transform.position.x, minX, maxX);
       // Mathf.Clamp(transform.position.y, minY, minX);
	}

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("TOUCHING");

    }
}
