using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AND logic gate, with two inputs and one output. The output turns on
/// when both inputs are also on.
/// 
/// see CircuitComponent
/// 
/// @author Alex Wills
/// </summary>
public class AndGate : CircuitComponent
{
    public override void UpdateCircuitSignals(float updateId)
    {
        this.outputs[0].SetSignal(this.inputs[0].GetSignal() && this.inputs[1].GetSignal());
        this.outputs[0].UpdateSignal(updateId);
    }
}
