using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

    public string charName;


    public enum Relationship{
        FriendsWith,
        Likes,
        Dislikes,
        Strangers,
        Acquaintance
    }
    public Relationship[] relationshipStates;
    public NPC[] targets;

    private Text nameText;
    private Text relationText;
    //SpriteRenderer sprite;
    // Use this for initialization
    GameObject info;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeNPC(string name){

        info = transform.GetChild(0).gameObject;
        nameText = info.transform.GetChild(1).GetComponent<Text>();
        relationText = info.transform.GetChild(2).GetComponent<Text>();
       // info.SetActive(false);

        charName = name;
        nameText.text = charName;
    }

    public void SetRelationships(List<NPC> ch, Relationship[] state){
        targets = new NPC[ch.Count];
        for (int j = 0; j < ch.Count; ++j){
            targets[j] = ch[j];
        }
        relationshipStates = new Relationship[state.Length];
        for (int k = 0; k < relationshipStates.Length; ++k){
            relationshipStates[k] = state[k];
        }
        string relation = "";
        for (int i = 0; i < relationshipStates.Length; ++i)
        {
            
            switch (relationshipStates[i])
            {
                case Relationship.Likes:
                    relation = "Likes " + targets[i].charName;
                    break;
                case Relationship.Acquaintance:
                    relation = "Kind of knows " + targets[i].charName;
                    break;
                case Relationship.Dislikes:
                    relation = "Dislikes " + targets[i].charName;
                    break;
                case Relationship.Strangers:
                    relation = "Doesn't know " + targets[i].charName;
                    break;
                case Relationship.FriendsWith:
                    relation = "Friends with " + targets[i].charName;
                    break;
            }
            if (i == relationshipStates.Length-1)
            {
                relationText.text += relation;
            } else{
                relationText.text += relation + ", ";
            }
        }
    }

	private void OnMouseOver()
	{

        //info.SetActive(true);
	}

	private void OnMouseExit()
	{
       // info.SetActive(false);
	}
}
