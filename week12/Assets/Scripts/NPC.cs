using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

    public string charName;
    public Dictionary<NPC,string> toRespondTo = new Dictionary<NPC, string>();

    public enum Relationship{
        FriendsWith,
        Likes,
        Dislikes,
        Strangers,
        Acquaintance
    }
    public Relationship[] relationshipStates;
    public NPC[] targets;

    public int[] points;

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
        points = new int[ch.Count];
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

    public void PublishRelationship(){
        relationText.text = "";
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
            if (i == relationshipStates.Length - 1)
            {
                relationText.text += relation;
            }
            else
            {
                relationText.text += relation + ", ";
            }
        }
    }

    public void EvalRelationship(){
        for (int i = 0; i < relationshipStates.Length; ++i){
            switch(relationshipStates[i]){
                case Relationship.Acquaintance:
                    if(points[i] > 2){
                        points[i] = 0;
                        relationshipStates[i] = Relationship.FriendsWith;
                    } else if(points[i] < -2){

                        points[i] = 0;
                        relationshipStates[i] = Relationship.Dislikes;
                    }
                    break;
                case Relationship.Dislikes:
                    if (points[i] > 5)
                    {

                        points[i] = 0;
                        relationshipStates[i] = Relationship.Acquaintance;
                    }
                    break;
                case Relationship.Strangers:
                    if (points[i] > 3)
                    {

                        points[i] = 0;
                        relationshipStates[i] = Relationship.Acquaintance;
                    } else if(points[i] < -1){

                        points[i] = 0;
                        relationshipStates[i] = Relationship.Dislikes;
                    }
                    break;
                case Relationship.FriendsWith:
                    if(points[i] > 3){

                        points[i] = 0;
                        relationshipStates[i] = Relationship.Likes;
                    } else if(points[i]< -2){

                        points[i] = 0;
                        relationshipStates[i] = Relationship.Acquaintance;
                    }
                    break;
            }
        }
        PublishRelationship();
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
