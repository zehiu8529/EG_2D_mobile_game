using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sce : MonoBehaviour
{
    // Start is called before the first frame update
    public void IveBeenClicked(string scene)
    {
        //The scene you are loading should be included in the build settings
        //Example, note without the .unity on the end of the scene name
        SceneManager.LoadScene(scene);
    }
}
