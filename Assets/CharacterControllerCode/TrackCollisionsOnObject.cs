﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCollisionsOnObject : MonoBehaviour {

    public Dictionary<Collider, Collision> collidingObjectDictionary = new Dictionary<Collider, Collision>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collidingObjectDictionary.ContainsKey(collision.collider))
        {
            collidingObjectDictionary[collision.collider] = collision;
        }
        else
        {
            collidingObjectDictionary.Add(collision.collider, collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collidingObjectDictionary.Remove(collision.collider);

    }

    private void OnCollisionStay(Collision collision)
    {
        
        collidingObjectDictionary[collision.collider] = collision;
    }
}
