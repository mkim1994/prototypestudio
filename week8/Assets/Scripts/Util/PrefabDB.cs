using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject {
    [SerializeField]
    private GameObject[] scenes;
    public GameObject[] Scenes { get { return scenes; } }

    [SerializeField]
    private GameObject leftFoot;
    public GameObject LeftFoot{ get { return leftFoot; }}

    [SerializeField]
    private GameObject rightFoot;
    public GameObject RightFoot { get { return rightFoot; } }

}
