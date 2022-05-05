using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows for the simulation to start at sunrise and go to sunset
/// 
/// @author Alex Wills
/// </summary>
public class DaySunCycle : MonoBehaviour
{
    [Tooltip("Time (in seconds) between sunrise and sunset")]
    public float dayTimeLength = 180f;

    public UnityEvent onSunset;


    private bool sunMoving = true;
    private float elapsedTime = 0f;

    private float sunriseXRotation = 0f;
    private float sunsetXRotation = 186f;

    private Vector3 startingRotation;

    // Start is called before the first frame update
    void Start()
    {
        this.startingRotation = this.transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // If the sun is moving, advance the time and find the new position of the sun
        if(sunMoving)
        {
            elapsedTime += Time.deltaTime;
            this.transform.eulerAngles = new Vector3(calculateSunRotation(), startingRotation.y, startingRotation.z);
            
            // Stop when sunset is reached, and call an event
            if(this.transform.eulerAngles.x >= sunsetXRotation)
            {
                sunMoving = false;
                onSunset.Invoke();
            }
        }
    }

    /// <summary>
    /// Begin the day time and move the sun from sunrise to sunset
    /// </summary>
    public void startDayTime()
    {
        sunMoving = true;
    }

    /// <summary>
    /// Calculates the rotation along the x-axis for the sun (directional light) to be at
    /// </summary>
    private float calculateSunRotation()
    {
        float deltaX = (sunsetXRotation - sunriseXRotation) * elapsedTime / dayTimeLength;
        return sunriseXRotation + deltaX;
    }
}
