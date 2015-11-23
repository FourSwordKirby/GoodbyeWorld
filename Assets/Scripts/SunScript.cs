using UnityEngine;
using System.Collections;

public class SunScript : InteractableScript {
    private float darknessTransparency = 0.0f;
	private bool increasing;
	private bool created;
	private bool selected;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (created) {
			Vector2 pos = transform.localPosition;
			if (pos.y >= 3.0f) {
				pos.y = 3.0f;
				created = false;
			} else {
				pos.y += 0.2f;
			}
			transform.localPosition = pos;
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
		GameObject darkness = GameObject.Find("Darkness");
		darkness.layer = 5; //move to foreground
		
		//start sun from off screen and come up over horizon lmao
		Vector2 pos = transform.localPosition;
		pos.y = -5.0f;
		transform.localPosition = pos;
		created = true;
		
		//ensure that sun is covered by darkness
		Vector3 position = darkness.transform.localPosition;
		position.y = -2;
		position.z -= 2;
		darkness.transform.localPosition = position;
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
        if ((darknessTransparency > 0.7 && radians > 0) || (darknessTransparency <= 0.0 && radians < 0))
            return;
        darknessTransparency += (radians / (2.0f * Mathf.PI)) * 0.1f;
        GameObject darkness = GameObject.Find("Darkness");

        Color oldColor = darkness.GetComponent<SpriteRenderer>().color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, darknessTransparency);

        Debug.Log(darknessTransparency);

        darkness.GetComponent<SpriteRenderer>().color = newColor;
    }
}
