using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows for a line to be drawn between two points
/// 
/// @author Alex Wills
/// </summary>
public class LineConnector : MonoBehaviour
{
    [Tooltip("The player's cursor location.")]
    public Transform raycastCursor;

    private LineRenderer lineRenderer;

    private Transform pointA = null;
    private Transform pointB = null;
    private bool connecting = false;

    // Start is called before the first frame update
    void Start()
    {
        raycastCursor = GameObject.Find("UIHelpers").transform.GetChild(1);
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
            lineRenderer.SetPosition(1, pointB.position);

            //Clone the line renderer to give to PointA
            LineRenderer newLine = Instantiate(lineRenderer);
            pointA.gameObject.GetComponent<LineConnectorEndpoint>().SetFinishedLine(newLine);
        }

        // Now that the line has been taken care of, reset the main line renderer
        DestroyLine();
    }

    // Reset the line renderer and stop connecting
    private void DestroyLine()
    {
        connecting = false;
        pointA = null;
        pointB = null;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}
