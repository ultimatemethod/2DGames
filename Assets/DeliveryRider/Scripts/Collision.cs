using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    bool hasPizza;
    [SerializeField] float destroyTime = 0.5f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color32 hasPizzaColor = new Color32(255, 0, 0, 255);
    [SerializeField] Color32 noPizzaColor = new Color32(255, 255, 255, 255);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("��ũ~");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pizza" && !hasPizza)
        {
            print("���� ȹ��~");
            hasPizza = true;
            spriteRenderer.color = hasPizzaColor;
            Destroy(collision.gameObject, destroyTime);
        }

        if(collision.tag == "Customer" && hasPizza == true)
        {
            print("����� �Ϸ�Ǿ����ϴ�!");
            hasPizza = false;
            spriteRenderer.color = noPizzaColor;
            collision.gameObject.SetActive(false);

            GameManager.Instance.Score++;
            GameManager.Instance.CheckNextStage();
        }

        //FollowCamera.plan
    }

}
