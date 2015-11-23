using UnityEngine;
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
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Exit")) 
            Destroy(this.gameObject);
    }
}
