using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashText : MonoBehaviour {

    public Text targetText;
    public float countdown = 0;
    public float flashFrequency = 0.3f;
	
	// Update is called once per frame
	void Update ()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = flashFrequency;
            targetText.enabled = !targetText.enabled;
        }
	}
}
