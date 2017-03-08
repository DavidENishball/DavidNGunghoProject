using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo_AnimationScript : MonoBehaviour {

    public Robo_CharacterController character;
    public Animator animator;

    public const string ANIM_PROP_SPEED = "HorizontalMovementSpeed";
    public const string ANIM_PROP_GROUND = "OnGround";
    public const string ANIM_PROP_PUNCHING = "IsPunching";
    public const string ANIM_PROP_JUMPING = "IsJumping";

    public float animationPlaySpeed;
    public float speedScaleDamping = 0.9f;

    // How fast the character's normal walk cycle is.  Used to normalize.
    public float defaultWalkAnimationSpeed = 1;

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

        character.Jumped += Character_Jumped;
    }

    private void Character_Jumped(Robo_CharacterController argCharacter)
    {
        if (argCharacter == character)
        {
            animator.SetTrigger(ANIM_PROP_JUMPING);
        }
    }


    // Update is called once per frame
    void Update ()
    {
        

        // Because this is an endless runner, we want to offset the character's velocity to make it appear that robo is walking even when still. 
        if (RoboRunnerSceneManager.Instance != null)
        {
            character.speedScale = RoboRunnerSceneManager.Instance.scrollSpeed;

            animationPlaySpeed = character.rigidbody.velocity.x + RoboRunnerSceneManager.Instance.scrollSpeed;
            animationPlaySpeed /= defaultWalkAnimationSpeed;
            animationPlaySpeed *= speedScaleDamping;

            animator.SetFloat(ANIM_PROP_SPEED, animationPlaySpeed, 0.1f, Time.deltaTime);
            animator.speed = animationPlaySpeed;
        }
        else
        {
            animationPlaySpeed = character.rigidbody.velocity.x / defaultWalkAnimationSpeed * speedScaleDamping;
            animator.SetFloat(ANIM_PROP_SPEED, animationPlaySpeed, 0.1f, Time.deltaTime);
            animator.speed = animationPlaySpeed;
        }
        animator.SetBool(ANIM_PROP_GROUND, character.isOnGround);
        
    }

}
