using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour {

    public PlayerController pc;
	
    public void setMove()
    {
        pc.setIsMove(true);
        
    }

    public void setNoMove()
    {
        pc.setIsMove(false);
    }

}
