using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using DG.Tweening;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {

    private FirstPersonController fps;
    public LayerMask everything;
    public LayerMask other;
    private Camera cam;

    private GameObject topeye, bottomeye;
    public GameObject PanicCanvas;

    private float originalEyePosY;

    Sequence closeSeq;
    Sequence openSeq;

    private float eyeTime = 0.5f;
    private float fadeTime = 0.1f;

	// Use this for initialization
	void Start () {
        fps = GetComponentInParent<FirstPersonController>();
        cam = GetComponent<Camera>();
        topeye = GameObject.FindWithTag("TopEye");
        bottomeye = GameObject.FindWithTag("BottomEye");
        originalEyePosY = topeye.transform.position.y;


	}
    void CloseEyes(){
        PanicCanvas.SetActive(false);
        cam.cullingMask = other;
        fps.ChangeWalkSpeed(0f);
        GetComponent<AudioSource>().DOFade(1f, 0.5f);
        cam.backgroundColor = Color.black;
    }

    void OpenEyes(){
        PanicCanvas.SetActive(true);
        fps.ChangeWalkSpeed(5f);
        cam.cullingMask = everything;
        GetComponent<AudioSource>().DOFade(0f, 0.5f);
        cam.backgroundColor = Color.white;
    }
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Tab)){ //animate eyes closing
            if (cam.cullingMask == everything)
            { // close eyes
                openSeq.Kill();
                closeSeq = DOTween.Sequence();
                closeSeq.Append(topeye.transform.DOLocalMoveY(0f, eyeTime));
                closeSeq.Insert(0f,bottomeye.transform.DOLocalMoveY(0f, eyeTime));
                closeSeq.AppendCallback(()=>CloseEyes());
                closeSeq.Append(topeye.GetComponent<Image>().DOFade(0f, fadeTime));
                closeSeq.Insert(eyeTime,bottomeye.GetComponent<Image>().DOFade(0, fadeTime));
                //seq.OnComplete(() => CloseEyes());

            } else{
                cam.cullingMask = everything; 
            }
        }else if(Input.GetKeyUp(KeyCode.Tab)){
            //open eyes
            closeSeq.Kill();
            openSeq= DOTween.Sequence();
            openSeq.Append(topeye.GetComponent<Image>().DOFade(1f, fadeTime));
            openSeq.Insert(0f, bottomeye.GetComponent<Image>().DOFade(1f, fadeTime));

            openSeq.AppendCallback(() => OpenEyes());
            openSeq.Append(topeye.transform.DOLocalMoveY(originalEyePosY, eyeTime));
            openSeq.Insert(fadeTime, bottomeye.transform.DOLocalMoveY(-originalEyePosY, eyeTime));

        }
	}
}
