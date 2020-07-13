using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel = 0;
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
    }
    #endregion
    void Start()
    {
        player = PlayerController.Instance.gameObject;
    }

    public void LevelForward(){

        //Fade out level change
        /*Camera.main.GetComponent<CameraMove>()
            .ChangeLevelBorders(levels[currentLevel].minX, levels[currentLevel].minY, 
                    levels[currentLevel].maxX, levels[currentLevel].maxY);*/
        player.transform.position = new Vector3(levels[currentLevel].startingPosition.transform.position.x,
         levels[currentLevel].startingPosition.transform.position.y, player.transform.position.z);
        
    }

    public void LevelBack(float x, float y){
        //Fade out level change
        /*Camera.main.GetComponent<CameraMove>().ChangeLevelBorders(levels[currentLevel].minX, levels[currentLevel].minY, 
                    levels[currentLevel].maxX, levels[currentLevel].maxY);*/
        player.transform.position = new Vector3(x, y, player.transform.position.z);
    }

    public void PutPlayerOnStartingPosition(){
        player.transform.position = new Vector3(levels[currentLevel].startingPosition.transform.position.x,
         levels[currentLevel].startingPosition.transform.position.y, player.transform.position.z);
    }
}
