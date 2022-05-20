using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Defines a button that can be pressed by a collision (hand pressing button)
/// 
/// @author Alex Wills
/// </summary>
public class ButtonBehavior : MonoBehaviour
{
    // Event to invoke when the button is pressed
    public UnityEvent buttonPressed;

    // Animator with isAnim bool parameter to activate the button press animation
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get the animator
        this.animator = this.GetComponent<Animator>();
    }


    /// <summary>
    /// When a collision begins, activate the button.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // Make sure the collision is not caused by a cursor raycast
        // NOTE: this use of the cursor does not currently work. The goal was to use the cursor to
        // change the appearance of objects you are aiming at to indicate interactivity, but the current OVR
        // script does not allow for that.
        if (!collision.gameObject.CompareTag("Cursor"))
        {
            ActivateButton();
        }
    }

    /// <summary>
    /// Activate button when a trigger presses the button as well.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Cursor"))
        {
            ActivateButton();
        }
    }

    /// <summary>
    /// Activate a button press. Invokes the UnityEvent and sets the animator parameter to start
    /// an animation.
    /// </summary>
    public void ActivateButton()
    {
        buttonPressed.Invoke();
        animator.SetBool("isAnim", true);
    }
}
