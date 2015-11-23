using UnityEngine;
using System.Collections.Generic;

public class TreeScript : InteractableScript {

	private GameObject selectionBox;
	private float darknessTransparency = 0.0f;
	public List<Sprite> treeSprites;
	public Sprite creepySprite;
	private bool destroy;
	
	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer> ().sprite = treeSprites[Random.Range (0,treeSprites.Count-1)];
		destroy = false;
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
	}
	
	//What happens when you select the object
	override public void Enter()
	{
		selectionBox = GameObject.CreatePrimitive(PrimitiveType.Plane);
		
		//Sets the bounds etc. of the selection box
		selectionBox.transform.Rotate(new Vector3(270, 0, 0));
		selectionBox.transform.localScale *= 0.3f;
		
		selectionBox.transform.SetParent(this.gameObject.transform);
		
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
