using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableObject : MonoBehaviour {
    public int scoreValue = 0;

	void IsPunched(Vector3 punchOrigin, Vector3 punchForce)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && rb.isKinematic == false)
        {
            rb.AddForce(punchForce);
        }
        else
        {
            Destroy(gameObject);
        }

        // Simply increment score for safety purposes.
        RoboRunnerSceneManager.Instance.playerScore += scoreValue;
    }
}
