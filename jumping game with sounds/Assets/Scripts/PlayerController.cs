using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // attaching a Rigidbody, like how we attach a GameObject.
    private AudioSource playerAudio;
    public float gravityModifier; // setting the gravity to whatever you want it to be.
    public float jumpForce = 10; // getting rid of the hard code number
    public bool isOnGround = true; // the player is on the ground.
    public bool gameOver = false;
    private Animator playerAnim; //we are calling our "Animator" as playerAnim in this script.
    public ParticleSystem explosionPartacle; // public-ing it so we can drag it in later in unity.
    public ParticleSystem dirtPartacle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
   
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); /* 
                                                 we don't automatically get access to our Rigidbody like transform component so we need to use method GetComponent() to call out our Rigidbody.
                                                 Rigidbody isn't something that's already in our component area, we have to call it out.
                                                 <> : called left and right carets. It's trying to get a type of smth. In this case we are getting a Rigidbody.
                                              */
        playerAnim =GetComponent<Animator>(); //grabbing our "Animator" and setting it to playerAnim.
        Physics.gravity *= gravityModifier; /*
                                              gravityModifier: if it's 0, then you have no gravity, you'll just keep on floating into the sky and never come down.
                                                               if it's 1, the it'll be normal gravity. 
                                              meaning of the whole line: setting the gravity of our physics system and multiple it by our gravityModifier to change how much or little gravity we want in our game.
                                              *=: Physics.gravity= Physics.gravity * gravityModifier;
                                            */
      playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
      // make the player jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) //and if the player is on the ground
        {                                                               //&& "!gameOver" = "gameOver == false" = "gameOver !=true", we used this so that the player won't still be able to jump even after game is over.
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); /* 
                                                                                void Rigidbody.AddForce(Vector3 force, ForceMode mode)
                                                                                ForceMode: can apply different force modes through this.
                                                                                there's 4 different force modes that we can apply to : ex. ForceMode.Impulse: which applies the force to the player immediately.
                                                                                                                                           normal ForceMode: which applies that force over time.
                                                                                Why we used 10, we need less force cause we're not trying to apply force over time but right now, so it immediately gets to 10 .
                                                                              */
                isOnGround = false; /*
                                      when the player jumps they are no longer on the ground, which makes the statement false and won't let you jump again.
                                    */                                                       
        
              //playerRb.AddForce(Vector3.up * 500); 
                                                      /*
                                                        We are applying a force (method AddForce()) to our Rigidbody to see if the top line actually worked.
                                                        can think of this as transform.Translate() but instead of moving smth phsically with the positions, we can apply forces to things just like in the real world using physics to make smth move.
                                                        we are going to force our player to go up * number
                                                      */ 
                playerAnim.SetTrigger("Jump_trig"); // we are making our jump into a trigger because we saw it as a trigger in our "Animator --> jumping layer's --> parameters". The string that we use is what they called the "run --> jump" stage.
                dirtPartacle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f); // we play the audio "jumpSound" once (PlayOneShot) at 1.0 sound.
        }
    }

    private void OnCollisionEnter(Collision collision) // how to make the gameOver
    {
      if (collision.gameObject.CompareTag("Ground")) // we put a tag in the ground from our Hierarchy.
      {                                              // if you collide with the ground...
        isOnGround = true; /*
                             whenever the player enters a Collision with smth, meaning that the collision box from the player hits the ground, it makes a collision with the ground.
                           */
        dirtPartacle.Play();
      }
      else if (collision.gameObject.CompareTag("Obstacle")) // we put a tag in the Obstacle prefab.
      {                                                     //if we collide with the obstacle...
        gameOver = true;                                               
        Debug.Log("Game Over!");
        //we want to make the player fall down and die here
        playerAnim.SetBool("Death_b",true); //we're using the conditions from Animator's death layer and clicking on Death_01 to see what conditions we need to write to create the animation on the player.
        playerAnim.SetInteger("DeathType_int",1); //we used death type 1
        explosionPartacle.Play(); // play the explosion effect when player hits the obstacle.
        dirtPartacle.Stop();
        playerAudio.PlayOneShot(crashSound,1.0f);
      }  
    }
}
