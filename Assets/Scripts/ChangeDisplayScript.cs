using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDisplayScript : MonoBehaviour {
    public Sprite stun;
    public Sprite speed;
    public Sprite size;
    public Sprite mass;
    public Sprite plain;
    public Button b1;
	void Awake () {

    }
	
	// when clicked the button would change sprite
	public void On_Click_Button () {

        b1.image.sprite = speed;
	}
}
