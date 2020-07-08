using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    public GameObject playerLevelPosition;
    public GameObject levelCamera;
    public GameObject player;

    public void ChangeLevel(){
        //Fade out level change
        levelCamera.GetComponent<CameraMove>().ChangeLevelBorders(minX, minY, maxX, maxY);
        player.transform.position = new Vector3(playerLevelPosition.transform.position.x,
         playerLevelPosition.transform.position.y, player.transform.position.z);
        
    }

    public void ChangeLevelBack(float x, float y){
        //Fade out level change
        levelCamera.GetComponent<CameraMove>().ChangeLevelBorders(minX, minY, maxX, maxY);
        player.transform.position = new Vector3(x, y, player.transform.position.z);
    }
}
