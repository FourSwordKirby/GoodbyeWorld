﻿using UnityEngine;
using System.Collections;

public class ShapeAnimator : MonoBehaviour
{
    public float decayRate = 1.0f;
    public float lifeSpan = 10.0f;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        // Define animator.
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO if some conditions are met increase/decrease decay rate
        lifeSpan -= Time.deltaTime * decayRate;
        if (lifeSpan <= 0)
        {
            animator.SetBool("Dead", true);
        }
        if (lifeSpan <= -1.15f) 
            Destroy(gameObject);

		//make it fall down to the "ground"
		Vector2 pos = transform.position;
		Vector2 size = GetComponent<SpriteRenderer> ().bounds.size;
		if (pos.y > -3.5 + size.y / 2) {
			pos.y -= 0.1f;
			transform.position = pos;
		}
    }
}
