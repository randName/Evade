using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour {

    public PlayerController pc;
	
    public void setMove()
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

    public void setNoMove()
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
