using UnityEngine;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	
	private List<InteractableScript> objects;
	private int selection; //the object that is currently selected
	private int trees;
	private int buildings;
	private int creation;
	public InteractableScript sun;
	public InteractableScript treeCreator;
	public InteractableScript buildingCreator;

	
	// Use this for initialization
	void Start () {
		selection = -1;
		trees = 0;
		buildings = 0;
		creation = 0;
		objects = new List<InteractableScript> ();
		objects.Add (sun);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void CreateObject() {
		//TODO: decide the order in which objects come in
		Debug.Log ("creat object");

		++creation;

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
	
	public void Turn(float radians) {
		Debug.Log ("Turn this many degrees " + radians);
		if (objects.Count > 0)
			objects[selection].Turn (radians);
	}
}

