using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryAndLose : MonoBehaviour
{
    public void playgame(string scene)
    {
        //The scene you are loading should be included in the build settings
        //Example, note without the .unity on the end of the scene name
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}