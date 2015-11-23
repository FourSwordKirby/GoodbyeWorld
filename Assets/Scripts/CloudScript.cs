using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CloudScript : InteractableScript {

	private GameObject selectionBox;
    public List<GameObject> clouds;
	private bool selected;
	private bool increasing;

    private bool creationAnim;

	// Use this for initialization
	void Start () {
		selected = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (creationAnim)
        {
            Turn(0.1f);
            float minDistance = 100;
            foreach (GameObject cloud in clouds)
            {
                minDistance = Mathf.Min(Mathf.Abs(cloud.transform.position.x), minDistance);
            }
            if (minDistance < 3.5f)
                creationAnim = false;
        }
		if (selected) {
			foreach (GameObject cloud in clouds) {
				Color color = cloud.GetComponent<SpriteRenderer>().color;
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
				cloud.GetComponent<SpriteRenderer>().color = color;
			}
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
		foreach (GameObject cloud in clouds) {
			Color color = cloud.GetComponent<SpriteRenderer> ().color;
			color.a = 1.0f;
			cloud.GetComponent<SpriteRenderer> ().color = color;
		}
		selected = false;
    }

    //Things that happen on object creation
    override public void Create()
    {
        this.creationAnim = true;
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
        float minDistance = 100;
        foreach (GameObject cloud in clouds)
        {
            minDistance = Mathf.Min(Mathf.Abs(cloud.transform.position.x), minDistance);
        }
        if (minDistance < 3.5f && radians > 0)
            return;

        float scale = 1 - (radians / (2.0f * Mathf.PI)) * 1f;
        foreach (GameObject cloud in clouds)
        {
            cloud.transform.localPosition = new Vector2(cloud.transform.localPosition.x * scale, cloud.transform.localPosition.y);
        }
    }
}
