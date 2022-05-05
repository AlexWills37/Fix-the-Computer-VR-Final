using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launches projectiles (beams) from a specified game object (like the OVR hands)
/// 
/// @author Alwx Wills
/// </summary>
public class BeamLaucherBehavior : MonoBehaviour
{
    [Tooltip("The place where to launch beams from. Uses the forward direction to launch the beam.")]
    public Transform launcher;

    [Tooltip("The beam object to launch")]
    public GameObject beam;

    public float launchSpeed = 4f;

    private static bool launchEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetLaunchStatus(bool status)
    {
        BeamLaucherBehavior.launchEnabled = status;
    }

    /// <summary>
    /// Creates a new beam from the launcher and sends it in the launcher's forward direction
    /// </summary>
    public void LaunchBeam()
    {
        if(launchEnabled)
        {
            GameObject newBeam = Instantiate(beam, launcher.position, launcher.rotation);
            Rigidbody rb = newBeam.GetComponent<Rigidbody>();

            rb.AddForce(launcher.transform.forward * launchSpeed, ForceMode.Impulse);

            Destroy(newBeam, 5);


        }
    }
}
