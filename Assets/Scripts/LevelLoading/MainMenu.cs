using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        transform.Find("Play Button").GetComponent<Button_UI>()
        .ClickFunc = () => {
            Loader.Load(Loader.Scene.PurpleLevel);
        };
    }
}
