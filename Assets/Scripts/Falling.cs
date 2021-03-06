using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public Rigidbody playerRb;
    bool isMoving;
    int counter;
    bool transfered;
    public Transform playerTransform;
    Vector3 fallingPosition;

    Vector3 targetPosition;
    GameObject downBox;

    Vector3 originalPosition;


    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        counter = 0;
        transfered = false;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            // Getting name of block
            switch (gameObject.name)
            {
                case "Block  (25)":
                    fallingPosition = new Vector3(3, 0.75f, 0); // Fall to Block (3)
                    downBox = GameObject.Find("Block  (3)");
                    break;
                case "Block  (56)":
                    fallingPosition = new Vector3(8, 4.75f, 4); // Fall to Block (48)
                    downBox = GameObject.Find("Block  (48)");
                    break;
                case "Block  (59)":
                    fallingPosition = new Vector3(1, 0.75f, 0); // Fall to Block (1)
                    downBox = GameObject.Find("Block  (1)");
                    break;
                case "Block  (69)":
                    fallingPosition = new Vector3(9, 3.75f, 3); // Fall to Block (32)
                    downBox = GameObject.Find("Block  (32)");
                    break;
                case "Block  (83)":
                    fallingPosition = new Vector3(4, 5.75f, 5); // Fall to Block (57)
                    downBox = GameObject.Find("Block  (57)");
                    break;
                case "Block  (91)":
                    fallingPosition = new Vector3(8, 7.75f, 7); // Fall to Block (73)
                    downBox = GameObject.Find("Block  (73)");
                    break;
                case "Block  (94)":
                    fallingPosition = new Vector3(6, 2.75f, 2); // Fall to Block (26)
                    downBox = GameObject.Find("Block  (26)");
                    break;
                case "Block  (99)":
                    fallingPosition = new Vector3(1, 7.75f, 7); // Fall to Block (80)
                    downBox = GameObject.Find("Block  (80)");
                    break;
            }

            // Fall player position
            if (!transfered)
            {
                // Lowering triggered blocks + player
                targetPosition = transform.position;
                targetPosition.y -= 3;
                StartCoroutine(fallingMove(transform.position, targetPosition, transform));
                targetPosition = downBox.transform.position;
                targetPosition.y -= 3;
                StartCoroutine(fallingMove(downBox.transform.position, targetPosition, downBox.transform));
                targetPosition = playerTransform.position;
                targetPosition.y -= 3;
                StartCoroutine(fallingMove(playerTransform.position, targetPosition, playerTransform));

                /*
                // If player in odd level -> look to right
                if (playerTransform.rotation.eulerAngles.y != 0 && (playerTransform.position.y - 0.75) % 2 == 0)
                    playerTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                // If player in even level -> look to left
                if (playerTransform.rotation.eulerAngles.y != 180 && (playerTransform.position.y - 0.75) % 2 != 0)
                    playerTransform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                */
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if player is on a falling cube
        if (playerRb.IsSleeping())
        {
            counter++;
            if (counter == 1)
                isMoving = false;
            else
                isMoving = true;
            StartCoroutine(waiting());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset values after transmission
        counter = 0;
    }

    IEnumerator waiting()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1);
    }

    public IEnumerator fallingMove(Vector3 currentPosition, Vector3 newPosition, Transform box)
    {
        float totalMovementTime = 2f;
        float currentMovementTime = 0f;
        while (Vector3.Distance(box.position, newPosition) > 0.1f) 
        {
            currentMovementTime += Time.deltaTime;
            box.position = Vector3.Lerp(currentPosition, newPosition, currentMovementTime / totalMovementTime);
            yield return null;
        }

        if (box == playerTransform && !transfered)
        {
            transfered = true;

            // Transmission of player to new position
            fallingPosition.y -= 3;
            playerTransform.position = fallingPosition;

            // Lifting triggered blocks + player
            StartCoroutine(fallingMove(transform.position, originalPosition, transform));
            targetPosition = downBox.transform.position;
            targetPosition.y += 3;
            targetPosition.y = Mathf.Round(targetPosition.y);
            StartCoroutine(fallingMove(downBox.transform.position, targetPosition, downBox.transform));
            targetPosition = playerTransform.position;
            targetPosition.y += 3;
            targetPosition.y = Mathf.Round(targetPosition.y) - 0.25f;
            StartCoroutine(fallingMove(playerTransform.position, targetPosition, playerTransform));

        }
    }
}
