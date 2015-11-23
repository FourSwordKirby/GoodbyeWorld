using UnityEngine;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	
	private List<InteractableScript> objects;
	private int selection; //the object that is currently selected
	private int trees;
	private int buildings;
	private int creation;
	private bool canMakeCreators;
	public InteractableScript treeCreator;
	public InteractableScript buildingCreator;

	
	// Use this for initialization
	void Start () {
		selection = 0;
		trees = 0;
		buildings = 0;
		creation = 0;
		objects = new List<InteractableScript> ();
		treeCreator.GetComponent<SpriteRenderer> ().enabled = false;
		buildingCreator.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//let the creators update the game manager about objects they've created so we can add it to our list
	public void ObjectWasCreated(InteractableScript spawn) {
		spawn.Create ();
		objects.Add (spawn);
	}
	
	public void CreateObject() {
		//TODO: decide the order in which objects come in
		Debug.Log ("creat object");

		//create sun
		if (creation == 0) {
			InteractableScript sun = ((GameObject)Instantiate (Resources.Load ("Sun"))).GetComponent<InteractableScript>();
			objects.Add (sun);
			sun.Create ();
			ChangeSelection(true);
		} 
		//create clouds
		else if (creation == 1) {
			//TODO: create clouds
			//let user have the ability to create trees and buildings
			treeCreator.GetComponent<SpriteRenderer> ().enabled = true;
			objects.Add(treeCreator);
			buildingCreator.GetComponent<SpriteRenderer> ().enabled = true;
			objects.Add (buildingCreator);
		}
		++creation;

	}
	
	public void DestroyObject() {
		if (objects.Count > 0) {
			InteractableScript obj = objects[selection];
			ChangeSelection(true);
			objects.Remove(obj);
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

	public void Lift() {
		if (creation <= 1)
			CreateObject ();
		else {
			objects[selection].Lift ();
		}
	}

	public void Throw() {
		if (objects.Count > 0) {
			objects [selection].Throw ();
		}
	}
}

