using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [UNUSED (for now)]
/// Makes a copy of a Grabbable object, leaving a kinematic copy behind.
/// Paritucarly used for building custom circuits, where the player must choose which gates to use (used
/// so that when the player picks up a gate, a copy is left behind for them to pick up more gates)
/// 
/// @author Alex Wills
/// </summary>
public class CloneGate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Creates a kinematic copy of the object that is marked as uncounted by OVRGrabbable
    /// </summary>
    public void MakeClone()
    {
        GameObject clone = Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
        clone.GetComponent<Rigidbody>().isKinematic = true;
        clone.GetComponent<OVRGrabbable>().isCounted = false;
    }
}
