using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;

public class GameSceneUI : MonoBehaviour
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        transform.Find("mainMenuBtn").GetComponent<Button_UI>()
        .ClickFunc = () => {
            Debug.Log("Click Main Menu");
            Loader.Load(Loader.Scene.MainMenu);
        };
    }
}
