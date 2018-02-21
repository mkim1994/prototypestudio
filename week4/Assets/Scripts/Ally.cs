using UnityEngine;
using System.Collections;

public class Ally : MonoBehaviour {

	GameObject player;

    private float originalgravity = 0.05f;
    private float gravity = 0.05f;


	//new AudioSource audio;

	// Start() is called at the beginning of the game
	void Start() {
        player = GameObject.FindWithTag("InnerPlayer"); //fill player Variable with reference to Player

		//int r = Random.Range (0, 2);
		if (transform.position.y > 0f) {
			gravity = -originalgravity;
		} else {
			gravity = originalgravity;
			GetComponent<SpriteRenderer> ().flipY = true;
		}

		//audio = GetComponent<AudioSource> ();
	}

	void Update(){
		transform.position = new Vector2 (transform.position.x, transform.position.y+gravity);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Ground"){
			//And tell the scoreManager & player that the player missed a hat
            Services.Main.score.SendMessage("AllyGround");
			//player.SendMessage("HatMissed");
			//Then destroy the hat, destroy needs to be sent a game object, which we can get from this.gameObject
			player.SendMessage("AllyGround");

			Destroy(this.gameObject);
		}
		//check to see if the colliding object had the tag 'Player'
		if (collision.collider.tag == "InnerPlayer"){
			//Tell the scoreManager & player that the player missed a hat

			if (collision.collider.gameObject.GetComponent<SpriteRenderer> ().sprite ==
			    collision.collider.gameObject.GetComponent<InnerPlayer> ().altsprite) {
                Services.Main.score.SendMessage ("AllyTouched");
				player.SendMessage ("AllyTouched");

				Destroy(this.gameObject);
			} else { //nonswitched mode, touched
                Services.Main.score.SendMessage("AllyHurt");
				player.SendMessage ("AllyHurt");

			}

		}
	}
}
