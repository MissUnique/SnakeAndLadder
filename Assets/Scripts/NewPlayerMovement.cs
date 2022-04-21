using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public static bool isTossed;

    Vector3 playerPosition;

    public float speed = 10f;
    Rigidbody rb;
    float newPosition;

    // Start is called before the first frame update
    void Start()
    {
        isTossed = false;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("isTossed: " + isTossed);
        //Debug.Log("NewDiceTossing.diceVelocity: " + NewDiceTossing.diceVelocity);

        playerPosition = transform.position;

        if (isTossed)
        {
            //Debug.Log("diceNumber: " + NewDiceNumberText.diceNumber);
            isTossed = false;

            // Check if player is in the 10 floors playground
            if (playerPosition.y < 10)
            {
                //Debug.Log("Player is in the 10 floors playground");

                // Player is in odd levels
                if ((playerPosition.y - 0.75) % 2 == 0)
                {
                    //Debug.Log("Player is in odd levels");

                    playerPosition.x += NewDiceNumberText.diceNumber;

                    // Right movement
                    if (playerPosition.x > 0 && playerPosition.x < 11)
                    {
                        StartCoroutine(moveObject(playerPosition));
                    }
                }
            }

            newPosition = transform.position.x + NewDiceNumberText.diceNumber;
            //Debug.Log("currentPosition: " + transform.position.x);
            //Debug.Log("newPosition: " + newPosition);

            
            /*      Try 2
            if (transform.position.x < newPosition)
                rb.velocity = new Vector3(NewDiceNumberText.diceNumber, rb.velocity.y, 0) * speed;
            */
            /*      Try 1
            gameObject.transform.position = new Vector3(transform.position.x + (NewDiceNumberText.diceNumber * speed * Time.deltaTime),
               transform.position.y, transform.position.z);
            */
        }
    }

    public IEnumerator moveObject(Vector3 playerNewPos)
    {
        return null;
    }
}
