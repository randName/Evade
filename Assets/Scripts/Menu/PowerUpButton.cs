using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : MonoBehaviour {
    public PlayerController pc;

    public void setUsingPowerUp()
    {

        pc.setUsePowerUp(true);
    }
    
    public void setNotUsingPowerUp()
    {
        pc.setUsePowerUp(false);
    }
}
