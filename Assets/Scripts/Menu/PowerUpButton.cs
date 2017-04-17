using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : MonoBehaviour {
    public PlayerController pc;

    public void setUsingPowerUp()
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
    
    public void setNotUsingPowerUp()
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
