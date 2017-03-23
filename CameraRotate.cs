/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    private int timer;

    void Update()
    {
        // slowly rotate the camera across the galaxy for an epic intro screen
        transform.Rotate(0 * Time.deltaTime, 3 * Time.deltaTime, 1 * Time.deltaTime);
    }
}