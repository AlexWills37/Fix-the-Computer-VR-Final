using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Forces a game object to rotate 90 degrees on the x-axis every frame.
/// Designed specifically for applying to TrailRenderer objects so that the 
/// trails always face towards the sky as if they were drawn on the ground.
/// 
/// This is a workaround for ignoring the parent object's rotation.
/// 
/// @author Alex Wills
/// </summary>
public class AlwaysStayVertical : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
