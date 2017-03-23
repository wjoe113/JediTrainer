# JediTrainer
Jedi Training Simulator

README.txt

/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

 In this project we created a Jedi training simulator to help young Padawan's practice the ways
 of the force. The force involves force push, force pull, force throw, healing, and rewinding
 time. A lightaber is a jedi's weapon of choice. It is lightweight, versitile, and comes in many
 pretty colors. On the ship, the Padawan will be trained using three re-modified Empirial drones.
 Don't worry, they're mostly harmless.
 
 Future Work:
	 - In order to calibrate hueristics for players of variable size, we will add a Debug.Log to 
		the Update() in order to note the following:
		+ which gesture (or non-gesture) is being recognized in the current frame
		+ location of kinect joints
		+ ratio/distance between joints
		+ make it CSV format
	 - Laser reflected do destroy other drones, initial lasers do not.
   
   SUMMARY
In this project, we created a Jedi training simulator to help young Padawan's (Padajuans) practice the ways of the force. The force involves force push, force pull, force throw, healing, and rewinding time. Using a Microsoft 360 Kinect 1.0 and the related MS_Kinect SDK v1.0 from the Unity Asset store, the player can perform their abilities using heuristics and gestures read by the Kinect. A lightsaber is a Jedi's weapon of choice. It is lightweight, versatile, and comes in many pretty colors. On the ship, the player will train against three re-modified Imperial drones; don't worry, they're mostly harmless.
DIVISION OF WORK
Brandon
•	Droid prefab
o	Visual mesh downloaded from 3dwarehouse.sketchup.com
o	TrainingDroidBehavior.cs
o	Droid AI
•	Waypoint prefab
o	WaypointBehavior.cs
•	Bullet prefab
o	Visual mesh created
o	BulletBehavior.cs
•	Code debug and heuristic calibration for all force abilities
o	Assist with the debugging of force abilities
o	Assist with the calibration of the gesture heuristics used for activating force abilities
•	Force pull (raycasting selection)
o	Create the ray necessary to select ‘forcible’ objects with a gesture
Joe
•	Lightsaber
o	Visual mesh downloaded from Asset store
o	SaberManager.cs 
•	First Person Controller
o	ForceManager.cs
o	GUI: Health, mana, stamina bars
o	Healing particles downloaded from Asset store
o	Force spawn: where the force abilities spawn from (if applicable)
•	Environment
o	ShipMove.cs
o	Training room built using ProBuilder
o	Door frame mesh downloaded from Asset store
o	Door programmed to open when “forced” on
o	Star Destroyer mesh downloaded from 3D Warehouse
o	Star Destroyers used in start menu and main scene
o	Skybox downloaded from Asset store
o	Music bought from soundtrack
o	Forcible boxes in environment to help practice force abilities
o	Blinking lights programmed to turn on when door is opened using the force
•	Main Menu
o	MenuScript.cs
•	Pause Menu
o	PauseMenuScript.cs
•	Failed Menu
o	PauseMenuScript.cs
•	Force push: Push objects in the immediate vicinity away from the player
o	ForceManager.cs
o	Heuristics
•	Force pull: After raycasting an object pull it in front of the player. When held, stamina is built to force throw the object
o	ForceManager.cs
o	Heuristics
•	Force throw: Using stamina, throw the object forward away from the player
o	ForceManager.cs
o	Heuristics
•	Force healing: Heals the player and restores mana
o	ForceManager.cs
o	Heuristics
•	Force future (rewind time): When activated, the scene will continue to run normally for three seconds. The scene runs at half the speed during the next second (two seconds in length). After the slowed second everything returns to the position it was in four seconds ago, not including the player. This gives the player a look into the future to know what will happen in the next four seconds.
o	ForceManager.cs
o	Heuristics
•	Raycasting: Select an object in the environment. Raycasting changes the color of the object when raycasted (highlighted). The object will turn blue, but whatever part of the object is blocked by another project will appear light blue.
o	ForceManager.cs
o	Outline_Diffuse.shader from YouTube tutorial
o	Heuristics
LIST OF SCRIPTS
•	BulletBehavior.cs - Used by each blaster laser instantiated by a Training Droid. Instructs lasers to move forward at a variable speed. Upon collision with a reflective surface, lasers are reflected back at enemies. Upon collision with a destructive surface, lasers are destroyed. If the laser makes no contact with any other object, it is destroyed after a variable amount of time.
•	CameraRotate.cs - Rotates the camera slowly across the stars to create a smooth main menu background.
•	MenuScript.cs - Regulates the main menu scene.
•	Outline_Diffuse.shader - Shader used in the material for highlighting objects when raycasted
•	PauseMenuScript.cs - Regulates the pause and failed menus.
•	RewindScript.cs - Used in all “forcible” objects. Stores the position when rewind is initiated, then returns the object to that position after four seconds.
•	SaberManager.cs - Rotates the saber to the player’s right hand position in 3D space.
•	ForceManager.cs - Regulates the player’s force abilities, stats (health, mana, stamina), and movement (future addition).
•	ShipMove.cs - Slowly moves ships forward through space.
•	TrainingDroidBehavior.cs - Used by each of the flying droids in the training room. Instructs droids to coherently roam between the provided waypoints without colliding onto the same waypoint. Upon arrival at a waypoint, droids are instructed to face the user and then shoot a variable number of bullets at a variable speed after a variable amount of time has passed.
•	WaypointBehavior.cs -  Describes each waypoints current occupation status in order to prevent multiple droids from grouping on a single waypoint.
HOW TO PLAY
Main Menu
Navigation from the main menu must be done with the mouse. From the main menu, users can:
1.	Modify game options by selecting “Options,” 
2.	Begin the demo by selecting “Play Demo”, or
3.	Exit the game by selecting “Exit.” When “Exit” is selected another menu will appear to confirm the player’s selection.
Options
From the options menu, volume and resolution may be adjusted. Volume is adjusted by updating the provided slider. Resolution is adjusted by selecting one of the provided resolution settings (1280x720, 1600x900, and 1920x1080).
Gameplay
Upon starting the game, players will find themselves isolated in a room filled with boxes and an ominous door between two inactive lights. Gameplay is controlled with a Kinect 1.0. The player’s position remains static throughout the game; that is to say that there is no need for the player to navigate throughout the room. As a Jedi in a strange place, it is necessary to learn what abilities you are capable of performing. The following table enumerates and describes the Force Abilities available to the player, as well as how to successfully produce the gesture necessary to activate each ability.
