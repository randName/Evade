using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour {
	
	
	void FixedUpdate () {
        transform.Rotate(new Vector3(23, 23, -23)*Time.deltaTime); //make the power ups feel alive
	}
}
