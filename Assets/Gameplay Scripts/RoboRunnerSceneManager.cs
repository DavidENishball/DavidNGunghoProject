using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRunnerSceneManager : MonoBehaviour {

    public static RoboRunnerSceneManager Instance;

    public float scrollSpeed = 1.8f;
    
	// Use this for initialization
	void Start () {
        // Singleton./
		if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
