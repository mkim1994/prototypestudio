using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArmFollow : MonoBehaviour {

    private Vector3 mousePosition;
    public Vector3 defaultPos;
    public float moveSpeed = 0.1f;
    public float minX, maxX, minY, maxY;
    public Vector3 lastMousePos;
    public bool handBackOff;
    public GameObject death;
	// Use this for initialization
	void Start () {
        DOTween.Init();
        lastMousePos = new Vector3(0, 0, 0);
        handBackOff = false;
        Services.EventManager.Register<HandTouchedEvent>(HandTouchedEvent);
        Services.EventManager.Register<HandRejectedEvent>(HandRejectedEvent);
        mousePosition = defaultPos;
	}
	
	// Update is called once per frame
	void Update () {
        if (!Services.Main.dialogue.variableStorage.GetValue("$chance").AsBool)
        {
            death.SetActive(true);
        }
        else
        {
            death.SetActive(false);
            if (!handBackOff)
            {
                mousePosition += new Vector3(Input.GetAxis("Mouse X"),
                                             Input.GetAxis("Mouse Y"), 0);

                /* mousePosition = Input.mousePosition;
                 mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                 mousePosition -= lastMousePos;*/
                mousePosition = new Vector3(Mathf.Clamp(mousePosition.x, minX, maxX),
                                            Mathf.Clamp(mousePosition.y, minY, maxY), 0);
                int r1 = Random.Range(0, 3);
                int r2 = Random.Range(0, 3);
                switch (r1)
                {
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
                switch (r2)
                {
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
                // transform.position = mousePosition;
                transform.position = new Vector3(
                    Mathf.PerlinNoise(mousePosition.x, mousePosition.y) * 0.05f * r1 + mousePosition.x,
                    Mathf.PerlinNoise(mousePosition.x, mousePosition.y) * 0.05f * r2 + mousePosition.y,
                    0);

            }
        }

	}

    void OnTriggerEnter2D(Collider2D other){
        Services.EventManager.Fire(new HandTouchedEvent());

    }

    void HandTouchedEvent(EventE e){
        if ((int)Services.Main.dialogue.variableStorage.GetValue("$gettingthehint").AsNumber == 3)
        {
            Services.GameManager.dateSuccess = true;
        }
        else
        {
            handBackOff = true;
        }
    }


    void HandRejectedEvent(EventE e)
    {
        transform.DOMove(defaultPos, 1f).OnComplete(() => HandFinishedBackingOff());
    }

    void HandFinishedBackingOff(){
        mousePosition = defaultPos;
        /*   lastMousePos = Input.mousePosition;
           lastMousePos = Camera.main.ScreenToWorldPoint(lastMousePos);
           lastMousePos = new Vector3(Mathf.Clamp(mousePosition.x, minX, maxX),
                                       Mathf.Clamp(mousePosition.y, minY, maxY), 0);
           lastMousePos -= new Vector3(3f, -4.5f, 0);*/

        Services.Main.dialogue.variableStorage.SetValue("$chance", new Yarn.Value(false));
        handBackOff = false;

    }
}
