using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows lantern object to grow and float
/// </summary>
public class LanternBehavior : MonoBehaviour
{
    [Tooltip("The speed this object will be released upward with")]
    public float floatSpeed = 3f;
    [Tooltip("How quickly this lantern will grow when growing is set to true")]
    public float growSpeed = 1f;

    [Tooltip("The smallest size for this lantern.")]
    public float minimumSize = 1f;

    [Tooltip("The largest size for this lantern.")]
    public float maximumSize = 3f;

    private bool growing;
    private Rigidbody lanternRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        this.lanternRigidbody = GetComponent<Rigidbody>();
        this.growing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Grow the lantern if growing is set to true
        if (growing)
        {
            this.transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        }
    }

    // To float the lantern, disable gravity and apply some upward force
    public void FloatLantern()
    {
        lanternRigidbody.useGravity = false;
        lanternRigidbody.AddForce(Vector3.up * floatSpeed, ForceMode.Impulse);
    }

    /// <summary>
    /// Change the size of this lantern.
    /// </summary>
    /// <param name="size"> Float from 0 to 1 (inclusive), where 0 is the smalles size, and 1 is the largest. </param>
    public void SetScale(float size)
    {
        // size = 0 will make sizeDiff = 0, so that newSize = minimumSize
        // size = 1 will make newSize = maximumSize
        float sizeDiffFromMinimum = (this.maximumSize - this.minimumSize) * size;
        float newSize = sizeDiffFromMinimum + this.minimumSize;

        this.transform.localScale = new Vector3(newSize, newSize, newSize);
    }
}
