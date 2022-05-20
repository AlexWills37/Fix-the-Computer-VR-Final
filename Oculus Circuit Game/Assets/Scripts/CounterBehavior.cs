using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Stores and displays a count
/// 
/// @author Alex Wills
/// </summary>
public class CounterBehavior : MonoBehaviour
{

    [Header("Text Settings")]
    [Tooltip("Text to display before the counter value.")]
    public string preText = "Counter: ";

    [Tooltip("Text to display after the counter value.")]
    public string postText = "";

    private TextMeshProUGUI textMesh;

    // This is where we store the count
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        count = 0;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Increase the counter's value by 1
    /// </summary>
    public void IncrementCounter()
    {
        count++;
        UpdateText();
    }

    /// <summary>
    /// Set the Text Mesh text to reflect the value in the counter
    /// </summary>
    private void UpdateText()
    {
        textMesh.SetText(preText + count + postText);
    }
}
