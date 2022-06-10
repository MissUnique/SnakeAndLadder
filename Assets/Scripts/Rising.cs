using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rising : MonoBehaviour
{
    public Rigidbody playerRb;
    bool isMoving;
    int counter;
    bool transfered;

    //protected float animationTime;
    public Transform playerTransform;
    Vector3 risingPosition;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        counter = 0;
        transfered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            //Debug.Log("You Did it Azza" + gameObject.name);
            /*animationTime += Time.deltaTime;
            animationTime = animationTime % 5f;
            playerTransform.position = Parabola(playerTransform.position, new Vector3(7, 1.75f, 1), 2f, animationTime / 5f);
            */

            // Getting name of block
            switch (gameObject.name)
            {
                case "Block  (4)":
                    risingPosition = new Vector3(7, 1.75f, 1); // Rise to Block (14)
                    break;
                case "Block  (9)":
                    risingPosition = new Vector3(10, 3.75f, 3); // Rise to Block (31)
                    break;
                case "Block  (20)":
                    risingPosition = new Vector3(3, 3.75f, 3); // Rise to Block (38)
                    break;
                case "Block  (28)":
                    risingPosition = new Vector3(4, 8.75f, 8); // Rise to Block (84)
                    break;
                case "Block  (36)":
                    risingPosition = new Vector3(4, 4.75f, 4); // Rise to Block (44)
                    break;
                case "Block  (42)":
                    risingPosition = new Vector3(3, 6.75f, 6); // Rise to Block (63)
                    break;
                case "Block  (51)":
                    risingPosition = new Vector3(7, 6.75f, 6); // Rise to Block (67)
                    break;
                case "Block  (62)":
                    risingPosition = new Vector3(1, 8.75f, 8); // Rise to Block (81)
                    break;
                case "Block  (71)":
                    risingPosition = new Vector3(10, 8.75f, 8); // Rise to Block (90)
                    break;
            }

            // Rise player position
            if (!transfered)
            {
                playerTransform.position = risingPosition;

                // If player in odd level -> look to right
                if (playerTransform.rotation.eulerAngles.y != 0 && (playerTransform.position.y - 0.75) % 2 == 0)
                    playerTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                // If player in even level -> look to left
                if (playerTransform.rotation.eulerAngles.y != 180 && (playerTransform.position.y - 0.75) % 2 != 0)
                    playerTransform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if player is on a rising cube
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
        transfered = true;
    }

    IEnumerator waiting()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1);
    }

    /*
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
    */
}


