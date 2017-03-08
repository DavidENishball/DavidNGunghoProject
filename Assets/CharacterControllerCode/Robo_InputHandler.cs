using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo_InputHandler : MonoBehaviour {

    public Robo_CharacterController character;

    // Determine if the input is relative to a transform, such as a camera.
    public Transform cameraTransform;

    // If true, input will be constrained to the horizontal plane.
    public bool lockToHorizontalPlane = false;

    public Vector3 finalInputVector;

	// Use this for initialization
	void Start () {
        if (character == null)
        {
            character = GetComponent<Robo_CharacterController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        // read inputs
        float h = Input.GetAxisRaw("Horizontal");
        float v = lockToHorizontalPlane ? 0 : Input.GetAxisRaw("Vertical");

        // calculate move direction to pass to character
        if (cameraTransform != null)
        {
            // calculate camera relative direction to move:
            Vector3 forwardVector =  Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;

            // Assume there's no roll so we can use the camera's transform's right without worry.
            finalInputVector =  v * forwardVector + h * cameraTransform.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            finalInputVector = v * Vector3.forward + h * Vector3.right;
        }

        // pass all parameters to the character control script
        character.CharacterMove(finalInputVector);
    }
}
