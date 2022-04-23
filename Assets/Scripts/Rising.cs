using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rising : MonoBehaviour
{
    public Rigidbody playerRb;
    bool isMoving;
    int counter;

    //protected float animationTime;
    public Transform playerTransform;
    Vector3 risingPosition;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        counter = 0;
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
            }
            
            playerTransform.position = risingPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if player is on rising cube
        if (playerRb.IsSleeping())
        {
            //Debug.Log("playerRb.IsSleeping(): " + playerRb.IsSleeping());
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
        counter = 0;
        //isMoving = true;
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1);
    }

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}


