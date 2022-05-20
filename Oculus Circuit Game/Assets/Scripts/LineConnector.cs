using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows for a line to be drawn between two points in a circuit.
/// 
/// See LineConnectorEndpoint.cs
/// 
/// @author Alex Wills
/// </summary>
public class LineConnector : MonoBehaviour
{
    [Tooltip("The player's cursor location.")]
    public Transform raycastCursor;

    // The currently active line renderer
    private LineRenderer lineRenderer;

    private Transform pointA = null;
    private Transform pointB = null;

    // Boolean used to indicate whether a point has already been selected or not
    private bool connecting = false;

    // Sound to make when a successful conneciton is made
    private AudioSource connectSound;

    // Start is called before the first frame update
    void Start()
    {
        raycastCursor = GameObject.Find("UIHelpers").transform.GetChild(1);
        lineRenderer = this.GetComponent<LineRenderer>();
        connectSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If one point is selected, continue to update the line renderer to follow the cursor
        if (connecting)
        {
            lineRenderer.SetPosition(1, pointB.position);
        }
    }

    /// <summary>
    /// Begin or end a line, with this new point.
    /// </summary>
    /// <param name="newPoint"> The point to begin/end with </param>
    public void ClickObject(GameObject newPoint)
    {
        // If not connecting, we should begin a line
        if (!connecting)
        {
            BeginLine(newPoint);

        // Otherwise, end the line.
        } else
        {
            EndLine(newPoint);
        }
    }

    /// <summary>
    /// Begin a line, showing the player the line going from the starting point to their cursor
    /// </summary>
    /// <param name="startingPoint"> Where the line should start </param>
    private void BeginLine(GameObject startingPoint)
    {
        connecting = true;
        pointA = startingPoint.transform;
        pointB = raycastCursor;
        lineRenderer.SetPosition(0, pointA.position);
    }

    /// <summary>
    /// After clicking a second point, store the line in a separate renderer that will persist
    /// </summary>
    /// <param name="endingPoint"> Where the line should end </param>
    private void EndLine(GameObject endingPoint)
    {
        // Make sure the end point is different from the starting point
        pointB = endingPoint.transform;
        if (pointA != pointB)
        {
            // Get connector scripts
            LineConnectorEndpoint pointAConnection = pointA.GetComponent<LineConnectorEndpoint>();
            LineConnectorEndpoint pointBConnection = pointB.GetComponent<LineConnectorEndpoint>();

            // Make sure both points allow the connection before making the connection
            bool successA = pointAConnection.AssignConnection(pointBConnection);
            bool successB = pointBConnection.AssignConnection(pointAConnection);

            if(successA && successB)
            {
                lineRenderer.SetPosition(1, pointB.position);
                connectSound.Play();

                // Clone the line renderer to give to the input (the receiving point)
                // This way, we can enforce every signal receiver only having 1 signal received
                LineRenderer newLine = Instantiate(lineRenderer);
                if (!pointAConnection.isSignalSender)
                {
                    pointAConnection.SetFinishedLine(newLine);
                } else
                {
                    pointBConnection.SetFinishedLine(newLine);
                }

                // Update the signals in case something should happen
                    pointAConnection.UpdateSignal();
                    pointBConnection.UpdateSignal();

            }
            
        }

        // Now that the line has been taken care of, reset the main line renderer
        DestroyLine();
    }

    /// <summary>
    /// Reset this main line renderer and prepare to draw a new line
    /// </summary>
    private void DestroyLine()
    {
        connecting = false;
        pointA = null;
        pointB = null;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}
