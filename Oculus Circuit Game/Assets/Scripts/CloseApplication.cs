using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows for the application to be closed on command
/// 
/// @author Alex Wills
/// </summary>
public class CloseApplication : MonoBehaviour
{
    /// <summary>
    /// Close the application
    /// </summary>
    public void QuitApp()
    {
        Application.Quit();
    }
}
