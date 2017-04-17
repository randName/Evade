using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    private bool isAlive; //check if the player is alive.
    private bool isStunned;
    private bool canStun;
    private double size;
    private double speed;
    private double mass;
    private PlayerController pc;
    private IEnumerator coroutine;
    // Use this for initialization
    void Start () {
        pc = GetComponent<PlayerController>();
        isAlive = true;
        isStunned = false;
        canStun = false;
        speed = 3;
        size = 40;
        mass = 1;

	}

    void Update()
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
    //need to implement coroutines here
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
            mass = mass + 1.25;
            yield return new WaitForSecondsRealtime(1);
        }
        yield return new WaitForSecondsRealtime(2);

        for (int i = 0; i < 2; i++)
        {
            size = size - 10.0;
            mass = mass - 2.5;
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
        double oldSpeed = speed;
        speed = 0;
        yield return new WaitForSecondsRealtime(2);
        isStunned = false;
        speed = oldSpeed; //if player gets pushed on power up, this stun is negated(bug)
    }
    public void getStunnedPlayerTemp()
    {
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
