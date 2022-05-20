using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for circuit components/logic gates. Implementations should specify inputs/outputs and the
/// logic for how they relate.
/// 
/// Circuit components can be like logic gates, having inputs and outputs, or they can be one-sided, like a
/// switch that only has an output signal, or a light that only has an input signal.
/// 
/// The inputs and outputs reference the order of their truth table, but currently components do not use Truth Table objects.
/// In the future, I would like to refactor circuit components to work by using a truth table.
/// You would provide a component with a truth table, then the inputs and outputs in the order they appear
/// in the truth table, and every component would simply reference their truth table to determine the output.
/// This approach would allow for easy construction of more circuit components, as well as custom cirucit components.
/// 
/// @author Alex Wills.
/// </summary>
public abstract class CircuitComponent : MonoBehaviour
{
    [Tooltip("List of inputs belonging to this component, in the order of their truth table.")]
    public LineConnectorEndpoint[] inputs;
    [Tooltip("List of outputs belonging to this component, in the order of their truth table.")]
    public LineConnectorEndpoint[] outputs;

    // UpdateId is used to detect loops.
    // This solution is inadequate for circuit components.
    // If a single signal is sent to multiple circuit components, and then those components come together again,
    // the component where they unite will be updated multiple times, which should not be considered a loop.
    private float currentUpdateId = 2f;


    // Start is called before the first frame update
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        UpdateLinePositions();
    }

    /// <summary>
    /// Update the LineRenderers so that the wires stay attached to the component as it moves.
    /// </summary>
    private void UpdateLinePositions()
    {
        // Update the Line Position for all inputs and outputs.
        foreach(LineConnectorEndpoint connection in this.inputs)
        {
            connection.UpdateLinePositions();
        }
        foreach (LineConnectorEndpoint connection in this.outputs)
        {
            connection.UpdateLinePositions();
        }
    }

    /// <summary>
    /// Read the inputs, perform logic, and/or update the outputs.   
    /// Must pass the updateId to any outputs this method updates (for infinite loop detection).
    /// </summary>
    public abstract void UpdateCircuitSignals(float updateId);

    /// <summary>
    /// [UNUSED]
    /// Checks to see if the updateId is already stored in this object.
    /// Use this function in any component with multiple inputs to avoid crashing because of recursion.
    /// In UpdateCircuitSignals, only do anything if this method returns false.
    /// </summary>
    /// <returns> true if this updateId indicates a loop in the circuit. </returns>
    protected bool IsLoop(float updateId)
    {
        // If we have seen this updateId before, return true
        if(currentUpdateId == updateId)
        {
            return true;

        } else
        {
            // If we have not seen the updateId before, remember it to avoid loops 
            this.currentUpdateId = updateId;
            return false;
        }
    }
    
}
