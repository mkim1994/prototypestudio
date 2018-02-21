using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject {
    [SerializeField]
    private GameObject[] scenes;
    public GameObject[] Scenes { get { return scenes; } }

    [SerializeField]
    private GameObject chatline;
    public GameObject ChatLine { get { return chatline; } }

    [SerializeField]
    private GameObject spawnerprefab;
    public GameObject SpawnerPrefab { get { return spawnerprefab; }}

    [SerializeField]
    private GameObject walls;
    public GameObject WallsPrefab { get { return WallsPrefab; }}

    [SerializeField]
    private GameObject ally;
    public GameObject Ally { get { return ally; }}

    [SerializeField]
    private GameObject enemy;
    public GameObject Enemy { get { return enemy;  }}
}
