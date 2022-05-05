using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enables and disables the trail renderer to make the rake behave like it is drawing lines on the ground.
/// For the rake to draw on a game object, the game object must have the "Rakeable" tag
/// 
/// @author Alex Wills
/// </summary>
public class RakeTrailBehavior : MonoBehaviour
{
    private TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        trail = this.transform.GetComponentInChildren<TrailRenderer>();
    }


    /// <summary>
    /// Turn on the trail emission when the trigger collides with rakeable ground
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rakeable"))
        {
            trail.emitting = true;
        }
    }

    /// <summary>
    /// Disable trail emission when the rake leaves a rakeable surface
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rakeable"))
        {
            trail.emitting = false;
        }
    }
}
