using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishController : MonoBehaviour {

    public GameObject fish;

    public Vector3 currDest;

    private float speed = 5f;
    public Transform[] locations; //locations[0] is the origin
    private FishAnim fishAnim;
	// Use this for initialization
	void Start () {
        DOTween.Init();
        fishAnim = FindObjectOfType<FishAnim>();
	}

    // Update is called once per frame
    void Update()
    {
        
       // float step = 1f;
       // fish.transform.position = Vector3.MoveTowards(fish.transform.position, locations[0].position, step);
      
    }

    public void MoveToLocation(Vector3 pos){
        // fishAnim.NewAnimSettings();
        if (currDest != pos)
        {
            currDest = pos;
            fish.transform.DOMove(pos, 5f).SetEase(Ease.InOutCubic);
            //  fish.transform.DORotate()
            fish.transform.DOLookAt(pos, 5f);
        }
    }
}
