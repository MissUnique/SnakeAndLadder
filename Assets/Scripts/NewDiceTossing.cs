using UnityEngine;
using UnityEngine.UI;

public class NewDiceTossing : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;

    public Button tossButton;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Button btn = tossButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;
    }

    // Rolling the dice 
    void TaskOnClick()
    {
        float dirX = Random.Range(-500, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        transform.position = new Vector3(13, 8, -2);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 100);
        rb.AddForce(transform.forward * 100);
        rb.AddTorque(dirX, dirY, dirZ);
    }
}
