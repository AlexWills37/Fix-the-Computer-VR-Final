using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follow the player when they are close, and send an affirmation when fed waffles
/// 
/// @author Alex Wills
/// </summary>
public class CatBehavior : MonoBehaviour
{
    Animator animController;
    GameObject destination;
    bool followingPlayer = false;
    Vector3 toPlayer;

    public float movementSpeed = 0.75f;

    public GameObject affirmation;


    // Start is called before the first frame update
    void Start()
    {
        animController = this.GetComponent<Animator>();
        destination = GameObject.Find("OVRCameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        // If following player, move towards them and face them
        if(followingPlayer)
        {
            this.transform.LookAt(destination.transform, Vector3.up);
            toPlayer = this.transform.forward;
            this.transform.Translate(toPlayer * movementSpeed * Time.deltaTime, Space.World);
        }
    }

    // Detect feeding cat
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "waffle")
        {
            GameObject.Destroy(collision.gameObject);
            affirmation.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When colliding, enable walking animation and start following the player
        if (other.gameObject.name == "PlayerTrigger")
        {
            animController.SetBool("walking", true);
            followingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "PlayerTrigger")
        {
            animController.SetBool("walking", false);
            followingPlayer = false;
        }
    }   
}
