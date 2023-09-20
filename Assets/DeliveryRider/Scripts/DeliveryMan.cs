using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMan : MonoBehaviour
{ 
    [SerializeField] float steerSpeed = 0.1f; // 소수점 6자리 까지 표현
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float originSpeed;
    [SerializeField] float boostAmount = 2f;
    [SerializeField] float slowAmount = 0.5f;

    // double: 소수점 15자리 

    private void Start()
    {
        originSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //           PC1                    PC2
        // Fps:      10fps                  100fps
        // Duration: 0.1s                   0.01s
        // Distance: 1 * 10 * 0.1s = 1      1 * 100 * 0.01 = 1
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        steerAmount = (moveAmount == 0) ? 0 : (moveAmount >= 0  ? -steerAmount : steerAmount);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            moveAmount *= boostAmount;
        }

        transform.Rotate(0, 0, steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Booster")
        {
            moveSpeed *= boostAmount;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed *= slowAmount;

        Invoke("ResetSpeed", 2f);
    }

    private void ResetSpeed()
    {
        moveSpeed = originSpeed;
    }
}
