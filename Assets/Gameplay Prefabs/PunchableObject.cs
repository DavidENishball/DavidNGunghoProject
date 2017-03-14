using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableObject : MonoBehaviour {
    public int scoreValue = 0;

	public void IsPunched(Vector3 punchOrigin, Vector3 punchForce)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && rb.isKinematic == false)
        {
            float lateralForceRoll = Random.value;
            rb.AddForce(punchForce + Vector3.forward * 100 * (lateralForceRoll > 0.5f ? -1 : 1 ));
            
            rb.AddTorque(new Vector3(0, 0, -punchForce.y));
            
            Destroy(gameObject, 2);
        }
        else
        {
            Destroy(gameObject);
        }

        // Simply increment score for safety purposes.
        RoboRunnerSceneManager.Instance.playerScore += scoreValue;
    }
}
