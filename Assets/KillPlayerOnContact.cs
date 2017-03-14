using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnContact : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Robo_CharacterController robo = collision.collider.gameObject.GetComponent<Robo_CharacterController>();
        if (robo != null)
        {
            robo.SetDead(true);
        }
    }
}
