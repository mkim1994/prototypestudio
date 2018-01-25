using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour {

    private DialogueRunner dialogue;
    private DialogueVariableStorage storage;
    private DialogueUI dialogueui;

	// Use this for initialization
    void Start () {
        dialogue = Services.Main.dialogue;
        storage = Services.Main.dialogue.GetComponent<DialogueVariableStorage>();
        dialogueui = Services.Main.dialogue.GetComponent<DialogueUI>();
        Services.EventManager.Register<HandTouchedEvent>(HandTouchedEvent);
	}
	
	// Update is called once per frame
	void Update () {
		/*if $gettingthehint is over 3, tell armfollow that touching is fine now
		 * use eventmanager on armfollow, broadcast when you touch
		 * depending on your $busted value it'll send you to the appropriate dialogue branch
		 * 
            */
	}

    void HandTouchedEvent(EventE e){
        Debug.Log("TOUCHING FOR REAL");
    }
}
