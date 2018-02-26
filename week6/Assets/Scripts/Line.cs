using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    [HideInInspector]
    public GameObject[] MainLine;

    public int size;
    private float offset = 0.5f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateSpaces(){
        MainLine = new GameObject[size];
        for (int i = 0; i < size; ++i){
            MainLine[i] = Instantiate(Services.Prefabs.Space,
                                                 new Vector2(0, i*offset), Services.Prefabs.Space.transform.rotation);
            MainLine[i].transform.SetParent(Services.Main.transform);
        }
    }
}

