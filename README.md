# JediTrainer
Jedi Training Simulator 

By Brandon McMillan and Joe Wileman 

CAP6121 Spring 2017 Homework 1

## Summary
In this project, we created a Jedi training simulator to help young Padawan's (Padajuans) practice the ways of the force. The force involves force push, force pull, force throw, healing, and rewinding time. Using a Microsoft 360 Kinect 1.0 and the related MS_Kinect SDK v1.0 from the Unity Asset store, the player can perform their abilities using heuristics and gestures read by the Kinect. A lightsaber is a Jedi's weapon of choice. It is lightweight, versatile, and comes in many pretty colors. On the ship, the player will train against three re-modified Imperial drones; don't worry, they're mostly harmless.

## List of Scripts
BulletBehavior.cs - Used by each blaster laser instantiated by a Training Droid. Instructs lasers to move forward at a variable speed. Upon collision with a reflective surface, lasers are reflected back at enemies. Upon collision with a destructive surface, lasers are destroyed. If the laser makes no contact with any other object, it is destroyed after a variable amount of time.

CameraRotate.cs - Rotates the camera slowly across the stars to create a smooth main menu background.

MenuScript.cs - Regulates the main menu scene.

Outline_Diffuse.shader - Shader used in the material for highlighting objects when raycasted

PauseMenuScript.cs - Regulates the pause and failed menus.

RewindScript.cs - Used in all “forcible” objects. Stores the position when rewind is initiated, then returns the object to that position after four seconds.

SaberManager.cs - Rotates the saber to the player’s right hand position in 3D space.

ForceManager.cs - Regulates the player’s force abilities, stats (health, mana, stamina), and movement (future addition).

ShipMove.cs - Slowly moves ships forward through space.

TrainingDroidBehavior.cs - Used by each of the flying droids in the training room. Instructs droids to coherently roam between the provided waypoints without colliding onto the same waypoint. Upon arrival at a waypoint, droids are instructed to face the user and then shoot a variable number of bullets at a variable speed after a variable amount of time has passed.

WaypointBehavior.cs -  Describes each waypoints current occupation status in order to prevent multiple droids from grouping on a single waypoint.

## How to Play

### Main Menu

Navigation from the main menu must be done with the mouse. From the main menu, users can:
1.	Modify game options by selecting “Options,” 
2.	Begin the demo by selecting “Play Demo”, or
3.	Exit the game by selecting “Exit.” When “Exit” is selected another menu will appear to confirm the player’s selection.

#### Options
From the options menu, volume and resolution may be adjusted. Volume is adjusted by updating the provided slider. Resolution is adjusted by selecting one of the provided resolution settings (1280x720, 1600x900, and 1920x1080).

#### Gameplay
Upon starting the game, players will find themselves isolated in a room filled with boxes and an ominous door between two inactive lights. Gameplay is controlled with a Kinect 1.0. The player’s position remains static throughout the game; that is to say that there is no need for the player to navigate throughout the room. As a Jedi in a strange place, it is necessary to learn what abilities you are capable of performing. The following table enumerates and describes the Force Abilities available to the player, as well as how to successfully produce the gesture necessary to activate each ability.

| Ability  | Description | Gesture | Notes |
| ------------- | ------------- | ------------- | ------------- |
| Lightsaber Control  | Use the force to move and rotate your lightsaber in any direction. | Move your right hand left or right (x-direction) to rotate the lightsaber from left to right. | Note that the lightsaber’s position on the screen is fixed. The lightsaber rotates with the position of your right hand. |
| Force Push  | Push back all ‘forcible’ objects that are directly in front of the player. This ability costs 10 mana and has a 2 second cooldown before it can be cast again. | With elbows below shoulders, place both hands together to generate a Force Push. | ‘Forcible’ objects include the boxes found throughout the room and the ominous door at the opposite end of the room. |
| Force Pull  | Use the force to draw a distant object into your reach. The selected object will appear and remain directly in front of the user until acted upon. This ability costs 10 mana and has no cooldown before it can be cast again. | Move your left hand from left to right (x-direction) in order to select a forcible object. Once selected, an object will be highlighted in blue. In order to pull the last selected object, raise your right arm straight above your head. | ‘Forcible’ objects include the boxes found throughout the room and the ominous door at the opposite end of the room. |
| Force Throw  | Use the force to throw the ‘forcible’ object that you are currently pulling towards you. The pulled object can be held for a fixed maximum time before being launched forward. This ability costs 10 mana and has a cooldown equivalent to the time needed for your stamina bar to refill. | Maintain the Force Pull gesture until your stamina (yellow resource bar) has been depleted. | If released early, before your stamina has been completely depleted, the pulled object will be let go and will fall, by means of gravity, from it’s current position. |
| Force Heal  | Use the force to restore any of your missing mana or health. This ability costs no mana and has no cooldown. | Raise your left arm straight above your head. | While actively using this ability, a particle animation will surround the user. These particles will slow down during the timewind ability and will dissipate once the gesture has been completed. |
| Force Time: Timewind  | Use the force to undo recent movements. During this ability, the user is free to move as they choose. Other objects affected by the timewind ability will appear to move slower and will rewind to their position 4 seconds before this ability was cast. This ability costs 51 mana and has a 4 second cooldown (surprisingly enough). | With elbows above shoulders, place both hands together to initiate a time(re)wind. | ‘Forcible’ objects include boxes, the Imperial droids, and the ships outside of the Training Room. During timewind, the movement of these objects will be significantly slowed. Upon completion, these objects will revert back to their previous position. |

![](images/jeditrainer1.png?raw=true)

![](images/jeditrainer2.png?raw=true)

![](images/jeditrainer3.png?raw=true)

![](images/jeditrainer4.png?raw=true)

## Future Work
In order to calibrate hueristics for players of variable size, we will add a Debug.Log to the Update() in order to note the following:
* which gesture (or non-gesture) is being recognized in the current frame
* location of kinect joints
* ratio/distance between joints
* make it CSV format

## Division of Work

### Brandon

* Droid prefab -Visual mesh downloaded from 3dwarehouse.sketchup.com
	* TrainingDroidBehavior.cs
	* Droid AI

* Waypoint prefab
	* WaypointBehavior.cs

* Bullet prefab
	* Visual mesh created
	* BulletBehavior.cs

* Code debug and heuristic calibration for all force abilities
	* Assist with the debugging of force abilities
	* Assist with the calibration of the gesture heuristics used for activating force abilities

* Force pull (raycasting selection)
	* Create the ray necessary to select ‘forcible’ objects with a gesture

### Joe

* Lightsaber
	* Visual mesh downloaded from Asset store
	* SaberManager.cs 

* First Person Controller
	* ForceManager.cs
	* GUI: Health, mana, stamina bars
	* Healing particles downloaded from Asset store
	* Force spawn: where the force abilities spawn from (if applicable)
	
* Environment
	* ShipMove.cs
	* Training room built using ProBuilder
	* Door frame mesh downloaded from Asset store
	* Door programmed to open when “forced” on
	* Star Destroyer mesh downloaded from 3D Warehouse
	* Star Destroyers used in start menu and main scene
	* Skybox downloaded from Asset store
	* Music bought from soundtrack
	* Forcible boxes in environment to help practice force abilities
	* Blinking lights programmed to turn on when door is opened using the force

* Main Menu
	* MenuScript.cs

* Pause Menu
	* PauseMenuScript.cs

* Failed Menu
	* PauseMenuScript.cs

* Force push: Push objects in the immediate vicinity away from the player
	* ForceManager.cs
	* Heuristics

* Force pull: After raycasting an object pull it in front of the player. When held, stamina is built to force throw the object
	* ForceManager.cs
	* Heuristics

* Force throw: Using stamina, throw the object forward away from the player
	* ForceManager.cs
	* Heuristics

* Force healing: Heals the player and restores mana
	* ForceManager.cs
	* Heuristics

* Force future (rewind time): When activated, the scene will continue to run normally for three seconds. The scene runs at half the speed during the next second (two seconds in length). After the slowed second everything returns to the position it was in four seconds ago, not including the player. This gives the player a look into the future to know what will happen in the next four seconds.
	* ForceManager.cs
	* Heuristics

* Raycasting: Select an object in the environment. Raycasting changes the color of the object when raycasted (highlighted). The object will turn blue, but whatever part of the object is blocked by another project will appear light blue.
	* ForceManager.cs
	* Outline_Diffuse.shader from YouTube tutorial
	* Heuristics
* Laser reflected do destroy other drones, initial lasers do not.
