using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRunner_KillRoboOnContact : MonoBehaviour {

    protected void OnCollisionEnter(Collision collision)
    {
        var robo = collision.gameObject.GetComponent<Robo_CharacterController>();
        if (robo != null)
        {
            robo.SetDead(true);
        }
    }
}
