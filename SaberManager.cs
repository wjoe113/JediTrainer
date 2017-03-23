/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LaserSword;
using UnityStandardAssets.Characters.FirstPerson;

public class SaberManager : MonoBehaviour
{
    [Tooltip("The player")]
    public GameObject FPS;
    [Tooltip("The player's lightsaber")]
    public GameObject saber;
    [Tooltip("How fast the saber rotates")]
    public float rotationSpeed = 8;
    KinectManager km;
    float timeLeft = 3.0f;
    public Plane reflectHere;
    

    void Start()
    {
        km = gameObject.GetComponent<KinectManager>();
    }

    void Update()
    {
        //Right hand joint that holds saber
        Vector3 rightHand = km.GetJointPosition(km.GetPlayer1ID(), 11);

        //Activate saber after 3 secs
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            saber.GetComponent<LaserSwordScript>().Activate();
        }

        rotateSaber(rightHand);
    }

    //Rotate the saber based on the player's right hand position
    void rotateSaber(Vector3 rightHand)
    {
        saber.transform.rotation = Quaternion.Euler(
            gameObject.transform.rotation.x + (rightHand.x * 100),
            gameObject.transform.rotation.y + (rightHand.y * 100),
            gameObject.transform.rotation.z + (-rightHand.z * 100) + 180);
        //Debug.Log((gameObject.transform.rotation.x + (rightHand.x * 100)) + " " + (gameObject.transform.rotation.y + (rightHand.y * 100)) + " " + (gameObject.transform.rotation.z + (rightHand.z * 100)));
    }
}