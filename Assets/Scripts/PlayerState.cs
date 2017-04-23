using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player state is a local class that checks and maintains the state of the player. 
//other scripts will check and update this class to determine whether the player is alive/not.
//Also, effects of the powerUps are implmented in this class. They were previously in a separate class
//but was moved here for for easy access. We use coroutines to implement the effects of the powerUps.
public class PlayerState : MonoBehaviour {
    private bool isAlive; //check if the player is alive.
    private bool isStunned; //check if the player is Stunned (cannot move forward)
    private bool canStun; //check if the player can stun the next player he collides into. (called when he uses the stunner powerup)
    private double size; //size of the player object
    private double speed; //speed of the player object
    private double mass; //mass of the player object
    private PlayerController pc;
    private IEnumerator coroutine;
    // Use this for initialization
    void Start () { //the default settings of the player object
        pc = GetComponent<PlayerController>();
        isAlive = true;
        isStunned = false;
        canStun = false;
        speed = 3;
        size = 40;
        mass = 1;

	}

    void Update()   //setting the varaibles inside the player controller.
    {
        pc.setSpeed(speed);
        //pc.setIsAlive(isAlive);
        pc.setCanStun(canStun);
        pc.setSize(size);
        pc.setMass(mass);
        
    }

    bool getisAlive(){ return isAlive; }
    bool getisStunned() { return isStunned; }
    
    //<<<<<All methods that the power Ups call are implemented below>>>>
    IEnumerator speedCor()
    {
        
        speed = speed + 3;
        yield return new WaitForSecondsRealtime(5);
        speed = speed - 3;
    }

    public void increaseSpeedTemp()
    {

        StartCoroutine(speedCor());
    }

    IEnumerator sizeCor()
    {
        for (int i = 0; i<4; i++)
        {
            size = size + 5.0;
            mass = mass + 0.6;
            yield return new WaitForSecondsRealtime(1);
        }
        yield return new WaitForSecondsRealtime(2);

        for (int i = 0; i < 2; i++)
        {
            size = size - 10.0;
            mass = mass - 1.2;
            yield return new WaitForSecondsRealtime(1);
        }
    }

    public void increaseSizeTemp()
    {
        stopAnyCoroutine();
        StartCoroutine(sizeCor());
    }
    
    IEnumerator StunNextPlayerCor()
    {
        canStun = true;
        yield return new WaitForSecondsRealtime(5);
        canStun = false;
    }

    public void stunNextPlayerTemp()
    {
        stopAnyCoroutine();
        StartCoroutine(StunNextPlayerCor());
    }
    IEnumerator getStunnedPlayerCor()
    {
        isStunned = true;
        speed = 0;
        yield return new WaitForSecondsRealtime(2);
        isStunned = false;
        speed = 3; //if player gets pushed on power up, this stun is negated(bug)
    }
    public void getStunnedPlayerTemp()
    {
        stopAnyCoroutine();
        StartCoroutine(getStunnedPlayerCor());
    }

    IEnumerator massCor()
    {
        mass = mass * 3;
        speed = 1;
        yield return new WaitForSecondsRealtime(5);
        mass = mass / 3;
        speed = 3;

    }

    public void increaseMassTemp()
    {
        stopAnyCoroutine();
        StartCoroutine(massCor());
    }

    public void stopAnyCoroutine() //This function stops overlapping power ups. It is called in all coroutines that buff the player.
    {
        StopAllCoroutines();
        canStun = false;
        speed = 3;
        size = 40;
        mass = 1;
    }


}
