/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBehavior : MonoBehaviour {

    public float bulletSpeed = 10.0f;
    public float bulletLifetime = 10.0f;
    private bool hasBeenDeflected = false;
    Vector3 velocity;

	// Use this for initialization
	void Start () {
        hasBeenDeflected = false;
        velocity = gameObject.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += bulletSpeed * Time.smoothDeltaTime * velocity;

        // Destroy after time 
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter(Collision info) // When we run into something
    {
        //Debug.Log("Laser Reflected");
        // bounce off surface
        foreach (ContactPoint c in info.contacts) // Find collision point
        {
            // find the BOUNCE of the object
            velocity = 2 * (Vector3.Dot(velocity, Vector3.Normalize(c.normal))) * Vector3.Normalize(c.normal) - velocity; // following formula  v' = 2 * (v . n) * n - v
            velocity *= -1; // had to multiply everything by -1. Don't know why, but it was all backwards.
            hasBeenDeflected = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        // if the laser is destroyed 
        if (other.gameObject.name.Equals("FPSController") 
            || !other.gameObject.name.Equals("door")
            || other.gameObject.name.Equals("force"))
        {
            //Debug.Log("Laser Destroyed by: " + other.name);
            Destroy(gameObject);
        }

        // else colliding with anything else
        else
        {
            // destroy the bullet
            Destroy(gameObject, 10);
        }
    }
}
