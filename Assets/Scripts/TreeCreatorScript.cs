using UnityEngine;
using System.Collections;

public class TreeCreatorScript : InteractableScript {
	
	private GameObject selectionBox;
	private float darknessTransparency = 0.0f;
	public GameObject gameManager;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	//What happens when you select the object
	override public void Enter()
	{
		selectionBox = GameObject.CreatePrimitive(PrimitiveType.Plane);
		
		//Sets the bounds etc. of the selection box
		selectionBox.transform.Rotate(new Vector3(270, 0, 0));
		selectionBox.transform.localScale *= 0.3f;
		selectionBox.transform.SetParent(this.gameObject.transform);
		selectionBox.transform.localPosition = new Vector2 (0, 0);
		
	}
	
	//What happens when you deselect the object
	override public void Exit()
	{
		Destroy(selectionBox);
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
		InteractableScript building = ((GameObject)Instantiate (Resources.Load ("Tree"))).GetComponent<InteractableScript>();
		gameManager.GetComponent<GameManagerScript> ().ObjectWasCreated (building);

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
