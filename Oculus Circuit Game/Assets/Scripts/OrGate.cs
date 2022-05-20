using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OR gate component. 
/// Needs two inputs and one output.
/// Sets the output to on whenever at least one of the inputs is on.
/// 
/// See CircuitComponent.cs
/// 
/// @author Alex Wills
/// </summary>
public class OrGate : CircuitComponent
{


    public override void UpdateCircuitSignals(float updateId)
    {
        this.outputs[0].SetSignal(this.inputs[0].GetSignal() || this.inputs[1].GetSignal());
        this.outputs[0].UpdateSignal(updateId);

    }
}
