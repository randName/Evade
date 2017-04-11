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

    // Use this for initialization
    void Start () {
        pc = GetComponent<PlayerController>();
        isAlive = true;
        isStunned = false;
        canStun = false;
        speed = 2;
        size = 1;
        mass = 1;
	}

    void Update()
    {
        pc.setSpeed(speed);
        pc.setIsAlive(isAlive);
        pc.setCanStun(canStun);
        pc.setSize(size);
        pc.setMass(mass);
        
    }

    private void FixedUpdate()
    {
        //might have to change the imers to here is the waitforSeconds doesn't work

    }

    bool getisAlive(){ return isAlive; }
    bool getisStunned() { return isStunned; }
    
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "boundary") //need to change this when we update to falling of a platform
    //    {
    //        isAlive = false;
    //    }
    //    if (other.tag == "speedboost")
    //    {
    //        //need to get an instance of a speedboost here?
    //        // use name of game object?
    //        other.GetComponent<powerUp>().accept(this);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "obstacle")
    //    {
    //        isStunned = true;
    //    }
    //}

    //<<<<<All methods that the power Ups call are implemented below>>>>
    //need to implement coroutines here
    IEnumerator speedCor()
    {
        Debug.Log("Changing the speed inside the playerstate");
        speed = speed + 2;
        Debug.Log(speed);
        yield return new WaitForSecondsRealtime(5);
        Debug.Log(speed);
        speed = speed - 2;
        Debug.Log(speed);
    }

    public void increaseSpeedTemp()
    {
        StartCoroutine(speedCor());
    }

    IEnumerator sizeCor()
    {
        for (int i = 0; i<4; i++)
        {
            size = size + 0.25;
            mass = mass + 1.25;
            yield return new WaitForSecondsRealtime(1);
        }
        yield return new WaitForSecondsRealtime(2);

        for (int i = 0; i < 2; i++)
        {
            size = size - 0.5;
            mass = mass - 2.5;
            yield return new WaitForSecondsRealtime(1);
        }
    }

    public void increaseSizeTemp()
    {
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
        StartCoroutine(StunNextPlayerCor());
    }
    IEnumerator getStunnedPlayerCor()
    {
        isStunned = true;
        speed = 0;
        yield return new WaitForSecondsRealtime(2);
        isStunned = false;
        speed = 2; //if player gets pushed on power up, this stun is negated(bug)
    }
    public void getStunnedPlayerTemp()
    {
        StartCoroutine(getStunnedPlayerCor());
    }

    IEnumerator massCor()
    {
        mass = mass * 1000000;
        speed = 0.5;
        yield return new WaitForSecondsRealtime(5);
        mass = mass / 1000000;
        speed = 2;

    }

    public void increaseMassTemp()
    {
        StartCoroutine(massCor());
    }
}
