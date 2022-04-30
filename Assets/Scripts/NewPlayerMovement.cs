using System.Collections;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public static bool isTossed;

    Vector3 currentPlayerPosition;
    Vector3 newPlayerPosition;
    Vector3 tempPlayerPosition;

    float forwardSteps;
    float extraSteps;

    public Transform cam;
    Vector3 camOffset;
    Vector3 camOriginalPosition;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isTossed = false;

        camOffset = new Vector3(0, 3, -3);
        camOriginalPosition = cam.position;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerPosition = transform.position;
        newPlayerPosition = currentPlayerPosition;

        if (isTossed)
        {
            isTossed = false;

            cam.position = currentPlayerPosition + camOffset;

            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            // Check if player is in the 10 floors playground
            if (currentPlayerPosition.y < 10)
            {
                currentPlayerPosition.y = Mathf.Round(currentPlayerPosition.y) - 0.25f;

                // Player is in odd levels
                if ((currentPlayerPosition.y - 0.75) % 2 == 0)
                {
                    // Player looks to the right
                    if (transform.rotation.eulerAngles.y != 0)
                        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                    newPlayerPosition.x += NewDiceNumberText.diceNumber;

                    // Right movement
                    if (newPlayerPosition.x > 0 && newPlayerPosition.x < 11)
                    {
                        StartCoroutine(moveObject(currentPlayerPosition, newPlayerPosition, 0f));
                    }

                    // Next level movement
                    else if (newPlayerPosition.x > 10)
                    {
                        forwardSteps = 10 - currentPlayerPosition.x;
                        extraSteps = newPlayerPosition.x - 10;

                        tempPlayerPosition = currentPlayerPosition;
                        tempPlayerPosition.x += forwardSteps;
                        StartCoroutine(moveObject(currentPlayerPosition, tempPlayerPosition, extraSteps));
                    }
                }

                // Player is in even levels
                else if ((currentPlayerPosition.y - 0.75) % 2 != 0)
                {
                    // Player looks to the left
                    if (transform.rotation.eulerAngles.y != 180)
                        transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

                    newPlayerPosition.x -= NewDiceNumberText.diceNumber;

                    // Left movement
                    if (newPlayerPosition.x > 0 && newPlayerPosition.x < 11)
                    {
                        StartCoroutine(moveObject(currentPlayerPosition, newPlayerPosition, 0f));
                    }

                    // Next level movement
                    else if (newPlayerPosition.x < 1)
                    {
                        forwardSteps = currentPlayerPosition.x - 1;
                        extraSteps = 1 - newPlayerPosition.x;

                        tempPlayerPosition = currentPlayerPosition;
                        tempPlayerPosition.x -= forwardSteps;
                        StartCoroutine(moveObject(currentPlayerPosition, tempPlayerPosition, extraSteps));
                    }
                }
            }
        }

        else
        {
            cam.position = camOriginalPosition;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    
    // Function to move player horizontally
    public IEnumerator moveObject(Vector3 currentPosition, Vector3 newPosition, float extraStepsToMove)
    {
        float totalMovementTime = 2f; 
        float currentMovementTime = 0f;
        while (transform.localPosition != newPosition) 
        {
            cam.position = transform.position + camOffset;
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
        // Player looks to the back
        if (transform.rotation.eulerAngles.y != 270)
            transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);

        Vector3 newPosition = currentPosition;

        // Move towards ladder
        newPosition.z += 0.25f;
        float totalMovementTime = 1f;
        float currentMovementTime = 0f;
        while (Vector3.Distance(transform.localPosition, newPosition) > 0.1f) // transform.localPosition != newPosition)
        {
            cam.position = transform.position + camOffset;
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Slerp(currentPosition, newPosition, currentMovementTime / totalMovementTime);
            yield return null;
        }

        // Climb the ladder
        currentPosition = newPosition;
        newPosition.y++;
        totalMovementTime = 1f;
        currentMovementTime = 0f;
        while (Vector3.Distance(transform.localPosition, newPosition) > 0.1f)
        {
            cam.position = transform.position + camOffset;
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Slerp(currentPosition, newPosition, currentMovementTime / totalMovementTime);
            yield return null;
        }

        // Move towards middle of block
        currentPosition = newPosition;
        newPosition.z += 0.75f;
        totalMovementTime = 1f;
        currentMovementTime = 0f;
        while (Vector3.Distance(transform.localPosition, newPosition) > 0.1f)
        {
            cam.position = transform.position + camOffset;
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Slerp(currentPosition, newPosition, currentMovementTime / totalMovementTime);
            yield return null;
        }

        // Move horizontally if required
        if (extraStepsUP > 0)
        {
            currentPosition = newPosition;
            // If player on even level -> move and look left
            if ((currentPosition.y - 0.75) % 2 != 0)
            {
                newPosition.x -= extraStepsUP;
                if (transform.rotation.eulerAngles.y != 180)
                    transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            // If player on odd level -> move and look right
            else if ((currentPosition.y - 0.75) % 2 == 0)
            {
                newPosition.x += extraStepsUP;
                if (transform.rotation.eulerAngles.y != 0)
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            StartCoroutine(moveObject(currentPosition, newPosition, 0f));
        }
    }

}

