using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Point that the player can draw lines to/from to connect circuit components.
/// This point is also able to send/receive signal to make the circuit work.
/// 
/// See LineConnector.cs
/// 
/// @author Alex Wills
/// </summary>
public class LineConnectorEndpoint : MonoBehaviour
{

    [Tooltip("Signal senders are outputs from logic gates")]
    public bool isSignalSender;

    [Tooltip("The circuit component this point powers (if it is not a signal sender)")]
    public CircuitComponent parentComponent;

    [Header("Line renderer between points")]
    [Tooltip("The material for the line to take when the signal is on.")]
    public Material lineOnMaterial;
    [Tooltip("The material for the line to take when the signal is off.")]
    public Material lineOffMaterial;

    // Scene's instance of the LineConnector script
    private static LineConnector mainConnector = null;

    // This line is used to persist after making a connection. Should only be set by mainConnector.
    private LineRenderer finishedLine = null;

    // List of connected points. If this is a signal sender, it can have multiple points.
    // If this is a signal receiver, it can only have one conncection.
    private List<LineConnectorEndpoint> connections = new List<LineConnectorEndpoint>();

    private bool signalOn = false;

    // Script for indicating the point's status
    // Unused because of change in how points are colored
    private ChangeColor colorChanger;

    // Update ID used to detect loops and prevent infinite loops.
    private float currentUpdateId = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the static main connector once for all objects
        if(mainConnector == null)
        {
            mainConnector = GameObject.Find("Connect the Dots").GetComponent<LineConnector>();
        }


        this.colorChanger = this.GetComponent<ChangeColor>();

        // If this point is input into a circuit component (if the point is a signal receiver), get the circuit component
        if (!this.isSignalSender)
        {
            this.parentComponent = this.transform.parent.GetComponent<CircuitComponent>();
        }

        //this.colorChanger.SetMaterialState(this.signalOn);
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
        // Disconnect the old connection if it exists
        if(finishedLine != null)
        {
            GameObject.Destroy(finishedLine);
        }
        finishedLine = newLine;
    }

    /// <summary>
    /// Attempt to connect this point to another.
    /// A connection will fail if you are trying to connect an input to an input, or an output to an output.
    /// </summary>
    /// <param name="connection"> The point to connect to this one. </param>
    /// <returns> True if the connection was successfully made. False otherwise. </returns>
    public bool AssignConnection(LineConnectorEndpoint connection)
    {
        bool success = false;
        // Do not let inputs connect with inputs or outputs connect with outputs
        if(this.isSignalSender != connection.isSignalSender)
        {
            // Do not add duplicate connections
            if (!this.connections.Contains(connection))
            {
                // If this is a signal receiver, it can only have 1 connection (its input)
                if (!this.isSignalSender && this.connections.Count > 0)
                {
                    // Clear the old connection is there is one
                    this.connections[0].RemoveConnection(this);
                    this.connections.Clear();
                }

                // Add the conneciton
                this.connections.Add(connection); 
                success = true;
            }
        }
        
        return success;
    }

    /// <summary>
    /// Changes the current state of this point.
    /// </summary>
    /// <param name="signalOn"> the new value for this point. </param>
    public void SetSignal(bool signalOn)
    {
        this.signalOn = signalOn;
    }
    
    /// <summary>
    /// Gets rid of a connection from this point.
    /// </summary>
    /// <param name="toRemove"> the point to disconnect from this one. </param>
    public void RemoveConnection(LineConnectorEndpoint toRemove)
    {
        this.connections.Remove(toRemove);
    }

    /// <summary>
    /// Updates the state of this point and all connections that will be affected.
    /// Call this method whenever the state of the circuit before this point changes, so that
    /// the state of the circuit after this point is also consistent.
    /// </summary>
    public void UpdateSignal(float updateId = 2f)
    {
        bool infiniteLoop = false;

        // If this update doesn't have an ID, assign it one. ID used to detect infinite loops.
        if(updateId == 2f)
        {
            updateId = Random.Range(0f, 1f);
            this.currentUpdateId = updateId;

        } else
        {
            // If the updateId was already assigned and matches, break the infinite loop
            if(updateId == this.currentUpdateId)
            {
                infiniteLoop = true;
            }
        }

        // Only continue to update connections if this update cycle is not repeating.
        if (!infiniteLoop)
        {
            // If this point sends signals, update its connections
            if (this.isSignalSender)
            {
                foreach (LineConnectorEndpoint connectionOut in this.connections)
                {
                    connectionOut.UpdateSignal(updateId);
                }

            // Signal receivers are connected to circuit components, so update the parent component after reading the input
            } else
            {
                this.SetSignal(this.connections[0].signalOn);
                parentComponent.UpdateCircuitSignals(updateId);
            }

            // Reflect this point's state in its material
            //this.colorChanger.SetMaterialState(this.signalOn);

            // Update the color of the connecting line to match the state
            if (this.finishedLine != null)
            {
                if (this.signalOn)
                {
                    this.finishedLine.material = this.lineOnMaterial;
                } else
                {
                    this.finishedLine.material = this.lineOffMaterial;
                }
            }
        }
    }

    /// <summary>
    /// Access this point's signal status.
    /// </summary>
    /// <returns> true if this point has an active signal, false otherwise. </returns>
    public bool GetSignal()
    {
        return this.signalOn;
    }

    /// <summary>
    /// Update the Line Renderer(s) that connect this point so that the wire follows the point.
    /// </summary>
    public void UpdateLinePositions()
    {
        // Only signal receivers should have lines, so they should only have one connection
        if (this.finishedLine != null)
        {
            this.finishedLine.SetPosition(0, this.transform.position);
            this.finishedLine.SetPosition(1, this.connections[0].transform.position);
        }

        // Signal senders should tell their connections to update the lines
        if (this.isSignalSender)
        {
            foreach(LineConnectorEndpoint connection in this.connections)
            {
                connection.UpdateLinePositions();
            }
        }
    }

}
