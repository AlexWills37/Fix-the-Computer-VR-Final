using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Indicator that changes material to reflect its input value.
/// Specifically used to progress the tutorial when the light is updated (when the user connects the button
/// to the light)
/// 
/// See LightOutput.cs and TutorialManager.cs
/// 
/// @author Alex Wills
/// </summary>
public class TutorialLight : CircuitComponent
{
    private ChangeColor colorSwapper;

    private bool tutorialStepActive = false;

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

    public override void UpdateCircuitSignals(float updateId)
    {
        colorSwapper.SetMaterialState(this.inputs[0].GetSignal());

        // If the tutorial is ready to be continued, continue the tutorial
        if (tutorialStepActive)
        {
            TutorialManager.tutorialManager.ContinueTutorial(3);
            tutorialStepActive = false;
        }
    }

    /// <summary>
    /// Access this component's signal
    /// </summary>
    /// <returns> true if the light is on, false otherwise. </returns>
    public bool GetSignal()
    {
        return this.inputs[0].GetSignal();
    }

    /// <summary>
    /// Allow the tutorial to be progessed by updating this light with a connection.
    /// </summary>
    public void ActivateTutorialStep()
    {
        this.tutorialStepActive = true;
    }

    /// <summary>
    /// Remove the line object connecting this light with a button.
    /// </summary>
    public void DestroyLine()
    {
        this.inputs[0].SetFinishedLine(null);
    }
}
