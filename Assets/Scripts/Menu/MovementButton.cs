using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour {

    public PlayerController pc;
	
    public void setMove() //when button is pressed, indicate to player controller that this player wishes to move
    {
        try
        {
            pc.setIsMove(true);
        }
        catch(System.Exception e)
        {
            gameObject.SetActive(false);
        }
        
    }

    public void setNoMove() //created to check if move can be changed
    {
        try
        {
            pc.setIsMove(false);
        }
        catch (System.Exception e)
        {
            gameObject.SetActive(false);
        }
    }

}
