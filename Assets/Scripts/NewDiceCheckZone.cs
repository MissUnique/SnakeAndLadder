using UnityEngine;

public class NewDiceCheckZone : MonoBehaviour
{
    Vector3 diceVelocity;
    int hitsCounter;

    private void Start()
    {
        hitsCounter = 0;
    }

    private void FixedUpdate()
    {
        diceVelocity = NewDiceTossing.diceVelocity;
    }

    private void OnTriggerStay(Collider other)
    {
        // Checking in dice is static
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            // Getting number on dice
            switch (other.gameObject.name)
            {
                case "Side1":
                    NewDiceNumberText.diceNumber = 6;
                    break;
                case "Side2":
                    NewDiceNumberText.diceNumber = 5;
                    break;
                case "Side3":
                    NewDiceNumberText.diceNumber = 4;
                    break;
                case "Side4":
                    NewDiceNumberText.diceNumber = 3;
                    break;
                case "Side5":
                    NewDiceNumberText.diceNumber = 2;
                    break;
                case "Side6":
                    NewDiceNumberText.diceNumber = 1;
                    break;
            }

            hitsCounter++;
            if (hitsCounter == 1)
                NewPlayerMovement.isTossed = true;
        }
    }

    // Reset counter when dice is tossed again
    private void OnTriggerExit(Collider other)
    {
        hitsCounter = 0;
    }
}
