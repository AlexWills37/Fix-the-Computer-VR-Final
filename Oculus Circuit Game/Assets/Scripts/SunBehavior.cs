using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines how the sun should work
/// </summary>
public class SunBehavior : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<DaySunCycle>().startDayTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
