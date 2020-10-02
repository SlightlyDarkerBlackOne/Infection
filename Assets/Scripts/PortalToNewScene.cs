using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNewScene : MonoBehaviour
{
    public bool hasKey = false;
    public bool ishouse;
    public bool portalToPreviousScene;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            if(hasKey){
                if(portalToPreviousScene){
                    Debug.Log("bump");
                    SceneManager.LoadScene(LevelManager.Instance.levels[LevelManager.Instance.CurrentLevel - 1].levelName);
                    LevelManager.Instance.LevelBackward();
                } else{
                    Debug.Log("bum");
                    SceneManager.LoadScene(LevelManager.Instance.levels[LevelManager.Instance.CurrentLevel + 2].levelName);
                    LevelManager.Instance.LevelForward();
                }
                
               // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}

