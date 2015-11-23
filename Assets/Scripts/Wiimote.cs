using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

/* 
 * To use Wiimote, person should run DarwiinRemote and start recording into a file called
 * "wiimote.txt" in the GoodbyeWorld directory. Once that is up and running, run this file
 * and the program will start reading coordinates from there.
 */
public class Wiimote : MonoBehaviour {
	
	public GameObject gameManager;
	private long lastAction; //time stamp of last action
	private bool performable;
	
	private float calZ = -1;
	private float calX = -100;
	private float smooth = 20;
	private float accX = 0, accY = 0, accZ = 0;
	private bool leftPressed;
	private bool rightPressed;
	private bool godmode;
	
	// Use this for initialization
	void Start () {
		performable = true;	
	}

	int GetWiimoteCoords() {
		
		string coordinates = "wiimote.txt";
		string coordinateFile = Path.GetFileName (coordinates);
		string[] values;
		
		//gets the coordinates from the file outputted by DarwiinRemote
		if (System.IO.File.Exists(coordinateFile)){ 
			
			using (StreamReader sr = new StreamReader(coordinateFile)) {
				//read two lines for the labels
				sr.ReadLine();
				sr.ReadLine();
				//get the updated information
				string[] txt = File.ReadAllLines(coordinateFile);
				values = txt[txt.Length-2].Split (new[] { "," }, StringSplitOptions.None);
				
				float tempX = float.Parse(values[1],CultureInfo.InvariantCulture);
				float tempY = float.Parse(values[2],CultureInfo.InvariantCulture);
				float tempZ = float.Parse(values[3],CultureInfo.InvariantCulture);
				
				if (Math.Abs (accX - tempX) > 0.1) {
					accX = tempX;
				}
				if (Math.Abs (accY - tempY) > 0.1) {
					accY = tempY;
				}
				if (Math.Abs (accZ - tempZ) > 0.1) {
					accZ = tempZ-calZ;
				}
			}
		}
		return 0;
	}
	
	void PerformAction () {
		lastAction = DateTime.Now.Ticks;
		performable = false;
	}
	
	void UpdateTimeStamp() {
		if (DateTime.Now.Ticks - lastAction > 18000000) { //1.5 seconds
			performable = true;
			Debug.Log ("okay you can go for it again");
		}
		else
			performable = false;
	}
	
	void Awake ()
	{	
		leftPressed = false;
		rightPressed = false;
		godmode = true;
		lastAction = 0;
	}
	
	void Update()
	{	
		if (!performable) UpdateTimeStamp ();
		
		//(1) BUTTON: toggle God mode
		if (Input.GetKey (KeyCode.Return)) {
			godmode = !godmode;
			Debug.Log ("currently in god mode: " + godmode);
		}
		
		GetWiimoteCoords();
		
		//A BUTTON: control rotation of the sun
		if (Input.GetKey (KeyCode.X)) {
			float rad = -accX * 2 * (float)Math.PI;
			gameManager.GetComponent<GameManagerScript> ().Turn (rad);
		}
		
		if (godmode) {
			//ARROW KEYS: switch between selected objects
			if (Input.GetKey (KeyCode.LeftArrow)) {
				if (!leftPressed) {
					leftPressed = true;
					gameManager.GetComponent<GameManagerScript> ().ChangeSelection (true);
				}
			} else {
				leftPressed = false;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				if (!rightPressed) {
					rightPressed = true;
					gameManager.GetComponent<GameManagerScript> ().ChangeSelection (false);
				}
			} else {
				rightPressed = false;
			}
			
			//LIFT AND THROW
			if (performable) {
				//create object
				if (-accZ > 2.5) {
					PerformAction ();
					gameManager.GetComponent<GameManagerScript> ().Lift();
				}
				//destroy object
				if (-accZ < -2.5) {
					PerformAction ();
					gameManager.GetComponent<GameManagerScript> ().Throw();
				}
			}
		} else {
			//be able to move around
		}
	}
}

