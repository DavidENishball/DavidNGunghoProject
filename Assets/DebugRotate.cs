using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRotate : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 180 * Time.deltaTime);
	}
}
