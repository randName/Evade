using UnityEngine;
using UnityEngine.Networking;

// TODO: write monkeyrunner script to test for random inputs.

// TODO: should make movement local... the lag is terrible.
public class PlayerController : NetworkBehaviour
{
    Rigidbody rb;
    float lockPos = 0;
    bool isAlive;
    public int speed = 2;
    void Start()
    {
        isAlive = true;
        rb = this.GetComponent<Rigidbody>();
        //setHandler();
    }

    void Update()
    {
        //check if player status has been updated
        if (!isAlive)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    /*
    void setHandler()
    {
        MovementButton mb = (MovementButton)GameObject.Find("TempHandler").GetComponent("MovementButton");
        mb.player = gameObject;
    }
    */

    void OnTriggerEnter()
    {
        // player loses, host is able to delete itself but clients cannot.
        // TODO: Refactor code to improve the network code.
        if (other.tag.Equals("PowerUp"))
        {
            powerUp p = (powerUp)other.GetComponent<powerUp>();
            p.accept(this.GetComponent<PlayerState>());
            Destroy(other.gameObject);
        }
        else // update later
        {
            isAlive = false;
        }
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        if (Input.anyKey)
        {
            transform.position = Vector3.Lerp((transform.position + transform.forward * Time.deltaTime*speed), transform.position, Time.deltaTime * 3.0f);
        }
        else
        {

            transform.Rotate(lockPos, 90 * Time.deltaTime, lockPos);
            transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);
        }

    }

    //check collision
    //provide impulse that makes players seem like they ricochet off each other.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="GamePlayer")
        {
            ContactPoint point = collision.contacts[0];
            Vector3 normal = point.normal;
            Vector3 impulse = Vector3.Reflect(transform.forward, normal);
            //Vector3.Lerp(impulse*5, transform.position,(float)0.5);
            rb.AddForce(impulse * 5, ForceMode.Impulse);
        }

        if(collision.gameObject.tag == "PowerUp")
        {
            speed += 2;
            Destroy(collision.gameObject);
        }
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    isAlive = false;
        //    Destroy(gameObject);
        //}
    }


}
