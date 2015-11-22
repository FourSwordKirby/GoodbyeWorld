using UnityEngine;
using System.Collections;

public class SunScript : InteractableScript {

    private GameObject selectionBox;
    private float darknessTransparency = 0.0f;

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
