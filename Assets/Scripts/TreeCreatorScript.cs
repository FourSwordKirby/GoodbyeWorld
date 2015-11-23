using UnityEngine;
using System.Collections;

public class TreeCreatorScript : InteractableScript {
	
	private GameObject selectionBox;
	private float darknessTransparency = 0.0f;
	public GameObject gameManager;
	private bool selected;
	private bool increasing;
	
	// Use this for initialization
	void Start () {
		selected = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (selected) {
			Color color = GetComponent<SpriteRenderer>().color;
			if (increasing) {
				if (color.a >= 1.0f) {
					increasing = false;
				} else {
					color.a += 0.03f;
				}
			} else {
				if (color.a <= 0.25f) {
					increasing = true;
				} else {
					color.a -= 0.03f;
				}
			}
			GetComponent<SpriteRenderer>().color = color;
		}
	}
	
	//What happens when you select the object
	override public void Enter()
	{
		selected = true;
		
	}
	
	//What happens when you deselect the object
	override public void Exit()
	{
		Color color = GetComponent<SpriteRenderer> ().color;
		color.a = 1.0f;
		GetComponent<SpriteRenderer> ().color = color;
		selected = false;
	}
	
	//Things that happen on object creation
	override public void Create()
	{
	}
	
	//Things that happen on object deletion
	override public void Destroy()
	{
	}
	
	//Object's response to bringing up the wiimote
	override public void Lift()
	{
		InteractableScript tree = ((GameObject)Instantiate (Resources.Load ("Tree"))).GetComponent<InteractableScript>();
		gameManager.GetComponent<GameManagerScript> ().ObjectWasCreated (tree, true);

	}
	
	//Object's response to bringing down the wiimote
	override public void Throw()
	{
	}
	
	//Object's response to turning the wiimote
	override public void Turn(float radians)
	{
	}
}
