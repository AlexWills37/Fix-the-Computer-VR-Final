using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Toggle input can be activated externally to send/stop sending a signal to a single connected point.
/// This version of the ToggleInput specifically works with TutorialManager to progress the tutorial level.
/// 
/// See TogleInput.cs and TutorialManager.cs
/// 
/// @author Alex Wills
/// </summary>
public class TutorialButton : CircuitComponent
{
    private bool signalOn = false;

    // Boolean used to indicate when the button should be able to progress the tutorial.
    private bool tutorialStepActive = false;

    // Start is called before the first frame update
    void Start()
    {
        this.outputs = new LineConnectorEndpoint[] { this.transform.GetComponentInChildren<LineConnectorEndpoint>() };

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void UpdateCircuitSignals(float updateId)
    {
        // If the tutorial is ready to progress and this button is pressed, continue the tutorial.
        if (tutorialStepActive)
        {
            TutorialManager.tutorialManager.ContinueTutorial(2);
            tutorialStepActive = false;
        }
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
    }

    /// <summary>
    /// Access this button's signal status.
    /// </summary>
    /// <returns> true if this button is on, false if it is off.</returns>
    public bool GetSignal()
    {
        return this.signalOn;
    }

    /// <summary>
    /// Change this button's signal status.
    /// </summary>
    /// <param name="signal"> the new status of the signal. </param>
    public void SetSignal(bool signal)
    {
        this.signalOn = signal;
        UpdateCircuitSignals(2f);
    }

    /// <summary>
    /// Gets ready to read the button press to continue the tutorial
    /// </summary>
    public void ActivateTutorialStep()
    {
        this.tutorialStepActive = true;
    }
}
