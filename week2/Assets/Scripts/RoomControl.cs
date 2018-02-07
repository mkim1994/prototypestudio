using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour {
    float speed = 50f;
    public GameObject goal;
    private float currentAngle;
    // Use this for initialization

    private float angleIncrements = 1f;

    void Awake(){
        goal.SetActive(false);
    }
	void Start () {
        StartCoroutine(WaitAndActive(1f));
	}
	
	// Update is called once per frame
	void Update () {
      /*  if (Input.GetMouseButton(0))
        {
            OnMouseDrag();
        }*/
        OnMouseDrag();

	}

    IEnumerator WaitAndActive(float sec){
        yield return new WaitForSeconds(sec);
        goal.SetActive(true);
    }

    void OnMouseDrag(){
        /*float h = speed * Input.GetAxis("Mouse X");
        float v = speed * Input.GetAxis("Mouse Y");
        transform.Rotate(v, h, 0);*/

       /* float h = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;
        float v = Input.GetAxis("Mouse Y") * speed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.down,h);
        transform.Rotate(Vector3.right,v);*/

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        /*if(currentAngle < angle){
            currentAngle += angleIncrements;
        } else if(currentAngle > angle){
            currentAngle -= angleIncrements;
        }*/
        // currentAngle = Mathf.Lerp(currentAngle,angle,1f);
        //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }



}
