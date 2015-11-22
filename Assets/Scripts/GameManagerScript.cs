using UnityEngine;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	
	private List<InteractableScript> objects;
	int selection; //the object that is currently selected
	public InteractableScript sun;
	
	// Use this for initialization
	void Start () {
		objects = new List<InteractableScript> ();
		objects.Add (sun);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void CreateObject() {
		//TODO: decide the order in which objects come in
		Debug.Log ("creat object");

	}
	
	public void DestroyObject() {
		if (objects.Count > 0) {
			objects[selection].Destroy ();
			objects.Remove (objects[selection]);
		}
		Debug.Log ("destroy object");
		
	}
	
	//cycle through all th different objects
	public void ChangeSelection(bool next) {
		if (objects.Count > 0) {
			objects[selection].Exit();
			if (next)
				selection = (selection + 1) % objects.Count;
			else 
				selection = (selection + objects.Count - 1) % objects.Count;
			objects [selection].Enter ();
		}
		Debug.Log ("current index " + selection);
	}
	
	public void TurnSun(float radians) {
		Debug.Log ("Turn sun this many degrees " + radians);
		sun.GetComponent<SunScript> ().Turn (radians);
	}
}

