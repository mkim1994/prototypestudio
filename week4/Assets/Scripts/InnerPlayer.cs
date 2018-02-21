using UnityEngine;
using System.Collections;

public class InnerPlayer : MonoBehaviour {
	private Rigidbody2D rb;
	public float moveSpeed = 6;
	private float drag = 0.8f;
	private float acceleration = 200.0f;
	private float maxspeed = 5f;
	private float minspeed = -5f;


	new AudioSource audio;
	public AudioClip allyyay;
	public AudioClip allyno;
	public AudioClip enemytouch;

	public Sprite pseudosprite;
	public Sprite altsprite;



	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();

		audio = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {

        //set max speed
        //make it so that the animation matches up to how much you press it

        //int speedMultiplier = 10; 

        if (Services.Main.chatManager.chatIsHidden)
        {

            float xinput = Input.GetAxis("Horizontal");
            Vector2 newVel = rb.velocity;
            newVel.x *= drag;
            newVel.x += xinput * acceleration * Time.fixedDeltaTime;
            newVel.x = Mathf.Clamp(newVel.x, minspeed, maxspeed);
            newVel.y = 0f;
            rb.velocity = newVel;
            int rounded = (int)(newVel.x);

            float switchinput = Input.GetAxis("Switch");
            int rounded2 = (int)(switchinput);

            if (rounded2 > 0)
            {
                GetComponent<SpriteRenderer>().sprite = altsprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = pseudosprite;
            }
        }


	}


	//press tab to saveally
	public void AllyTouched(){
		audio.clip = allyyay;
		audio.Play ();
	}
	public void AllyHurt(){
		audio.clip = allyno;
		audio.Play ();
	}

	public void AllyGround(){
		//audio.clip = allyno;
		//audio.Play ();
	}

	public void EnemyTouched(){
		audio.clip = enemytouch;
		audio.Play ();
	}

	public void EnemyHurt(){
		audio.clip = allyno;
		audio.Play ();
	}
}
