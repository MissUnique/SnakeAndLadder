using UnityEngine;
using UnityEngine.UI;


public class NewDiceNumberText : MonoBehaviour
{
    Text text;
    public static int diceNumber;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = diceNumber.ToString();
    }
}
