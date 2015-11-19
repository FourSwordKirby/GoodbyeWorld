using UnityEngine;
using System.Collections;

//Interface for all things that are interactable
public abstract class InteractableScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //What happens when you select the object
    public abstract void Enter();

    //What happens when you deselect the object
    public abstract void Exit();

    //Things that happen on object creation
    public abstract void Create();

    //Things that happen on object deletion
    public abstract void Destroy();

    //Object's response to bringing up the wiimote
    public abstract void Lift();

    //Object's response to bringing down the wiimote
    public abstract void Throw();

    //Object's response to turning the wiimote
    public abstract void Turn();
}
