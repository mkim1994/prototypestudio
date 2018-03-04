using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject {
    [SerializeField]
    private GameObject[] scenes;
    public GameObject[] Scenes { get { return scenes; } }

    [SerializeField]
    private GameObject space;
    public GameObject Space { get { return space; }}

    [SerializeField]
    private GameObject point;
    public GameObject Point { get { return point; }}

    [SerializeField]
    private GameObject curvedline;
    public GameObject CurvedLine { get { return curvedline; }}

}
