using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hi : MonoBehaviour
{

    public int speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerState ps = (PlayerState)GameObject.Find("myCube").GetComponent("PlayerState");
        PowerUpFactory pfact = (PowerUpFactory)GameObject.Find("myCube").GetComponent("PowerUpFactory");
        powerUp sb = pfact.getPowerUp("SpeedBoost");
        sb.accept(ps);
        Destroy(other.gameObject);
        //StartCoroutine(upSpeed());
    }

    public void increaseSpeed()
    {
        int changeInSpeed = 2;
        Debug.Log(speed);
        speed = speed + changeInSpeed;
        new WaitForSecondsRealtime(5);
        Debug.Log(speed);
        speed = speed - changeInSpeed;
        Debug.Log(speed);
        
    }

    IEnumerator upSpeed()
    {
        Debug.Log("UpSpeed Initiated");
        Debug.Log(speed);
        speed = speed + 2;
        Debug.Log(speed);
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("Waiting for 5 seconds");
        Debug.Log(speed);
        speed = speed - 2;
        Debug.Log("Speed changed back");
        Debug.Log(speed);
    }
}
