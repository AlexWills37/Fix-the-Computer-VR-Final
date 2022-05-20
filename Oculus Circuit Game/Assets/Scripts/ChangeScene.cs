using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Change the scene or close the application.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    /// <summary>
    /// Switch to a different scene.
    /// </summary>
    /// <param name="sceneIndex"> The index of the scene, indicated in Build Settings. </param>
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Exit the application.
    /// </summary>
    public void CloseApplication()
    {
        Application.Quit();
    }
}
