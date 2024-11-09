using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneMenu
{
    private static void SetScene(string _sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(_sceneName);
        }
    }

    [MenuItem("Scenes/Main Menu")]
    private static void MainMenuScene()
    {
        SetScene(@"Assets/Scenes/MainMenu.unity");
    }

    [MenuItem("Scenes/Red Level")]
    private static void RedLevelScene()
    {
        SetScene(@"Assets/Scenes/RedLevel.unity");
    }

    [MenuItem("Scenes/Purple Level")]
    private static void PurpleLevelScene()
    {
        SetScene(@"Assets/Scenes/PurpleLevel.unity");
    }

    [MenuItem("Scenes/Green Level")]
    private static void GreenLevelScene()
    {
        SetScene(@"Assets/Scenes/GreenLevel.unity");
    }
} 