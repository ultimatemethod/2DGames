using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadDelay = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("check");
            Invoke("ReloadScene", loadDelay);
        }
    }

    void ReloadScene()
    {
        print("Á¾·á");

        SceneManager.LoadScene(0);
    }
}
