using UnityEngine;
using UnityEngine.Networking;

// TODO: write monkeyrunner script to test for random inputs.

public class PlayerController : NetworkBehaviour
{
    Rigidbody rb;
    float lockPos = 0;

    [SyncVar]
    public int speed = 2;

    [SyncVar]
    bool isAlive;

    [SyncVar]
    bool isMove;

    void Start()
    {
        isAlive = true;
        rb = this.GetComponent<Rigidbody>();
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    void Update()
    {
        if (!isAlive)
        {
            Destroy(gameObject);
        }

        if (!isLocalPlayer)
        {
            return;
        }

        isMove = Input.anyKey;
    }

    void OnTriggerEnter(Collider other)
    {
        // player loses, host is able to delete itself but clients cannot.
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

        if (isMove)
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
        else if (collision.gameObject.tag == "PowerUp")
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
