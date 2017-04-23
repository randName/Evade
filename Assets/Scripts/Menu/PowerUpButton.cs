using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : MonoBehaviour {
    public PlayerController pc;

    public void setUsingPowerUp() //when button is pressed, indicate to player controller that this player wishes to use power up
    {
        try
        {
            pc.setUsePowerUp(true);
        }
        catch (System.Exception e)
        {
            gameObject.SetActive(false);
        }
        
    }
    
    public void setNotUsingPowerUp() //created to test if power up status can be changed
    {
        try
        {
            pc.setUsePowerUp(false);
        }
        catch (System.Exception e)
        {
            gameObject.SetActive(false);
        }
    }
}
