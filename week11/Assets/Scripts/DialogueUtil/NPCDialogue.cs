
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using Yarn.Unity;
/// attached to the non-player characters, and stores the name of the
/// Yarn node that should be run when you talk to them.
public class NPCDialogue : MonoBehaviour
{

    public string characterName = "";

    [FormerlySerializedAs("startNode")]
    public string talkToNode = "";

    [Header("Optional")]
    public TextAsset scriptToLoad;

    // Use this for initialization
    void Start()
    {
        if (scriptToLoad != null)
        {
            FindObjectOfType<DialogueRunner>().AddScript(scriptToLoad);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}


