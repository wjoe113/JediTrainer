/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindScript : MonoBehaviour
{
    Vector3 pastPos;
    Quaternion pastRot;

    //Save current position and rotation
    public void savePos()
    {
        pastPos = transform.position;
        pastRot = transform.rotation;
    }

    //rewind to previous position and rotation
    public void rewindMe()
    {
        transform.position = pastPos;
        transform.rotation = pastRot;
    }
}
