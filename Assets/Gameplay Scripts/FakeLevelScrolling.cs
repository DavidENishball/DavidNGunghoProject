using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLevelScrolling : MonoBehaviour {

    public Vector3 simulatedVelocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (RoboRunnerSceneManager.Instance == null)
        {
            return;
        }
        // Forced scrolling offset.
        Vector3 scrolledOffset = Vector3.left * RoboRunnerSceneManager.Instance.scrollSpeed * Time.deltaTime;
        Vector3 simulatedVelocityOffset = simulatedVelocity * Time.deltaTime;

        transform.position += (scrolledOffset + simulatedVelocityOffset);
	}
}
