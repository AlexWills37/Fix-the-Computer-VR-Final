using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Move an object from its current position to a destination.
/// 
/// @author Alex Wills
/// </summary>
public class Transition : MonoBehaviour
{
    [Tooltip("The location this object should move to.")]
    public Vector3 endPosition;
    
    [Tooltip("How long the transition should take (in seconds).")]
    public float moveTime = 1f;
    
    // Event to call when the transition is complete
    public UnityEvent onArrival;

    // Used to move the object over time in Update()
    private bool moving;
    private float timeElapsed = 0;

    private Vector3 startPosition;
    private Vector3 totalChangeInPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.startPosition = this.transform.position;
        this.moving = false;
        // Calculate the distance to move
        this.totalChangeInPosition = endPosition - startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // If moving, slowly move the object to its destination
        if (moving)
        {
            timeElapsed += Time.deltaTime;
            this.transform.position = startPosition + totalChangeInPosition * (timeElapsed / moveTime);

            // When transition is finished, invoke the event and snap the object to its destination
            if(timeElapsed >= moveTime)
            {
                onArrival.Invoke();
                moving = false;
                this.transform.position = endPosition;
            }
        }
    }

    /// <summary>
    /// Begin the move toward this object's destination.
    /// </summary>
    public void MoveToDestination()
    {
        this.moving = true;
    }
}
