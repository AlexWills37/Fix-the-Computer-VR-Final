using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made for debugging, so you can see values while in VR
/// </summary>
public class DisplayValue : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValue(string newText)
    {
        text.text = newText;
    }
}
