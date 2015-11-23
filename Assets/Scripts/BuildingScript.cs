using UnityEngine;
using System.Collections.Generic;

public class BuildingScript : InteractableScript {
	private bool destroy;
	private bool create;
	private float SCALE = 2.0f;
	private bool selected;
	private bool increasing;
	private long createdTimeStamp;
	public long lifeSpan;
	public float originalScale;

	public List<Sprite> buildingSprites;

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
		if (System.DateTime.Now.Ticks - createdTimeStamp > lifeSpan * 10000000) {
			Destroy();
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

		//prepare for decay
		createdTimeStamp = System.DateTime.Now.Ticks;
	}
	
	//Things that happen on object deletion
	override public void Destroy()
	{
		destroy = true;
		lifeSpan = 20000000;
		GameObject.Find ("GameManager").GetComponent<GameManagerScript> ().DestroyObject (this, false);
	}
	
	//Object's response to bringing up the wiimote
	override public void Lift()
	{
	}
	
	//Object's response to bringing down the wiimote
	override public void Throw()
	{
		Destroy ();
	}
	
	//Object's response to turning the wiimote
	override public void Turn(float radians)
	{
	}
}
