using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public bool hasKey = false;
    public bool ishouse;

    public GameObject nextLevel;
    public GameObject textPrefab;

    public bool portalBack;
    public bool portalFromHouse;
    public float lastLevelPosX;
    public float lastLevelPosY;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            if(hasKey && !portalBack){
                nextLevel.GetComponent<LevelManager>().ChangeLevel();
            } else if((hasKey && portalBack) || portalFromHouse){
                nextLevel.GetComponent<LevelManager>().ChangeLevelBack(lastLevelPosX, lastLevelPosY);
            }
            else if(hasKey == false){
                var go = Instantiate(textPrefab, transform.position, transform.rotation, transform);
                go.GetComponent<Text>().text = "Find the key to access new level!";
            } else if(ishouse){
                nextLevel.GetComponent<LevelManager>().ChangeLevel();
            }
        }
        
    }
}
