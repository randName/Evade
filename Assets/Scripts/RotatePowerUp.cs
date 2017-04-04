using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour {
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(new Vector3(23, 23, -23)*Time.deltaTime);
	}
}
