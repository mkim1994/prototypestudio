using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnim : MonoBehaviour {

	[SerializeField] GameObject body1, body2, body3, body4;

	List<GameObject> fishParts = new List <GameObject> ();


	public float speed = 3;
	public float amplitudeX = 10.0f;
	float amplitudeY = 5.0f;
	public float omegaX = 1.0f;
	float omegaY = 5.0f;

	public float frequency;
	public float amplitude;
	public float phase = 0;
	float currentSpeed;
	Vector3 currentAngularVel;
	public float angularBend;


	public float index1 = 0, index2 = 1, index3 = 2,index4 = 3;


	float x1,x2,x3,x4;

	float y;
	float yRot;

	float rot1,rot2,rot3,rot4;

	float y1,y2,y3,y4;

	float vel;

    void Awake(){
        transform.parent.transform.localPosition = new Vector3(-0.027f, 0.2f, -0.531f);
        transform.parent.transform.localEulerAngles = new Vector3(-48.772f, 180f, 0f);
    }
	void Start () {
		fishParts.Add (body1);
		fishParts.Add (body2);
		fishParts.Add (body3);
		fishParts.Add (body4);

	}
	

	public void Update(){

		currentSpeed = 2f;
		OldAnim ();







	}



	void OldAnim () {

        vel = currentSpeed * 0.01f +1f;

		index1 += Time.deltaTime ;
		index2 += Time.deltaTime ;
		index3 += Time.deltaTime ;
		index4 += Time.deltaTime ;


		x1 = 1+(vel * 0.3f)*amplitudeX*Mathf.Cos (vel*omegaX*index1);
		x2 = 1+(vel *0.3f)*amplitudeX*Mathf.Cos (vel*omegaX*index2);
		x3 = 1+(vel * 0.3f)*amplitudeX*Mathf.Cos (vel*omegaX*index3);
		x4 = 1+(vel * 0.3f)*amplitudeX*Mathf.Cos (vel*omegaX*index4);

		y = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index1));


		rot1 = x1;


		rot2 = 4*x2;


		rot3 = 3*x3;


		rot4 = -3*x4;

		body1.transform.localRotation = Quaternion.Euler(new Vector3 (0, rot1, 0));

		body2.transform.localRotation = Quaternion.Euler(new Vector3 (0, rot2, 0));

		body3.transform.localRotation = Quaternion.Euler(new Vector3 (0, rot3, 0));

		body4.transform.localRotation = Quaternion.Euler(new Vector3 (0, rot4, 0));

	}

}
