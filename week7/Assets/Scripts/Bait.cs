using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Bait : MonoBehaviour {
    /*range: -130 to 15*/
    // Use this for initialization
    public Vector3 rotChoice;
    bool beingDestroyed;
    TextMeshPro tmp;

    public bool goodThought; //false = bad thought
	void Start () {
        DOTween.Init();

        rotChoice = rotChoice * 0.5f;
        beingDestroyed = false;
        goodThought = false;

        float r = Random.Range(0f, 1f);

        tmp = GetComponentInChildren<TextMeshPro>();
        if (Services.GameManager.dudeBathroom)
        {
            if (Services.Main.goodThoughts == 0)
            {

                tmp.text = "NOT A MAN";
                Services.Main.goodThoughts++;
                goodThought = true;
            }
            else
            {
                if (r > 0.5f)
                {
                    tmp.text = "NOT A MAN";
                    Services.Main.goodThoughts++;
                    goodThought = true;
                }
                else
                {
                    tmp.text = "i'm a man";
                }
            }

        }
        else
        {
            if (Services.Main.goodThoughts == 0) { 
                tmp.text = "NOT A WOMAN";
                Services.Main.goodThoughts++;
                goodThought = true;
            }
            else
            {
                if (r > 0.5f)
                {
                    tmp.text = "NOT A WOMAN";
                    Services.Main.goodThoughts++;
                    goodThought = true;
                }
                else
                {
                    tmp.text = "i'm a woman";
                }
            }
        }


        StartCoroutine(WaitToDrop());



	}
	
	// Update is called once per frame
	void Update () {
        if (beingDestroyed)
        {

        }
        else
        {
            transform.Rotate(rotChoice);
        }
	}
    IEnumerator WaitToDrop(){
        yield return new WaitForSeconds(Random.Range(1f,3f));
        DropBait(3f);
    }

    public void EnableMesh(bool enable){
        if(enable){
            foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()){
                mr.enabled = true;
            }
        } else{
            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = false;
            }

            tmp.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void DropBait(float duration){
        transform.DOMoveY(Random.Range(-110f, 15f), duration).SetEase(Ease.InOutBack);
    }

	private void OnTriggerEnter(Collider other)
	{
        if (!beingDestroyed)
        {
            if (other.tag == "fish")
            {
                if(!goodThought){
                    FindObjectOfType<Underwater>().currentFogDensity = FindObjectOfType<Underwater>().currentFogDensity * 0.2f + FindObjectOfType<Underwater>().currentFogDensity;
                    Services.GameManager.currentCamera.DOShakePosition(0.5f);
                }
                
                GetComponent<BoxCollider>().enabled = false;
                beingDestroyed = true;
                transform.DOScale(0f, 2f).SetEase(Ease.InOutBack).OnComplete(()=>OnCompleteScale());
            }
        }
	}

    private void OnCompleteScale(){
        Services.Main.goodThoughts--;
        Services.Main.baits.Remove(this);
        Destroy(this.gameObject);
    }




}
