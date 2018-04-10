using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float Speed;
    //public Text countText;
    //public Text winText;

    private new Rigidbody rigidbody;
    private int count;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        count = 0;
        //SetCountText();
        //winText.text = "";
    }

    // performed before any physics calculations
    void FixedUpdate()
    {
        float moveHotizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHotizontal, 0.0f, moveVertical);
        rigidbody.AddForce(movement * Speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            //SetCountText();
        }
    }

    //void SetCountText()
    //{
    //    countText.text = "Count: " + count.ToString();
    //    if (count >= 4)
    //    {
    //        winText.text = "You Win!";
    //    }
    //}
}
