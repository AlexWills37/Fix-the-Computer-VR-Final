using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Indicator that changes material to reflect its input value.
/// This component should have a single input and no outputs.
/// 
/// See CircuitComponent.cs
/// 
/// @author Alex Wills
/// </summary>
public class LightOutput : CircuitComponent
{
    private ChangeColor colorSwapper;

    // Start is called before the first frame update
    void Start()
    {
        this.colorSwapper = this.GetComponent<ChangeColor>();
        this.inputs = new LineConnectorEndpoint[] { this.transform.GetComponentInChildren<LineConnectorEndpoint>() };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set the material to reflect this component's current signal (based on the attached input)
    /// </summary>
    /// <param name="updateId"></param>
    public override void UpdateCircuitSignals(float updateId)
    {
        colorSwapper.SetMaterialState(this.inputs[0].GetSignal());
    }

    /// <summary>
    /// Access this component's signal status.
    /// </summary>
    /// <returns> True if this output is active, false otherwise. </returns>
    public bool GetSignal()
    {
        return this.inputs[0].GetSignal();
    }
}
