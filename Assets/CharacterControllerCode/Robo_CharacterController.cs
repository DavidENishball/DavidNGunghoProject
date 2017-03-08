using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Robo_CharacterController : MonoBehaviour {

    // Public for debug purposes.  Make private later.
    public bool isOnGround;
    public bool isPunching;
    public float maximumHorizontalSpeed;
    public float jumpVelocity;

    public float accelerationPerSecond = 1;

    public float currentHorizontalSpeed = 0;

    public float groundedRaycastDistance = .2f;

    // Used to control turn speed.
    protected Vector3 previousHorizontalVector;

    // Degrees per second at which the character turns.
    public float rotationSpeed = 720;

    protected Rigidbody rigidbody;


	// Use this for initialization
	void Start () {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
	}
	
    public void CharacterMove(Vector3 movementDirection)
    {
        // Flatten the input to ensure horizontal movement.
        Vector3 flattenedMovementDirection = movementDirection;
        flattenedMovementDirection.Scale(new Vector3(1, 1, 0));
        if (flattenedMovementDirection.sqrMagnitude != 1)
        {
            flattenedMovementDirection.Normalize();
        }

        CheckIfOnGround();
        


        rigidbody.velocity = flattenedMovementDirection * maximumHorizontalSpeed;

        if (flattenedMovementDirection != Vector3.zero)
        {
            previousHorizontalVector = flattenedMovementDirection;
        }
        currentHorizontalSpeed = Vector3.Scale(rigidbody.velocity, new Vector3(1, 0, 1)).magnitude;
        //Debug.Log(rigidbody.velocity);
    }

	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, rigidbody.velocity, Color.black, 0, false);
	}

    private void FixedUpdate()
    {
        if (previousHorizontalVector != Vector3.zero)
        {
            transform.forward =  Vector3.RotateTowards(transform.forward, previousHorizontalVector, Mathf.Deg2Rad * (Time.fixedDeltaTime * rotationSpeed), 360);
            
        }
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
}
