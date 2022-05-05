using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates objects at a specific location
/// 
/// @author Alex Wills
/// </summary>
public class ObjectSpawnerBehavior : MonoBehaviour
{
    [Tooltip("Where objects should be spawned. Leave empty to default to this object's transform.")]
    public Transform spawnLocation;

    [Tooltip("The object to create with the create method.")]
    public GameObject objectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // If the spawnLocation is not set through the editor, use the parent object's location
        if (spawnLocation == null)
        {
            spawnLocation = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Make a new instance of the stored object at the specified location.
    /// </summary>
    public void CreateObject()
    {
        Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
    }
}
