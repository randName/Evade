using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    private bool isAlive; //check if the player is alive.
    private bool isStunned;
    private bool canStun;
    private int size;
    private int speed;
    private int mass;

    // Use this for initialization
    void Start () {
        isAlive = true;
        isStunned = false;
        canStun = false;
        
	}

    private void FixedUpdate()
    {
        //might have to change the imers to here is the waitforSeconds doesn't work

    }

    bool getisAlive(){ return isAlive; }
    bool getisStunned() { return isStunned; }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "boundary") //need to change this when we update to falling of a platform
        {
            isAlive = false;
        }
        if (other.tag == "speedboost")
        {
            //need to get an instance of a speedboost here?
            // use name of game object?
            other.GetComponent<powerUp>().accept(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "obstacle")
        {
            isStunned = true;
        }
    }

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
        size = size + 2;
        yield return new WaitForSecondsRealtime(5);
        size = size - 2;
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

    IEnumerator massCor()
    {
        mass = mass + 2;
        yield return new WaitForSecondsRealtime(5);
        mass = mass + 2;

    }

    public void increaseMassTemp()
    {
        StartCoroutine(massCor());
    }
}
