using UnityEngine;
using System.Collections.Generic;

public class BuildingScript : InteractableScript {
	private GameObject selectionBox;
	private float darknessTransparency = 0.0f;
	private bool destroy;
	private float originalScale;
	private bool create;
	private float SCALE = 2.0f;

	public List<Sprite> buildingSprites;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (destroy) {
			if (transform.localScale.y < 0) {
				Destroy (gameObject);
			}
			Vector2 scale = transform.localScale;
			scale.y = transform.localScale.y - (float)0.3;
			transform.localScale = scale;
		}
		if (create) {
			Vector2 scale = transform.localScale;
			if (scale.y > SCALE) {
				scale.y = SCALE;
				transform.localScale = scale;
				create = false;
			} else {
				scale.y += (float)0.15;
				transform.localScale = scale;
			}
		}
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
		//show
		this.GetComponent<SpriteRenderer> ().sprite = buildingSprites[Random.Range (0,buildingSprites.Count)];
		destroy = false;

		//make bigger
		transform.localScale *= SCALE;

		//place in right location
		Vector2 location = transform.localPosition;
		location.y = (float)-3.5 + GetComponent<SpriteRenderer> ().bounds.size.y/2;
		location.x = (Random.value * 20)- 10;
		transform.localPosition = location;

		//prepare for animation
		originalScale = GetComponent<SpriteRenderer>().bounds.size.y;
		Debug.Log ("BEGIN: " + originalScale);
		Vector2 scale = transform.localScale;
		scale.y = 0;
		transform.localScale = scale;
		create = true;
	}
	
	//Things that happen on object deletion
	override public void Destroy()
	{
		destroy = true;
	}
	
	//Object's response to bringing up the wiimote
	override public void Lift()
	{
	}
	
	//Object's response to bringing down the wiimote
	override public void Throw()
	{
		GameObject.Find ("GameManager").GetComponent<GameManagerScript> ().DestroyObject (false);
		Destroy ();
	}
	
	//Object's response to turning the wiimote
	override public void Turn(float radians)
	{
	}
}
