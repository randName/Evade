using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUpFactory : MonoBehaviour 
{
    public powerUp getPowerUp(string input)
    {
        if (input.Equals("SpeedBoost"))
        {
            return new SpeedBoost();
        }
        if (input.Equals("IncreaseSize"))
        {
            return new IncreaseSize();
        }
        if (input.Equals("StunNextPlayer"))
        {
            return new StunNextPlayer();
        }
        if (input.Equals("IncreaseMass"))
        {
            return new IncreaseMass();
        }
        else
        {
            return null;
        }
    }
}

/*
public abstract class powerUp:MonoBehaviour
{
    abstract protected void consume(PlayerController playerController);
    public void usePowerUp(PlayerController playerController)
    {
        this.consume(playerController);
        Destroy(this.gameObject);    
    }
}
*/



