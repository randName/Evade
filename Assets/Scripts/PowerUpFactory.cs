using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUpFactory 
{
    public void getPowerUp(string input,GameObject pup) //Adds color and powerUp class to the empty powerUp prefab.
    {
        Color color = Color.white;
        if (input.Equals("SpeedBoost"))
        {
            color = Color.yellow;
            pup.AddComponent(typeof(SpeedBoost));
            pup.GetComponent<MeshFilter>().mesh = (Mesh)Resources.Load("SpeedPowerUp",typeof(Mesh));
        }
        if (input.Equals("IncreaseSize"))
        {
            color = Color.red;
            pup.AddComponent(typeof(IncreaseSize));

            //pup.AddComponent(Resources.Load("Sprites/IncreaseSizePowerUp"));
            pup.GetComponent<MeshFilter>().mesh = (Mesh)Resources.Load("IncreaseSizePowerUp",typeof(Mesh));
        }
        if (input.Equals("StunNextPlayer"))
        {
            color = Color.blue;
            pup.AddComponent(typeof(StunNextPlayer));
            pup.GetComponent<MeshFilter>().mesh = (Mesh)Resources.Load("StunNextPlayer",typeof(Mesh));
        }
        if (input.Equals("IncreaseMass"))
        {
            color = Color.green;
            pup.AddComponent(typeof(IncreaseMass));
            pup.GetComponent<MeshFilter>().mesh = (Mesh)Resources.Load("IncreaseMass",typeof(Mesh));
        }

        pup.GetComponent<MeshRenderer>().material.color = color;
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



