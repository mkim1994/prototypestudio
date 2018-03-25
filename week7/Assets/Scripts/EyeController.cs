using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour {

    public GameObject fish;

    public GameObject[] eyes;

    private Vector3 eye2offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < eyes.Length; ++i){
            eyes[i].transform.LookAt(fish.transform);
        }
	}
}
