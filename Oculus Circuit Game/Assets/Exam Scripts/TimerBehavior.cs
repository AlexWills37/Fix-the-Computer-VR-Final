using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Counts down from a starting value each second, updates a text object, and invokes an event when the timer
/// is over
/// 
/// @author Alex Wills
/// </summary>
public class TimerBehavior : MonoBehaviour
{
    [Tooltip("How many seconds the timer should count down from.")]
    public float timerStart = 60;

    [Header("Text Settings")]
    [Tooltip("Text to display before the timer value.")]
    public string preText = "Time Remaining: ";

    [Tooltip("Text to display after the timer value.")]
    public string postText = "";

    public UnityEvent onTimerEnd;

    private float timerValue;
    private TextMeshProUGUI timerText;

    // Indicator of whether timer is actively counting down or not
    private bool decreasing = true;


    // Start is called before the first frame update
    void Start()
    {
        this.timerText = this.GetComponent<TextMeshProUGUI>();
        // Set the timer
        timerValue = timerStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (decreasing)
        {
            // Decrease the timer
            timerValue -= Time.deltaTime;

            // When timer finishes, invoke the event 
            if (timerValue <= 0)
            {
                decreasing = false;
                UpdateText("0");
                onTimerEnd.Invoke();

            // If timer is not finished, update the text
            } else
            {
                UpdateText("" + (int)timerValue);
            }

        }
    }

    /// <summary>
    /// Set the timer text to match the current time left.
    /// </summary>
    /// <param name="timeToDisplay"> The time value to show in the text </param>
    private void UpdateText(string timeToDisplay)
    {
        timerText.SetText(preText + timeToDisplay + postText);
    }
}
