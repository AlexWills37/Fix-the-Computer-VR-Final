using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Control active status of beam launchers for rainbow hands
///
/// @author Alex Wills 
/// </summary>
public class ToggleRainbowHands : MonoBehaviour
{
    
    private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = this.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLauncherStatus()
    {
        setLauncherStatus(toggle.isOn);
    }

    public void setLauncherStatus(bool isActive)
    {
        BeamLaucherBehavior.SetLaunchStatus(isActive);
    }
}
