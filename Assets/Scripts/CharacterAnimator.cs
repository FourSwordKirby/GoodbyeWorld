using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour
{
	public float speed = 1.5f;
	string direction = "right"; // Facing right to start.
	Animator animator;
	public bool move;
	
	// Use this for initialization
	void Start ()
	{
		// Define animator.
		animator = gameObject.GetComponent<Animator>();
		move = false;
	}
	
	void setDirection(string dir)
	{
		if (direction != dir)
		{
			if (direction == "right")
			{
				transform.Rotate(0, 180, 0);
			}
			else if (direction == "left")
			{
				transform.Rotate(0, -180, 0);
			}
			direction = dir;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (move) {
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.position += Vector3.left * speed * Time.deltaTime;
				setDirection("left");
				animator.SetBool ("Moving", true);
			}
			else if (Input.GetKey (KeyCode.RightArrow))
			{
				transform.position += Vector3.right * speed * Time.deltaTime;
				setDirection ("right");
				animator.SetBool ("Moving", true);
			}
			else 
			{
				animator.SetBool ("Moving", false);
			}
		}
		/*
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		*/
	}
}
