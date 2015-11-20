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
	
	// Use this for initialization
	void Start () {
		
	}
	
	//for Wiimote
	float calZ = -1;
	float calX = -100;
	float smooth = 20;
	float accX = 0, accY = 0, accZ = 0;
	private bool zPressed;
	private bool xPressed;
	private bool godmode;
	
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
	
	void UpdateTimeStamp() {
		lastAction = DateTime.Now.Ticks;
	}
	
	bool CanActionPerformNow() {
		return (DateTime.Now.Ticks - lastAction) > 1000; //TODO: probably want to put it at 1.5 seconds, need to test this out to see how long it actually is
	}
	
	void Awake ()
	{	
		zPressed = false;
		godmode = true;
		lastAction = 0;
	}
	
	void Update()
	{	
		//(1) BUTTON: toggle God mode
		if (Input.GetKey (KeyCode.Return)) {
			godmode = !godmode;
		}
		
		//A BUTTON: control rotation of the sun
		if (Input.GetKey (KeyCode.X)) {
			GetWiimoteCoords();
			
		}
		
		if (godmode) {
			//ARROW KEYS: switch between selected objects
			if (Input.GetKey (KeyCode.LeftArrow)) {
				//go back one index in the global array of objects
			}  else if (Input.GetKey (KeyCode.RightArrow)) {
				//go forward one index in the global array of objects
			}
			
			if (CanActionPerformNow) {
				//LIFT
				if (accY > 5) {
					
				}
				//THROW
				if (accY < -5) {
				}
			}
		}
	}
}

