using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnythingThatTouchesThis : MonoBehaviour {

	void OnCollisionEnter(Collision coll)
    {
        Destroy(coll.collider.gameObject);
    }
}
