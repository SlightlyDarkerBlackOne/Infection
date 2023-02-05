using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel {get; private set;} = 0;
    public Level[] levels;

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
        //player = PlayerController.Instance.gameObject;
    }
    #endregion

    public void LevelForward(){

        //Fade out level change
        /*Camera.main.GetComponent<CameraMove>()
            .ChangeLevelBorders(levels[currentLevel].minX, levels[currentLevel].minY, 
                    levels[currentLevel].maxX, levels[currentLevel].maxY);*/
        CurrentLevel++;
        PutPlayerOnStartingPosition(PlayerController2D.Instance.gameObject);
    }
    public void LevelBackward(){
        //Fade out level change
        /*Camera.main.GetComponent<CameraMove>().ChangeLevelBorders(levels[currentLevel].minX, levels[currentLevel].minY, 
                    levels[currentLevel].maxX, levels[currentLevel].maxY);*/
        CurrentLevel--;
        PutPlayerOnEndPosition(PlayerController2D.Instance.gameObject);
    }

    public void PutPlayerOnStartingPosition(GameObject player){
        player.transform.position = new Vector3(levels[CurrentLevel].startingPosition.transform.position.x,
            levels[CurrentLevel].startingPosition.transform.position.y, player.transform.position.z);
    }
    public void PutPlayerOnEndPosition(GameObject player){
        player.transform.position = new Vector3(levels[CurrentLevel].endPosition.transform.position.x,
         levels[CurrentLevel].endPosition.transform.position.y, player.transform.position.z);
    }

    public void SetToLevelOne()
    {
        CurrentLevel = 0;
        SceneManager.LoadScene(levels[0].levelName);
        PutPlayerOnStartingPosition(PlayerController2D.Instance.gameObject);
    }
}
