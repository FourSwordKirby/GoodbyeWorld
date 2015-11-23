using UnityEngine;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	
	private List<InteractableScript> objects;
	private int selection; //the object that is currently selected
	public int trees;
	public int buildings;
	private int creation;
	private bool canMakeCreators;
	public InteractableScript treeCreator;
	public InteractableScript buildingCreator;
	public InteractableScript clouds;
	
	// Use this for initialization
	void Start () {
		selection = 0;
		trees = 0;
		buildings = 0;
		creation = 0;
		objects = new List<InteractableScript> ();
		treeCreator.GetComponent<SpriteRenderer> ().enabled = false;
		buildingCreator.GetComponent<SpriteRenderer> ().enabled = false;
		foreach (SpriteRenderer renderer in clouds.GetComponentsInChildren<SpriteRenderer>()) {
			renderer.enabled = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//toggle god mode, basically select and deselect
	public void SetGodMode (bool godmode) {
		//entering godmode
		if (objects.Count > 0) {
			if (godmode) {
				objects [selection].Enter ();
			} else {
				objects [selection].Exit ();
			}
		}
	}

	//let the creators update the game manager about objects they've created so we can add it to our list
	public void ObjectWasCreated(InteractableScript spawn, bool tree) {
		spawn.Create ();
		objects.Add (spawn);
		Debug.Log ("Add " + spawn);

		if (tree) {
			++trees;
			GameObject.Find ("TreeCreator").GetComponent<TreeCreatorScript> ().IncreaseLifeSpan ();
		} else {
			++buildings;
			GameObject.Find ("TreeCreator").GetComponent<TreeCreatorScript> ().DecreaseLifeSpan ();
		}
	}

	public void DestroyObject(InteractableScript spawn, bool tree) {
		Debug.Log ("DESTROY: index " + selection + " objects count " + objects.Count);
		InteractableScript newObj = null;
		InteractableScript obj = objects[selection];
		if (obj == spawn) {
			ChangeSelection (true);
			newObj = objects [selection];
		}
		objects.Remove(spawn);
		Debug.Log ("remove " + obj);
		if (obj == spawn) selection = objects.IndexOf (newObj);
		
		if (tree)
			--trees;
		else {
			--buildings;
			GameObject.Find("TreeCreator").GetComponent<TreeCreatorScript>().IncreaseLifeSpan();
		}
	}
	
	public void CreateObject() {
		//create sun
		if (creation == 0) {
			InteractableScript sun = ((GameObject)Instantiate (Resources.Load ("Sun"))).GetComponent<InteractableScript>();
			objects.Add (sun);
			sun.Create ();
			ChangeSelection(true);
		} 
		//create clouds
		else if (creation == 1) {
			//create clouds
			foreach (SpriteRenderer renderer in clouds.GetComponentsInChildren<SpriteRenderer>()) {
				renderer.enabled = true;
			}
			clouds.Create ();
			objects.Add (clouds);
			//let user have the ability to create trees and buildings
			treeCreator.GetComponent<SpriteRenderer> ().enabled = true;
			objects.Add(treeCreator);
			buildingCreator.GetComponent<SpriteRenderer> ().enabled = true;
			objects.Add (buildingCreator);
		}
		++creation;

	}
	
	//cycle through all th different objects
	public void ChangeSelection(bool next) {
		if (objects.Count > 0) {
			if (objects[selection]) objects[selection].Exit();
			if (next)
				selection = (selection + 1) % objects.Count;
			else 
				selection = (selection + objects.Count - 1) % objects.Count;
			objects [selection].Enter ();
		}
	}
	
	public void Turn(float radians) {
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
		if (objects.Count > 0) 
			objects [selection].Throw ();
	}
}

