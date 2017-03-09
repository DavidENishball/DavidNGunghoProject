using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRunner_GivePointsOnContact : MonoBehaviour {

    public float pointValue = 10;
    private void OnCollisionEnter(Collision collision)
    {

        var robo = collision.gameObject.GetComponent<Robo_CharacterController>();
        if (robo != null)
        {
            if (RoboRunnerSceneManager.Instance != null)
            {
                RoboRunnerSceneManager.Instance.playerScore += pointValue;
            }
            Destroy(this.gameObject);
        }
    }
}
