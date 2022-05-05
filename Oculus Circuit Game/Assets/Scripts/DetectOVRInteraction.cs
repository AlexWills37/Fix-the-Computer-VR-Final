using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invokes UnityEvents when the game object this is attached to is grabbed and released by an OVR hand.
/// 
/// !!! For this script to work, 
/// 1) the Hands, with OVR Grabber scripts and sphere colliders, must have 
///    the tag "Hand" in the Unity Editor
/// 2) this game object's rigidbody must NOT be set to kinematic (make sure isKinematic = false)
/// 
/// It detects OVR grabs by relying on the fact that the hand's trigger collider must
/// collide, and the fact that the OVR Grabber sets the grabbed-object to be kinematic.
/// 
/// Edge cases where this script will not work:
///     - If this object is grabbed before colliding with the hand, the events will only occur
///       when/if this object collides with the hand while still grabbed
///     - If this object is released after leaving the hand's collider, the release event
///       will not occur (unless the hand collides again)
/// In most cases I believe these edge cases will not be a problem.
/// 
/// @author Alex Wills
/// </summary>
public class DetectOVRInteraction : MonoBehaviour
{
    // Occurs when this object is grabbed by an OVRGrabber
    public UnityEvent grabBegin;

    // Occurs when this object is released by an OVRGrabber
    public UnityEvent grabRelease;

    private Rigidbody objectRigidbody;

    // Keep track of status so we can trigger events a single time
    private bool grabbed;

    // Start is called before the first frame update
    void Start()
    {
        grabbed = false;
        objectRigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // On update, check for release (to refactor code, remove other release checks)
        if(grabbed && !objectRigidbody.isKinematic)
        {
            grabbed = false;
            this.grabRelease.Invoke();
        }
    }

    /// <summary>
    /// Detect a immediate grab.
    /// Note that this method only happens when the trigger is first entered, so it is unlikely
    /// this will ever happen
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // When the hand collides and this object is set to kinematic, call the event
        if(!this.grabbed && other.gameObject.CompareTag("Hand") && objectRigidbody.isKinematic)
        {
            this.grabBegin.Invoke();
            this.grabbed = true;
        }
    }

    /// <summary>
    /// Detect grab and release while the trigger is still colliding
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        // Ignore triggers that are not tagged with 'Hand'
        if(other.gameObject.CompareTag("Hand"))
        {
            // Detect grab
            if(!this.grabbed && this.objectRigidbody.isKinematic)
            {
                this.grabbed = true;
                this.grabBegin.Invoke();

            // Detect release
            } else if(this.grabbed && !this.objectRigidbody.isKinematic)
            {
                this.grabbed = false;
                this.grabRelease.Invoke();
            }
        }
    }

    /// <summary>
    /// Detect release when the hand is no longer colliding.
    /// NOTE: I do not know if this method will ever really happen.
    ///     For it to happen, this object must be released right when leaving the hand's collider.
    ///     I am including this method to be safe and cover as many cases as possible.
    ///     
    ///     Also note: If this object is released _after_ leaving the hand's collider, the release will
    ///     not be detected.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if(this.grabbed && other.gameObject.CompareTag("Hand") && !this.objectRigidbody.isKinematic)
        {
            this.grabbed = false;
            this.grabRelease.Invoke();
        }
    }
}
