using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float loadDelay = 3f;
    [SerializeField] ParticleSystem dustParticle;
    [SerializeField] ParticleSystem boostParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Invoke("ReloadScene", loadDelay);
        }

    }

    void ReloadScene()
    {
        print("안녕히계세요");

        SceneManager.LoadScene(0);
    }    
}
