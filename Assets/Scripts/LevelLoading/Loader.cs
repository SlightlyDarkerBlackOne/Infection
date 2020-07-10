using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader
{
    public enum Scene{
        PurpleLevel,
        GreenLevel,
        MainMenu,
        Loading
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene){
        onLoaderCallback = () => {
            SceneManager.LoadScene(scene.ToString());
        };

        //Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    //Refreshing the screen at least once to load the loading scene
    public static void LoaderCallback(){
        if(onLoaderCallback != null){
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
