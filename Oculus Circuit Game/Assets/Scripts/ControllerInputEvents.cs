using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Detect OVR Controller input and trigger events when input happens
/// 
/// @author Alex Wills
/// </summary>
public class ControllerInputEvents : MonoBehaviour
{
    // Unity events
    public UnityEvent leftTriggerPressed;
    public UnityEvent rightTriggerPressed;

    // Booleans to handle pressing button, holding button, releasing button
    private bool leftTriggerDown;
    private bool rightTriggerDown;

    // The value (between 0 and 1) where a trigger is considered released / not pressed
    private float notPressedZone = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Left index trigger
        if ( !leftTriggerDown && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > notPressedZone)
        {
            leftTriggerDown = true;
            leftTriggerPressed.Invoke();

            // Disable leftTriggerDown if it's over
        } else if (leftTriggerDown)
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < notPressedZone)
            {
                leftTriggerDown = false;
            }
        }

        // Right index trigger
        if ( !rightTriggerDown && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > notPressedZone)
        {
            rightTriggerDown = true;
            rightTriggerPressed.Invoke();

            // Disable rightTriggerDown if the trigger is no longer pressed
        } else if ( rightTriggerDown )
        {
            if(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < notPressedZone)
            {
                rightTriggerDown = false;
            }
        }
    }
}
