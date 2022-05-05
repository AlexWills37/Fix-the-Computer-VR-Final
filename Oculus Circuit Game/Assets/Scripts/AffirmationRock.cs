using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to a rock to activate an affirmation when colliding with the hand
/// </summary>
public class AffirmationRock : MonoBehaviour
{

    public GameObject affirmation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            affirmation.SetActive(true);
        }
    }
}
