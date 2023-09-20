using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    Rigidbody2D rb2D;
    [SerializeField] float boostSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // GetKey를 이용, KeyCode에서 Left, Right Arrow
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Torque
            rb2D.AddTorque(torqueAmount * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2D.AddTorque(-torqueAmount * Time.deltaTime);
        }

    }
}
