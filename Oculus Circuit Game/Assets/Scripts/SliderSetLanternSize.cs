using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Connects a UI slider with a lantern object to link their values
/// 
/// @author Alex Wills
/// </summary>
public class SliderSetLanternSize : MonoBehaviour
{
    [Tooltip("The behavior script from the Lantern object to link with this slider.")]
    public LanternBehavior lanternBehavior;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();

        // Find a lantern to link
        if(lanternBehavior == null)
        {
            lanternBehavior = GameObject.Find("Lantern").GetComponent<LanternBehavior>();
        }

        // Update the starting values so the lantern matches the slider
        this.UpdateLanternSize();
    }

    /// <summary>
    /// Update the lantern's size based on the slider's value.
    /// </summary>
    public void UpdateLanternSize()
    {
        lanternBehavior.SetScale(slider.value);
    }
}
