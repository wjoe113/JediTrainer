/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour {

    [Tooltip("Where the force spawns")]
    public GameObject forceSpawn;
    [Tooltip("The force (not midichlorians)")]
    public GameObject force;
    [Tooltip("The force of the force (~1000)")]
    public float forceForce = 1000;
	[Tooltip("Time the force travels (0.75)")]
    public float forceTime = 1.0f;
    [Tooltip("Material changed by force")]
    public Material forceMaterial;
    [Tooltip("Door")]
    public GameObject door;
    KinectManager km;
    GameObject obj;
    Renderer objRenderer;
    Material oldMtl;
    bool grabbing;
    bool grabbable;
    public bool doorOpened;
    GameObject[] taggedObjects;
    [Tooltip("Time to rewind")]
    public float rewindTime = 4.0f;
    private float saveRewindTime;
    bool rewinding;
    bool usedPush;
    public float timeLeft = 1.0f;
    private float saveTime;
    Vector3 nullVector = new Vector3(0, 0, 0);

    /* Player stats */
    [Tooltip("HP Texture")]
    public Texture2D HpBarTexture;
    [Tooltip("Mana Texture")]
    public Texture2D ManaBarTexture;
    [Tooltip("Stamina Texture")]
    public Texture2D StaminaBarTexture;
    [Tooltip("The mana animation")]
    public ParticleSystem ManaAnimation;
    public float curHp = 100.0f;
    static float maxHp = 100.0f;
    static float curMana = 100.0f;
    static float maxMana = 100.0f;
    public float curStamina = 100.0f;
    static float maxStamina = 100.0f;
	static float staminaBuilt;
    float hpBarLength;
    float percentOfHP;
    float manaBarLength;
    float percentOfMana;
    float stamBarLength;
    float percentOfStamina;
    Vector3 rayCastOrigin;
    Vector3 rayCastDir;
    Vector3 totalWristToShoulderLen;
    string forceUsed;
    [Tooltip("Door light 1")]
    public GameObject light1;
    [Tooltip("Door light 2")]
    public GameObject light2;
    [Tooltip("Material used in lights")]
    public Material lightOnMaterial;
    Renderer light1Renderer;
    Renderer light2Renderer;
    Material orgLightMat;
    float lightTimer = 10.0f;
    [Tooltip("The training drone")]
    public GameObject trainingDrone;
    [Tooltip("The training drone spawn")]
    public GameObject trainingDroneSpawn;
    [Tooltip("Drones that spawn")]
    public int droneCount = 3;
    private int droneCountSave;

    void Start()
    {
        km = gameObject.GetComponent<KinectManager>();
		staminaBuilt = 0.0f;
        saveTime = timeLeft;
        saveRewindTime = rewindTime;
        forceUsed = "Hello young Padawan!";
        light1Renderer = light1.GetComponent<Renderer>();//Blinking lights
        light2Renderer = light2.GetComponent<Renderer>();
        orgLightMat = light1Renderer.material;
        droneCountSave = droneCount;
    }

    void OnGUI()
    {
        if (curHp > 0) // Health bar
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 100, Screen.height - 20, hpBarLength * 2, 20), HpBarTexture);
        }
        if (curMana > 0) // Mana bar
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 100, Screen.height - 40, manaBarLength * 2, 20), ManaBarTexture);
        }
        if (curStamina > 0) // Stamina bar
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 100, Screen.height - 60, stamBarLength * 2, 20), StaminaBarTexture);
        }
        GUI.TextField(new Rect((Screen.width - 215), Screen.height - 30, 200, 25), forceUsed); // Force used text
    }

    void Update()
    {
        /* Kinect joints */
        Vector3 head = km.GetJointPosition(km.GetPlayer1ID(), 3);
        Vector3 leftShoulder = km.GetJointPosition(km.GetPlayer1ID(), 4);
        Vector3 leftElbow = km.GetJointPosition(km.GetPlayer1ID(), 5);
        Vector3 leftWrist = km.GetJointPosition(km.GetPlayer1ID(), 6);
        Vector3 leftHand = km.GetJointPosition(km.GetPlayer1ID(), 7);
        Vector3 leftKnee = km.GetJointPosition(km.GetPlayer1ID(), 13);
        Vector3 rightShoulder = km.GetJointPosition(km.GetPlayer1ID(), 8);
        Vector3 rightElbow = km.GetJointPosition(km.GetPlayer1ID(), 9);
        Vector3 rightHand = km.GetJointPosition(km.GetPlayer1ID(), 11);
        Vector3 rightKnee = km.GetJointPosition(km.GetPlayer1ID(), 17);
        //Vector3 spineBase = km.GetJointPosition(km.GetPlayer1ID(), 0);
        //Vector3 spineMid = km.GetJointPosition(km.GetPlayer1ID(), 1);

        /* Walk right and reft 
        Vector3 walkLeft = new Vector3(gameObject.transform.position.x + 5.0f, gameObject.transform.position.y, gameObject.transform.position.z);
        Vector3 walkRight = new Vector3(gameObject.transform.position.x - 5.0f, gameObject.transform.position.y, gameObject.transform.position.z);
        Debug.Log("Head:"+ head + "  ||  spine:" + spineMid);
        if (head.x >= 0.1f)
        {
            gameObject.transform.position = walkRight;
        }
        if (head.x <= -0.1f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, walkLeft, 0.5f * Time.deltaTime);
        }*/

        /* Force push - Hands are close together */
        if (Mathf.Abs(Mathf.Abs(rightHand.x) - Mathf.Abs(leftHand.x)) <= 0.07f        // if both hands are aligned horizontally
            && Mathf.Abs(Mathf.Abs(rightHand.y) - Mathf.Abs(leftHand.y)) <= 0.07f     // if both hands are aligned vertically
            && Mathf.Abs(Mathf.Abs(rightHand.z) - Mathf.Abs(leftHand.z)) <= 0.07f     // if both hands are aligned in the z-direction >_>
            && leftElbow.y < leftShoulder.y && rightElbow.y < rightShoulder.y         // if elbows are below shoulders
            && leftHand != nullVector && rightHand != nullVector                      // if head exists in scene
            && curMana >= 10.0f                                                       // if player has enough mana
            && !usedPush)                                                             // if not already force pushing
        {
            forceUsed = "Force Push";
            usedPush = true;
            curMana -= 10.0f;   // costs 10 mana
            GameObject tempForce = Instantiate(force, forceSpawn.transform.position, forceSpawn.transform.rotation) as GameObject;  // create the force
            tempForce.transform.Rotate(Vector3.left * 90);                                              // rotate to the correct direction (?)
            Rigidbody tempRigidBody = tempForce.GetComponent<Rigidbody>();                              // get the rigidbody of the force
            tempRigidBody.AddForce(transform.forward * forceForce);                                     // add force to force
            Destroy(tempForce, forceTime);                                                              // destroy force after given time
        }
        if (usedPush)                                           // if the forcePush has been used, decrement it's timer. once complete, reset the timer.
        {
            timeLeft -= Time.deltaTime;     
            if (timeLeft < 0)
            {
                usedPush = false;
                timeLeft = saveTime;
            }
        }

        /* Rewind time */
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Forcible");     // find forcible objects
        if (Mathf.Abs(Mathf.Abs(leftHand.y) - Mathf.Abs(head.y)) <= 0.1f                // if left hand at center of head
            && Mathf.Abs(Mathf.Abs(rightHand.y) - Mathf.Abs(head.y)) <= 0.1f            // if right hand at center of head
            && Mathf.Abs(Mathf.Abs(leftElbow.x) - Mathf.Abs(rightElbow.x)) >
                Mathf.Abs(Mathf.Abs(leftShoulder.x) - Mathf.Abs(rightShoulder.x))       // if distance between elbows > distance between shoulders
            && leftElbow.y > leftShoulder.y && rightElbow.y > rightShoulder.y           // if elbows are above shoulders
            && curMana >= 51.0f                                                         // if player has enough mana
            && !rewinding                                                               // if not already rewinding
            && head != nullVector)                                                      // if head exists in scene
        {
            foreach (GameObject curObj in taggedObjects)
            {
                if (curObj.GetComponent<RewindScript>() != null)
                {
                    curObj.GetComponent<RewindScript>().savePos(); // save object's current position
                }
            }
            rewinding = true; // start rewinding
            curMana -= 50.0f; // costs 50 mana
        }
        if (rewinding)
        {
            rewindTime -= Time.deltaTime; // running out of time
        }
        if (rewindTime < 1)
        {
            Time.timeScale = 0.5f;                          // slow down time once 1 sec is left
            Time.fixedDeltaTime = 0.02f * Time.timeScale;   // smooth time slowdown
        }
        if (rewindTime < 0)                                 // if done rewinding time
        {
            foreach (GameObject curObj in taggedObjects)
            {
                if (curObj.GetComponent<RewindScript>() != null)
                {
                    curObj.GetComponent<RewindScript>().rewindMe(); // recover to saved pos
                }
            }
            forceUsed = "Time Warp";
            rewinding = false; 
            rewindTime = saveRewindTime; // reset time
            Time.timeScale = 1.0f;
        }

        /* Force pull */
        Vector3 currWristToShoulderLen = km.GetJointPosition(km.GetPlayer1ID(), 7) - km.GetJointPosition(km.GetPlayer1ID(), 4);         // Note the distance between left wrist and left shoulder
        if (Mathf.Abs(Mathf.Abs(rightShoulder.x) - Mathf.Abs(rightElbow.x)) <= 0.1f       // if right shoulder and elbow are straight up, not tilted left/right
            && Mathf.Abs(Mathf.Abs(rightShoulder.z) - Mathf.Abs(rightElbow.z)) <= 0.1f    // if right shoulder and elbow are straight up, not tilted fwd,back
            && rightElbow.y > rightShoulder.y                                   // if right elbow is above left shoulder 
            && grabbable                                                        // if the currently selected object is grabbable
            && curMana >= 10.0f                                                 // if the player has enough mana
            && leftHand != nullVector)                                              // if head exists in scene
        {
            if (obj == door)
            {
                forceUsed = "Door Opened";
                door.GetComponent<Animation>().Play("open");
                doorOpened = true;
            }
            else
            {
                forceUsed = "Force Pull";
                grabbing = true; // you are grabbing an object
                obj.transform.position = forceSpawn.transform.position; // move the object with the camera
            }
        }
        else
        {
            grabbing = false;
        }
		
		/* Force throw - drain stamina */
        if (grabbing) // if you're grabbing an object
        {
            if (curStamina < 2.0f)
            {
                forceUsed = "Force Throw";
                curMana -= 10.0f; // costs 10 mana
                grabbable = false; // you're out of energy and the object is no longer grabbable
                Vector3 throwDisWay = new Vector3(0.0f, 0.25f, 0.25f);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                float speedOfThrow = throwDisWay.magnitude / Time.deltaTime;
                Vector3 throwVelocity = speedOfThrow * throwDisWay.normalized;
                throwVelocity += Camera.main.transform.forward * staminaBuilt; // throw the object from camera with stamina built
                rb.velocity = throwVelocity;
                rb.useGravity = true;
                staminaBuilt = 0.0f;
            }
            else
            {
                staminaBuilt += 0.65f;
                curStamina -= 2.0f;
            }
        }
        else
        {
            staminaBuilt = 0.0f;
        }

        /*HP*/
        percentOfHP = curHp / maxHp;
        hpBarLength = percentOfHP * 100;

        /*Mana*/
        percentOfMana = curMana / maxMana;
        manaBarLength = percentOfMana * 100;

        /*Stamina*/
        percentOfStamina = curStamina / maxStamina;
        stamBarLength = percentOfStamina * 100;

        /*Drain HP*/
        if (Input.GetKeyDown("h"))
        {
            if (curHp >= 10.0f)
            {
                curHp -= 10.0f;
            }
        }

        /*Drain Mana*/
        if (Input.GetKeyDown("m"))
        {
            if (curMana >= 10.0f)
            {
                curMana -= 10.0f;
            }
        }

        /* Restore Health and Mana - "Heroes never die!" */
        if (Mathf.Abs(Mathf.Abs(leftShoulder.x) - Mathf.Abs(leftElbow.x)) <= 0.09f    // if left shoulder and elbow are straight up
            && Mathf.Abs(Mathf.Abs(leftShoulder.z) - Mathf.Abs(leftElbow.z)) <= 0.09f // if left shoulder and elbow are straight up
            && leftElbow.y > leftShoulder.y                                // if left elbow is above left shoulder //leftHand.y >= leftShoulder.y + (leftWrist.y - leftElbow.y)) && (leftHand.y != 0.0f))
            && head != nullVector)                                         // if hand is in frame
        {
            ParticleSystem.EmissionModule em = ManaAnimation.emission;
            em.enabled = true;
            if (percentOfHP <= 0.995f)
            {
                forceUsed = "Force Heal";
                ManaAnimation.Play();
                curHp = curHp + 0.50f;
            }
            if (percentOfMana <= 0.995f)
            {
                forceUsed = "Force Mana";
                ManaAnimation.Play();
                curMana = curMana + 0.50f;
            }
        }
        else
        {
            ManaAnimation.Stop();
        }

        /* Slowly regain hp, mana, and stamina */
        if (percentOfHP <= 0.999f)
        {
            // curHp += 0.1f; // health regen was disabled, because reasons
        }

        if (percentOfMana <= 0.9995f)
        {
            curMana += 0.05f;
        }

        if (percentOfStamina <= 0.995f)
        {
            curStamina += 0.5f;
        }

        /* If you DIE! DIE! DIE! */
        if(curHp == 0.0f)
        {
            Time.timeScale = 0.0f;
            //gameObject.GetComponent<PauseMenuScript>().pauseGame(true);
            gameObject.GetComponent<PauseMenuScript>().simFailed(true);
        }

        /* Lights flash */
        if (doorOpened)
        {
            if (Mathf.Round(lightTimer) % 2 == 0)
            {
                light1Renderer.material = lightOnMaterial;
                light2Renderer.material = orgLightMat;
            }
            else
            {
                light1Renderer.material = orgLightMat;
                light2Renderer.material = lightOnMaterial;
            }
            if (lightTimer > 0)
            {
                lightTimer -= Time.deltaTime;
            }
            if(lightTimer < 9.0f && droneCount == droneCountSave) 
            {
                GameObject.Instantiate(trainingDrone, trainingDroneSpawn.transform.position, trainingDroneSpawn.transform.rotation); // TODO: Instantiate more waypoints for each additional drone
                droneCount--;
            }
            if (lightTimer < 7.0f && droneCount == droneCountSave - 1)
            {
                GameObject.Instantiate(trainingDrone, trainingDroneSpawn.transform.position, trainingDroneSpawn.transform.rotation);
                droneCount--;
            }
            if (lightTimer < 5.0f && droneCount == droneCountSave - 2)
            {
                GameObject.Instantiate(trainingDrone, trainingDroneSpawn.transform.position, trainingDroneSpawn.transform.rotation);
                droneCount--;
            }
            if (lightTimer <= 0)
            {
                doorOpened = false;
                light1Renderer.material = orgLightMat;
                light2Renderer.material = orgLightMat;
            }
        }
    }

    /* Raycasting - object selection */   // So far most applicants only have origin and magnitude, few have direction
    void FixedUpdate()
    {
        RaycastHit hit;
        if (!grabbing)
        {
            Vector3 raycastDirection = new Vector3(km.GetJointPosition(km.GetPlayer1ID(),7).x, 0.0f, 1.0f);
            if (Physics.Raycast(transform.position, raycastDirection, out hit))
            {
                totalWristToShoulderLen = km.GetJointPosition(km.GetPlayer1ID(), 7) - km.GetJointPosition(km.GetPlayer1ID(), 4); // left shoulder to wrist length
                if (obj != hit.collider.gameObject) // if you're not pointing at the previous target
                {
                    if (obj != null) // if no longer in view, reset its material
                    {
                        objRenderer.material = oldMtl;
                    }
                    if (hit.collider.gameObject.tag == "Forcible")
                    {
                        obj = hit.collider.gameObject;              // store reference of target to a variable
                        objRenderer = obj.GetComponent<Renderer>(); // get targets Renderer
                        oldMtl = objRenderer.material;              // store targets current material
                        objRenderer.material = forceMaterial;       // set target to new material
                        grabbable = true;                           // you can grab this object
                    }
                }
            }
            else if (obj != null)                                   // ffs we're not pointing at anything
            {
                objRenderer.material = oldMtl;                      // reset targets material
                obj = null;                                         // clear reference
            }
        }
    }

    /* Got hit - lose health when hit with laser */
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("npc_blasterLaser"))
        {
            curHp -= 2.0f;
        }
    }
}
