/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDroidBehavior : MonoBehaviour {

    // Variables
    // For the bullet
    public GameObject bulletLeftSpawn;
    public GameObject bulletRightSpawn;
    public GameObject theBullet;           // This is the object that the droid will fire at the target, FPS.
    public int fireRate = 5;               // This is the number of frames to wait before firing another shot.
    private int frameCounter = 0;          // This is the number of frames that have passed since the last shot was fired.
    public int maxShots = 25;              // This is the number of shots to fire before moving to the next waypoint.
    private int shotsFired = 0;            // This is a counter of shots that have been fired while at the current waypoint.
    public float maxFiringRotation = 50.0f;
    
    // For the path following
    // public Transform[] path;            // These empty-game-objects are merely markers on the map that the droid will aimlessly wander between.
    public GameObject[] waypoints;         // These empty-game-objects are merely markers on the map that the droid will aimlessly wander between.
    public float speed = 5.0f;             // This is the speed at which the droid will travel between waypoints.
    public float reachDist = 1.0f;         // This is the droid's ideal distance to keep between itself and the FPS before shooting.
    private int currPoint = 0;             // This is the index of the waypoint that this droid is currently moving toward.
    
    // For cleaner movement
    [Tooltip("This is the speed at which the droid will rotate towards a target. (1.0f)")]
    public float rotateSpeed = 10.0f;              // This is the speed at which the droid will rotate towards a target.

    // For destructive collision
    public float deathTimer = 7.0f;
    private bool isDying = false;
    

	// Use this for initialization
	void Start () {
        // Start by moving toward your first waypoint.
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        // print("There are " + waypoints.Length + " objects tagged as waypoints.");
}

    // Update is called once per frame
    private void Update()
    {
        // If the droid is alive
        if (!isDying)
        {
            // Keep holding this waypoint
            waypoints[currPoint].GetComponent<WaypointBehavior>().isOccupied = true;

            // Case0: Shoot at player if in range of the currWaypoint and there are shots to be fired
            // Note the current distance between this NPC and the currWaypoint.
            float currDistToWaypoint = findDistToCurrWaypoint();

            // If within range of the current waypoint, and there are shots to be fired
            if (currDistToWaypoint <= reachDist && shotsFired <= maxShots)
            {
                // Look at FPS.
                //gameObject.transform.LookAt(GameObject.Find("FPSController").transform);
                //smoothLookAt(GameObject.Find("FPSController").transform);
                // Calculate the rotation needed to look at the targetObj
                Quaternion targetRotation = Quaternion.LookRotation(GameObject.Find("FPSController").transform.position - this.gameObject.transform.position);

                // Smoothly rotate towards the target point.
                this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                // Pause before shooting.
                frameCounter++;

                // If ready to shoot,
                if (frameCounter >= fireRate)
                {
                    // Calculate the remaining  angle of rotation 
                    float remainingRotation = Quaternion.Angle(this.gameObject.transform.rotation, targetRotation);

                    // If there's not much left to rotate
                    if (remainingRotation < maxFiringRotation)
                    {

                        // Reset frame counter and create theBullet.
                        frameCounter = 0;

                        // Shoot from the left or the right
                        GameObject.Instantiate(theBullet, bulletLeftSpawn.transform.position, bulletLeftSpawn.transform.rotation);
                        GameObject.Instantiate(theBullet, bulletRightSpawn.transform.position, bulletRightSpawn.transform.rotation);


                        // Count the number of shots fired
                        shotsFired++;
                    }
                    // Pick the nextWaypoint, if we're done firing shots here
                    if (shotsFired >= maxShots)
                    {
                        // Identify the next, unoccupied point
                        int nextWaypoint = findUnoccupiedWaypoint();

                        // If there are no unoccupied points, do nothing.
                        if (nextWaypoint >= waypoints.Length)
                        {
                            ;
                        }

                        // Else, move to the nextWaypoint
                        else
                        {
                            // Call dibs on the nextWaypoint
                            waypoints[nextWaypoint].GetComponent<WaypointBehavior>().isOccupied = true;

                            // Let go of the currWaypoint
                            waypoints[currPoint].GetComponent<WaypointBehavior>().isOccupied = false;

                            // Update the pointer to the next waypoint
                            currPoint = nextWaypoint;

                            // Reset the number of shotsFired
                            shotsFired = 0;
                        }
                    }
                }
            }

            // Case1: Move to the nextWaypoint if we're not in range of the currWaypoint
            else if (currDistToWaypoint > reachDist)
            {
                // Look at the destination
                smoothLookAt(waypoints[currPoint].transform);

                // Lerp towards the destination
                transform.position = Vector3.Lerp(transform.position, waypoints[currPoint].transform.position, Time.deltaTime * speed);
            }
        }

        // Else the droid is dying
        else
        {
            // TODO: Crash animation

            // Move toward the ground
            Vector3 deathStination = new Vector3(transform.position.x, 0, transform.position.z);
            this.gameObject.transform.position = Vector3.Lerp(transform.position, deathStination, Time.deltaTime * speed);

            // Destroy this object
            Destroy(this.gameObject, deathTimer);
        }
        
    }

    /* Successfully roams between waypoints, shooting at the player, if the FPS is in range.
        // Update is called once per frame
        private void Update()
        {
            // Note the current distance between this NPC and the FPS.
            float currDistToFPS = findCurrDistToFPS();

            // If within range of FPS,
            if (currDistToFPS <= reachDist)
            {
                // Look at FPS.
                //gameObject.transform.LookAt(GameObject.Find("FPSController").transform);
                GameObject target = GameObject.Find("FPSController");
                smoothLookAt(target.transform);

                shotTimer++;

                // If ready to shoot,
                if (shotTimer >= fireRate)
                {
                    // Reset frame counter and create theBullet.
                    shotTimer = 0;

                    // Shoot from the left or the right
                    GameObject.Instantiate(theBullet, bulletLeftSpawn.transform.position, bulletLeftSpawn.transform.rotation);
                    GameObject.Instantiate(theBullet, bulletRightSpawn.transform.position, bulletRightSpawn.transform.rotation);    
                }
            }

            // Else continue on path
            else
            {
                // Find the distance to the currPoint
                float dist = Vector3.Distance(path[currPoint].position, transform.position);

                // Look at the destination
                smoothLookAt(path[currPoint]);

                // Slerp towards the destination
                transform.position = Vector3.Lerp(transform.position, path[currPoint].position, Time.deltaTime * speed);

                // If in range of the currPoint
                if (dist <= reachDist)
                {
                    // Move to the next point
                    currPoint++;
                }

                // Loop the path after the last point
                if (currPoint >= path.Length)
                {
                    currPoint = 0;
                }
            }
        }
    */
    
    // findCurrDisttoFPS returns the floating value of the distance between this NPC and the "FPSController" object
    private float findCurrDistToFPS() {
        return Vector3.Distance(GameObject.Find("FPSController").transform.position, gameObject.transform.position);
    }
    
    private float findDistToCurrWaypoint()
    {
        return Vector3.Distance(this.gameObject.transform.position, waypoints[currPoint].transform.position);
    }

    private int findUnoccupiedWaypoint()
    {
        int i = 0;

        for (i=0; i<waypoints.Length; i++)
        {
            if (!waypoints[i].GetComponent<WaypointBehavior>().isOccupied)
            {
                break;
            }
        }

        // print("Waypoint " + i + " is unoccupied.");
        return i;
    }

    private void smoothLookAt(Transform targetObj)
    {
        // Calculate the rotation needed to look at the targetObj
        Quaternion targetRotation = Quaternion.LookRotation(targetObj.transform.position - this.gameObject.transform.position);

        // Smoothly rotate towards the target point.
        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // things that will cause drones to DYE pretty paintings and PRETTY pictures ECKS DEE *XD
        if (other.gameObject.tag.Equals("Forcible")
            || other.gameObject.name.Equals("force")
            || other.gameObject.name.Equals("npc_deflectedBullet")
            || !other.gameObject.tag.Equals("Wall")
            || !other.gameObject.tag.Equals("Waypoint")
            || !other.gameObject.name.Equals("door")
            || !other.gameObject.name.Contains("npc_blasterLaser")
            || other.gameObject.name.Equals("Cube")
            || !other.gameObject.name.Contains("npc_trainingDrone"))
        {
            Debug.Log("Droid colliding with " + other.name);
            isDying = true;
            this.gameObject.tag.Equals("Forcible");
        }
        else
        {
            //NO EFFECT
        }
    }

}