using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Performs a logical NOT. Outputs the opposite of the input.
/// Inputs/outputs set in the editor.
/// There should be one input and one output.
/// 
/// see CircuitComponent.cs
/// 
/// @author Alex Wills
/// </summary>
public class NotGate : CircuitComponent
{
    /// <summary>
    /// Sets the output to the negation of the input.
    /// </summary>
    public override void UpdateCircuitSignals(float updateId)
    {
        bool input = this.inputs[0].GetSignal();
        this.outputs[0].SetSignal(!input);
        this.outputs[0].UpdateSignal(updateId);
    }
}
