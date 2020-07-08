using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNewScene : MonoBehaviour
{
    public bool hasKey = false;
    public bool ishouse;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            if(hasKey){
                SceneManager.LoadScene("GreenLevel");
               // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}

