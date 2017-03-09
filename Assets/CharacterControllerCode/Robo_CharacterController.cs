using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Robo_CharacterController : MonoBehaviour {

    // Public for debug purposes.  Make private later.
    public bool isOnGround;
    public bool isPunching;

    // Independent of the game's scroll speed.
    public float baseHorizontalSpeed;
    public float basePunchDuration = .5f;

    public float horizontalSpeed;

    public float speedScale = 1;

    public float accelerationPerSecond = 1;

    public float jumpVelocity = 5;

    public float groundedRaycastDistance = .2f;

    

    public float punchDuration = .5f;

    // Allows you to make the character fall faster or slower relative to the rest of the game.
    [Range(0,5)]
    public float gravityMultiplier = 1;

    // Used to control turn speed.
    protected Vector3 previousHorizontalVector;

    // Degrees per second at which the character turns.
    public float rotationSpeed = 720;

    public Vector3 flattenedMovementDirection;
    public Rigidbody rigidbody;

    public delegate void RoboCharacterControllerDelegate(Robo_CharacterController character);
    public event RoboCharacterControllerDelegate Jumped;
    public event RoboCharacterControllerDelegate PerformedPunch;

    private bool jumpedThisFrame;
    private bool punchedThisFrame;

    protected Vector3 punchOffset;
    protected Vector3 punchDimensions;


    public bool allowInput = true;

    // Use this for initialization
    void Start () {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
	}
	
    public void CharacterMove(Vector3 movementDirection)
    {
        if (allowInput)
        {
            // Flatten the input to ensure horizontal movement.
            flattenedMovementDirection = movementDirection;
            flattenedMovementDirection.Scale(new Vector3(1, 1, 0));
            if (flattenedMovementDirection.sqrMagnitude != 1)
            {
                flattenedMovementDirection.Normalize();
            }

            CheckIfOnGround();

            horizontalSpeed = baseHorizontalSpeed * speedScale;
            Vector3 newVelocity = flattenedMovementDirection * horizontalSpeed;
            newVelocity.y = rigidbody.velocity.y;
            rigidbody.velocity = newVelocity;

            if (flattenedMovementDirection != Vector3.zero)
            {
                previousHorizontalVector = flattenedMovementDirection;
            }
            //currentHorizontalSpeed = Vector3.Scale(rigidbody.velocity, new Vector3(1, 0, 1)).magnitude;
            //Debug.Log(rigidbody.velocity);

            if (!isOnGround)
            {
                // apply extra gravity.
                Vector3 additionalGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
                rigidbody.AddForce(additionalGravityForce);
                jumpedThisFrame = false;
            }
            else
            {
                if (jumpedThisFrame)
                {
                    jumpedThisFrame = false;
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpVelocity, rigidbody.velocity.z);
                    if (Jumped != null)
                    {
                        Jumped.Invoke(this);
                    }
                }
            }
        }
    }

    public void JumpInput()
    {
        if (isOnGround)
        {
            jumpedThisFrame = true;
        }

    }

    public void PunchInput()
    {
        if (!isPunching)
        {
            punchedThisFrame = true;
            StartCoroutine(PunchCoroutine());
        }
    }

    IEnumerator PunchCoroutine()
    {
        isPunching = true;
        if (PerformedPunch != null)
        {
            PerformedPunch.Invoke(this);
        }
        
        yield return new WaitForSeconds(punchDuration);
        isPunching = false;
    }


    protected void PunchEffect()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + (transform.rotation * punchOffset), punchDimensions / 2, transform.forward);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit foundHit = hits[i];

            GameObject otherGameObject =  foundHit.collider.gameObject;
            // Implement hitable interface.
        }
    }

	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, rigidbody.velocity, Color.black, 0, false);
        /*
        if (Input.GetKeyDown(KeyCode.F) && isOnGround)
        {
            
           
            //isOnGround = false;
            jumpedThisFrame = true;
        }
        */
	}


    void CheckIfOnGround()
    {
        RaycastHit hit;

        // Assumes the object's origin is at the base.
        if (Physics.Raycast(transform.position + (Vector3.up * 0.01f), Vector3.down, out hit, groundedRaycastDistance))
        {
            isOnGround = true;
           // m_IsGrounded = true;
           // m_Animator.applyRootMotion = true;
        }
        else
        {
            isOnGround = false;
            //m_GroundNormal = Vector3.up;
           // m_Animator.applyRootMotion = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit by " + collision.collider.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited by " + collision.collider.name);
    }

    private void OnCollisionStay(Collision collision)
    {

    }
}
