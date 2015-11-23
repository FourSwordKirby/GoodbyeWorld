using UnityEngine;
using System.Collections.Generic;

public class TreeScript : InteractableScript {

	private GameObject selectionBox;
	private float darknessTransparency = 0.0f;
	public List<Sprite> treeSprites;
	public Sprite creepySprite;
	private bool destroy;
	private float originalScale;
	private bool create;
	private float SCALE = 0.5f;
	private bool selected;
	private bool increasing;
	
	// Use this for initialization
	void Start () {
		selected = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (destroy) {
			if (transform.localScale.y < 0) {
				Destroy (gameObject);
			}
			Vector2 scale = transform.localScale;
			scale.y = transform.localScale.y - (float)0.10;
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
		//show creepy sprite if there are too many buildings
		GameManagerScript gameManager = ((GameObject)GameObject.Find ("GameManager")).GetComponent<GameManagerScript>();
		if (gameManager.buildings >= gameManager.trees + 4)
			this.GetComponent<SpriteRenderer> ().sprite = creepySprite;
		else 
			this.GetComponent<SpriteRenderer> ().sprite = treeSprites[Random.Range (0,treeSprites.Count)];
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
		GameObject.Find ("GameManager").GetComponent<GameManagerScript> ().DestroyObject (true);
		Destroy ();
	}
	
	//Object's response to turning the wiimote
	override public void Turn(float radians)
	{
	}
}
