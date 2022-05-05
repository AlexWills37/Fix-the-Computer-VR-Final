using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Removes an object from the game when it enters this trigger.
/// Also calls a UnityEvent so that other scripts can be called when an object is removed.
/// (for example counting the number of removed objects, or respawning an object that is removed)
/// 
/// @author Alex Wills
/// </summary>
public class DestroyOnTrigger : MonoBehaviour
{
    public UnityEvent onDestroy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Remove objects that come in contact with this game object's trigger
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Destroy object and invoke the onDestroy event
        Destroy(other.gameObject);
        onDestroy.Invoke();
    }
}
