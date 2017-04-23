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
    public PlayerController pc;
    
	void Awake () {

    }
	
	//sprite is updated whenever playercontroller picks a power up.
    public void updateSprite(powerUp powerUpType)
    {
        if (powerUpType == null)
        {
            b1.image.sprite = plain;
        }
        else if (powerUpType.GetComponent<SpeedBoost>() != null)
        {
            b1.image.sprite = speed;
        }
        else if (powerUpType.GetComponent<IncreaseMass>() != null)
        {
            b1.image.sprite = mass;
        }
        else if (powerUpType.GetComponent<IncreaseSize>() != null)
        {
            b1.image.sprite = size;
        }
        else if (powerUpType.GetComponent<StunNextPlayer>() != null)
        {
            b1.image.sprite = stun;
        }
    }
}
