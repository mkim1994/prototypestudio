using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject {
    [SerializeField]
    private GameObject[] scenes;
    public GameObject[] Scenes { get { return scenes; } }


    [SerializeField]
    private GameObject letter;
    public GameObject Letter { get { return letter; }}

    [SerializeField]
    private GameObject otherletter;
    public GameObject OtherLetter { get { return otherletter; } }

    [SerializeField]
    private GameObject panictext;
    public GameObject PanicText { get { return panictext; } }

    [SerializeField]
    private GameObject thought;
    public GameObject Thought { get { return thought; } }

    [SerializeField]
    private GameObject hiddenthought;
    public GameObject HiddenThought { get { return hiddenthought; } }

}
