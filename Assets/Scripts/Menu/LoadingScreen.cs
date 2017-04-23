using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {
    public float speed = 0.5f;
	
	// Update is called once per frame
	void Update () { //Causes loading screen to feel alive
        Vector2 offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
