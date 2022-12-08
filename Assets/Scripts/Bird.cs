using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundListener))]
public class Bird : MonoBehaviour {

    public float upForce;                    //Upward force of the "flap".
    private int lives = 2;
    private KeyCode key = KeyCode.Space;

    private Animator anim;                    //Reference to the Animator component.
    private Rigidbody2D rb2d;                //Holds a reference to the Rigidbody2D component of the bird.
    private SoundListener _soundListener;

    void Start()
    {
        //Get reference to the Animator component attached to this GameObject.
        anim = GetComponent<Animator>();
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();
        //Get and store the sound listener component (reference to microphone)
        _soundListener = GetComponent<SoundListener>();
    }

    void Update()
    {
        //Don't allow control if the bird has died.
        if (lives > 0)
        {
            //Look for input to trigger a "flap".
            if (_soundListener.Listen())
            {
                //...tell the animator about it and then...
                anim.SetTrigger("Clap");
                //...zero out the birds current y velocity before...
                rb2d.velocity = Vector2.zero;
                //    new Vector2(rb2d.velocity.x, 0);
                //..giving the bird some upward force.
                rb2d.AddForce(new Vector2(0, upForce));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Zero out the bird's velocity
        rb2d.velocity = Vector2.zero;
        // If the bird collides with something set it to dead...
        lives -= 1;
        if(lives == 0)
        {
            anim.SetTrigger("Die");
            //...and tell the game control about it.
            GameControl.instance.BirdDied();
        }
        else
        {
            anim.SetTrigger("Dmg");
        } 
        
    }

}
