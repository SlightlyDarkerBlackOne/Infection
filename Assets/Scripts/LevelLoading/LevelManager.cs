using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel {get; private set;} = 0;
    public Level[] levels;
    private GameObject player;

    #region Singleton
    public static LevelManager Instance{get; private set;}
    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else{
            Destroy(gameObject);
        }
        player = PlayerController.Instance.gameObject;
    }
    #endregion

    public void LevelForward(){

        //Fade out level change
        /*Camera.main.GetComponent<CameraMove>()
            .ChangeLevelBorders(levels[currentLevel].minX, levels[currentLevel].minY, 
                    levels[currentLevel].maxX, levels[currentLevel].maxY);*/
        CurrentLevel++;
        PutPlayerOnStartingPosition();
    }
    public void LevelBackward(){
        //Fade out level change
        /*Camera.main.GetComponent<CameraMove>().ChangeLevelBorders(levels[currentLevel].minX, levels[currentLevel].minY, 
                    levels[currentLevel].maxX, levels[currentLevel].maxY);*/
        CurrentLevel--;
        PutPlayerOnEndPosition();
    }

    //In the if statement adds levelPos to the starting position because of level specific offset
    //and because player prefab can't be parented 
    public void PutPlayerOnStartingPosition(){
        if(CurrentLevel> 0){
            Vector2 levelPos = levels[CurrentLevel].transform.position;
            player.transform.position = new Vector3(levelPos.x + levels[CurrentLevel].startingPosition.transform.position.x,
                levelPos.y + levels[CurrentLevel].startingPosition.transform.position.y, player.transform.position.z); 
        } else  
            player.transform.position = new Vector3(levels[CurrentLevel].startingPosition.transform.position.x,
                levels[CurrentLevel].startingPosition.transform.position.y, player.transform.position.z);
    }
    public void PutPlayerOnEndPosition(){
        player.transform.position = new Vector3(levels[CurrentLevel].endPosition.transform.position.x,
         levels[CurrentLevel].endPosition.transform.position.y, player.transform.position.z);
    }
}
