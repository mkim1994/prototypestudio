using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{

    private DialogueRunner dialogue;
    private DialogueVariableStorage storage;
    private DialogueUI dialogueui;

    private string prevNode;
    private string stayingNode;
    public List<string> visitedNodes;

    // Use this for initialization
    void Start()
    {
        dialogue = Services.Main.dialogue;
        storage = Services.Main.dialogue.GetComponent<DialogueVariableStorage>();
        dialogueui = Services.Main.dialogue.GetComponent<DialogueUI>();
        Services.EventManager.Register<HandTouchedEvent>(HandTouchedEvent);
        Services.EventManager.Register<HandRejectedEvent>(HandRejectedEvent);

        visitedNodes = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if $gettingthehint is over 3, tell armfollow that touching is fine now
		 * use eventmanager on armfollow, broadcast when you touch
		 * depending on your $busted value it'll send you to the appropriate dialogue branch
		 * 
            */
        /*if (dialogue.currentNodeName != stayingNode)
        {
            Debug.Log("staying node");
            if (visitedNodes.Contains(dialogue.currentNodeName))
            {
                stayingNode = dialogue.currentNodeName;
                Debug.Log("setting true");
                storage.SetValue("$hasvisited", new Yarn.Value(true));
            }
            else
            {
                Debug.Log("setting variable false");
                storage.SetValue("$hasvisited", new Yarn.Value(false));
            }
        }*/
    }

    void HandTouchedEvent(EventE e)
    {
        int bustedVal = (int)storage.GetValue("$busted").AsNumber;
        prevNode = dialogue.currentNodeName;

        dialogue.StartDialogue("Busted" + bustedVal);


    }

    void HandRejectedEvent(EventE e)
    {

        //storage.SetValue("$busted", new Yarn.Value(storage.GetValue("$busted").AsNumber + 1f));
        //dialogue.StartDialogue(prevNode);
        if (storage.GetValue("$busted").AsNumber > 3f)
        {
            dialogue.Stop();

            Services.SceneStackManager.Swap<EndScreen>();
        } else{
            dialogue.StartDialogue(prevNode);
        }

    }

    [YarnCommand("rejecthand")]
    public void RejectHand()
    {
        Services.EventManager.Fire(new HandRejectedEvent());
    }

    [YarnCommand("visit")]
    public void Visit(){
        return;
    }
}
