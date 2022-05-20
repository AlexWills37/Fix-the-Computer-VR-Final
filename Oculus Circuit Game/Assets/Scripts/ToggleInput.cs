using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toggle input can be activated externally to send/stop sending a signal to a single connected point.
/// 
/// See CircuitComponent.cs
/// 
/// @author Alex Wills
/// </summary>
public class ToggleInput : CircuitComponent
{
    [Tooltip("Color Changing script to change the color of this component when the input is toggled.")]
    public ChangeColor colorChanger;

    private bool signalOn = false;

    // Start is called before the first frame update
    void Start()
    {
        this.outputs = new LineConnectorEndpoint[] { this.transform.GetComponentInChildren<LineConnectorEndpoint>() };
    }


    public override void UpdateCircuitSignals(float updateId)
    {
        outputs[0].SetSignal(this.signalOn);
        outputs[0].UpdateSignal();
    }

    /// <summary>
    /// Toggle this switch's state and update the outputs.
    /// </summary>
    public void ToggleSignal()
    {
        this.signalOn = !this.signalOn;
        UpdateCircuitSignals(2f);
        colorChanger.SetMaterialState(this.signalOn);
    }

    /// <summary>
    /// Access this component's current signal.
    /// </summary>
    /// <returns> The signal value (true/fasle) for this component. </returns>
    public bool GetSignal()
    {
        return this.signalOn;
    }

    /// <summary>
    /// Set the signal of this input to true/false.
    /// </summary>
    /// <param name="signal"> The value to make this signal output. </param>
    public void SetSignal(bool signal)
    {
        this.signalOn = signal;
        UpdateCircuitSignals(2f);
        colorChanger.SetMaterialState(this.signalOn);
    }
}
