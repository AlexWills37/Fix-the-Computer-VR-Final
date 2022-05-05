using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to game objects that can be points for the line connector
/// 
/// @author Alex Wills
/// </summary>
public class LineConnectorEndpoint : MonoBehaviour
{

    private static LineConnector mainConnector = null;

    // This line is used to persist after making a connection. Should only be edited by mainConnector
    private LineRenderer finishedLine;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the static main connector once for all objects
        if(mainConnector == null)
        {
            mainConnector = GameObject.Find("Connect the Dots").GetComponent<LineConnector>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Send this game object to the Main Line Connector to start/end a line
    /// </summary>
    public void AddToLine()
    {
        mainConnector.ClickObject(this.gameObject);
    }

    /// <summary>
    /// Add a line renderer with a line to store, so that the line may persist.
    /// </summary>
    /// <param name="newLine"> The LineRenderer to hold onto. </param>
    public void SetFinishedLine(LineRenderer newLine)
    {
        finishedLine = newLine;
    }
}
