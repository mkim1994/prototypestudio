using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using Yarn.Unity;

public class Friend : MonoBehaviour {

	//this is where we decide which script to play?

	public string characterName = "";

	[FormerlySerializedAs("startNode")]
	public string talkToNode = "";

	[Header("Optional")]
	public TextAsset scriptToLoad;


	// Use this for initialization
	void Start () {
		if (scriptToLoad != null) {
			FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
		}

	}

	// Update is called once per frame
	void Update () {

	}
}