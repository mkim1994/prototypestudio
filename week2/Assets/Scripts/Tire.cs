using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class Tire : MonoBehaviour {

    public GameObject winText;
    public GameObject trunk;
	// Use this for initialization
	void Start () {
        DOTween.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Goal"){
            GameObject.FindWithTag("SFX").GetComponent<AudioSource>().Play();
            winText.SetActive(true);
            trunk.transform.DOLocalMoveY(45f,3f);
            StartCoroutine(LoadNewScene());
        }
    }

    IEnumerator LoadNewScene(){
        yield return new WaitForSeconds(3f);
        GameObject.FindWithTag("Fade").GetComponent<Image>().DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);

        if (Services.SceneStackManager.CurrentScene == Services.Main)
        {

            Services.SceneStackManager.PopScene();
            Services.SceneStackManager.PushScene<Main2>();
        }
        else if (Services.SceneStackManager.CurrentScene == Services.Main2)
        {

            Services.SceneStackManager.PopScene();
            Services.SceneStackManager.PushScene<Main3>();
        }
        else if (Services.SceneStackManager.CurrentScene == Services.Main3)
        {

            Services.SceneStackManager.PopScene();
            Services.SceneStackManager.PushScene<Main4>();
        }
        else if (Services.SceneStackManager.CurrentScene == Services.Main4)
        {

            Services.SceneStackManager.PopScene();
            Services.SceneStackManager.PushScene<TitleScreen>();
        }

    }

}
