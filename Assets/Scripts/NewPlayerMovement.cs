using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public static bool isTossed;

    Vector3 currentPlayerPosition;
    Vector3 newPlayerPosition;
    Vector3 tempPlayerPosition;

    float forwardSteps;
    float extraSteps;

    //public float speed = 10f;
    //Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isTossed = false;

        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("isTossed: " + isTossed);
        //Debug.Log("NewDiceTossing.diceVelocity: " + NewDiceTossing.diceVelocity);

        currentPlayerPosition = transform.position;
        newPlayerPosition = currentPlayerPosition;

        if (isTossed)
        {
            //Debug.Log("diceNumber: " + NewDiceNumberText.diceNumber);
            isTossed = false;

            // Check if player is in the 10 floors playground
            if (currentPlayerPosition.y < 10)
            {
                //Debug.Log("Player is in the 10 floors playground");

                // Player is in odd levels
                if ((currentPlayerPosition.y - 0.75) % 2 == 0)
                {
                    //Debug.Log("Player is in odd levels");

                    newPlayerPosition.x += NewDiceNumberText.diceNumber;

                    // Right movement
                    if (newPlayerPosition.x > 0 && newPlayerPosition.x < 11)
                    {
                        //Debug.Log("currentPlayerPosition: " + currentPlayerPosition);
                        //Debug.Log("newPlayerPosition: " + newPlayerPosition);
                        StartCoroutine(moveObject(currentPlayerPosition, newPlayerPosition, 0f));
                    }

                    // Next level movement
                    else if (newPlayerPosition.x > 10)
                    {
                        forwardSteps = 10 - currentPlayerPosition.x;
                        //Debug.Log("forwardSteps: " + forwardSteps);
                        extraSteps = newPlayerPosition.x - 10;
                        //Debug.Log("extraSteps:" + extraSteps);

                        tempPlayerPosition = currentPlayerPosition;
                        tempPlayerPosition.x += forwardSteps;
                        StartCoroutine(moveObject(currentPlayerPosition, tempPlayerPosition, extraSteps));

                    }
                }

                // Player is in even levels
                else if ((currentPlayerPosition.y - 0.75) % 2 != 0)
                {
                    newPlayerPosition.x -= NewDiceNumberText.diceNumber;

                    // Left movement
                    if (newPlayerPosition.x > 0 && newPlayerPosition.x < 11)
                    {
                        StartCoroutine(moveObject(currentPlayerPosition, newPlayerPosition, 0f));
                    }


                }

            }
            
        }
    }
    
    // Function to move player horizontally
    public IEnumerator moveObject(Vector3 currentPosition, Vector3 newPosition, float extraStepsToMove)
    {
        float totalMovementTime = 1f; 
        float currentMovementTime = 0f;
        while (transform.localPosition != newPosition)
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(currentPosition, newPosition, currentMovementTime / totalMovementTime);
            yield return null;
        }

        // Move up if required
        if (extraStepsToMove > 0)
        {
            extraStepsToMove--;
            StartCoroutine(moveObjectUP(newPosition, extraStepsToMove));
        }
    }

    // Function to move player vertically
    public IEnumerator moveObjectUP(Vector3 currentPosition, float extraStepsUP)
    {
        Vector3 newPosition = currentPosition;
        newPosition.y++;
        newPosition.z++;
        Debug.Log("Start moving up..");
        float totalMovementTime = 1f;
        float currentMovementTime = 0f;
        while (transform.localPosition != newPosition)
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Slerp(currentPosition, newPosition, currentMovementTime / totalMovementTime);
            yield return null;
        }

        // Move horizontally if required
        if (extraStepsUP > 0)
        {
            currentPosition = newPosition;
            newPosition.x -= extraStepsUP;
            StartCoroutine(moveObject(currentPosition, newPosition, 0f));
        }
    }
}

