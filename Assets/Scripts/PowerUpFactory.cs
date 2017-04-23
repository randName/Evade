using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUpFactory  //our factory
{
    public void getPowerUp(string input,GameObject pup) //Adds color and powerUp class to the empty powerUp prefab.
    {
        Color color = Color.white;  
        //a powerUp
        if (input.Equals("SpeedBoost"))
        {
            color = Color.yellow; //the color of the powerUp that is spawned
            pup.AddComponent(typeof(SpeedBoost));   //adding actual effects to the powerUp generated
            pup.GetComponent<MeshFilter>().mesh = (Mesh)Resources.Load("SpeedPowerUp",typeof(Mesh));    //adding the mesh to the prefab
        }
        if (input.Equals("IncreaseSize"))
        {
            color = Color.red;
            pup.AddComponent(typeof(IncreaseSize));

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




