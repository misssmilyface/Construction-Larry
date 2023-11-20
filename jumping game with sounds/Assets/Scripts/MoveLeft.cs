using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30;
    private PlayerController playerControllerScript; //we are calling out our "PlayerController" script (the first PlayerController) and calling it in here (this script) as "playerControllerScript".
    private float leftBound = -15;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //we are going to find our "PlayerController.cs" from our "Player" from our Hierarchy that we created and attached the script onto.`
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false) //if the game isn't over
        {                                             // we found the "gameOver" bool from our "Player"'s "PlayerController.cs" script.
                                                      // if you don't set the bool of "gameOver" to public, you wouldn't be able to see it in unity or in other classes. That's why we can call it out now, cause we set it to public.
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject); //destroy the obstacle once it's past the player.
        }
    }
}
