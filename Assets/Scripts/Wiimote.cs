using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Wiimote : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//for Wiimote
	float calZ = -1;
	float calX = -100;
	float smooth = 20;
	float accX = 0, accY = 0, accZ = 0;
	private bool zPressed;
	private bool xPressed;

	
	int getWiimoteCoords() {
		
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
	
	void Awake ()
	{	
		zPressed = false;
	}
	
	/* 
	 * To use Wiimote, person should run DarwiinRemote and start recording into a file called
	 * "wiimote.txt" in the SceneStudio directory. Once that is up and running, run this file
	 * and the program will start reading coordinates from there.
	 */
	void Update()
	{	
		//B BUTTON
		if (Input.GetKey (KeyCode.Z)) {
			if (!zPressed) {
				//first time pressing Z, place cursor in center of canvas
				zPressed = true;
				
				
			} else {
				//Z has been pressed for a while start moving the cursor
				getWiimoteCoords ();
			}
			// A BUTTON
			if (Input.GetKey (KeyCode.X)) {
				//stuff
			}
		}
		if (!Input.GetKey (KeyCode.Z)) {
			//hide cursor
			zPressed = false;
		}
		if (!Input.GetKey (KeyCode.X)) {
			xPressed = false;
		}
	}
}
