using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo_AnimationScript : MonoBehaviour {

    public Robo_CharacterController character;
    public Animator animator;

    public const string ANIM_PROP_SPEED = "HorizontalMovementSpeed";
    public const string ANIM_PROP_GROUND = "OnGround";
    public const string ANIM_PROP_PUNCHING = "IsPunching";

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (character == null)
        {
            character = GetComponent<Robo_CharacterController>();
        }
    }

	
	// Update is called once per frame
	void Update ()
    {
        animator.SetFloat(ANIM_PROP_SPEED, character.currentHorizontalSpeed / character.maximumHorizontalSpeed, 0.1f, Time.deltaTime);
        animator.SetBool(ANIM_PROP_GROUND, character.isOnGround);
    }
}
