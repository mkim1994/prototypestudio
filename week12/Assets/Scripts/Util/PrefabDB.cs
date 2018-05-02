using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject {
    [SerializeField]
    private GameObject[] scenes;
    public GameObject[] Scenes { get { return scenes; } }

    [SerializeField]
    private GameObject npc;
    public GameObject NPC { get { return npc; } }

    [SerializeField]
    private GameObject textObj;
    public GameObject TextObj{ get { return textObj; }}

}
