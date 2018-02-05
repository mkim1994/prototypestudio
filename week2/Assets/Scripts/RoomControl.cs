using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour {
    float speed = 30f;
    public GameObject goal;
	// Use this for initialization

    void Awake(){
        goal.SetActive(false);
    }
	void Start () {
        StartCoroutine(WaitAndActive(1f));
	}
	
	// Update is called once per frame
	void Update () {
       /* if (Input.GetMouseButton(0))
        {*/
            OnMouseDrag();
       // }

	}

    IEnumerator WaitAndActive(float sec){
        yield return new WaitForSeconds(sec);
        goal.SetActive(true);
    }

    void OnMouseDrag(){
        /*float h = speed * Input.GetAxis("Mouse X");
        float v = speed * Input.GetAxis("Mouse Y");
        transform.Rotate(v, h, 0);*/

        float h = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;
        float v = Input.GetAxis("Mouse Y") * speed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.down,h);
        transform.Rotate(Vector3.right,v);
    }


}
