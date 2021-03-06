﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CloudScript : InteractableScript {
    public List<GameObject> clouds;
	private List<string> rain;
	private bool selected;
	private bool increasing;

    private bool creationAnim;

	// Use this for initialization
	void Start () {
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
		selected = false;
		rain = new List<string> ();
		rain.Add ("PentagonObjects");
		rain.Add ("TriangleObjects");
		rain.Add ("SquareObjects");
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
		//set position
		for (int i = 0; i < 3; i++) {
			//make it rain
			int index = Random.Range (0, rain.Count);
			ShapeAnimator animator = ((GameObject)Instantiate (Resources.Load (rain [index]))).GetComponent<ShapeAnimator> ();

			Vector3 pos = animator.transform.localPosition;
			pos.x = (Random.value * 20) - 10;
			pos.y = 0;
			pos.z += 0.2f;
			animator.transform.localPosition = pos;

			//make smaller
			animator.transform.localScale *= 0.20f;
		}
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

        float scale = 1 - (radians / (2.0f * Mathf.PI)) * 0.251f;
        foreach (GameObject cloud in clouds)
        {
            cloud.transform.localPosition = new Vector2(cloud.transform.localPosition.x * scale, cloud.transform.localPosition.y);
        }
    }
}
