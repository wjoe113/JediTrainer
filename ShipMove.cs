/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour {

	//Move the star destroyers one x-wing a second
	void Update ()
    {
        transform.Translate(0, 0, -1*Time.deltaTime);
	}
}