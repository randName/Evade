using UnityEngine;
using UnityEngine.Networking;

// TODO: write monkeyrunner script to test for random inputs.

public class PlayerController : NetworkBehaviour //PlayerState sets the local variables while PlayerController updates the game model on the server.
{
    Rigidbody rb;
    PlayerState ps;
    GameManager gm;
    float lockPos = 0;

    [SyncVar]
    private double speed = 2;

    [SyncVar]
    private double sizeScale = 1;

    [SyncVar]
    private double massScale = 1;

    [SyncVar]
    private bool isAlive;

    [SyncVar]
    private bool isMove;

    [SyncVar]
    private bool canStun;

    

    void Start()
    {
        isAlive = true;
        rb = this.GetComponent<Rigidbody>();
        ps = GetComponent<PlayerState>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.addPlayer(gameObject);
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

        transform.localScale = new Vector3((float)sizeScale, (float)sizeScale, (float)sizeScale); //update size
        rb.mass = (float)massScale; //update mass

        isMove = Input.anyKey;
    }

    void OnTriggerEnter(Collider other)
    {
        // player loses, host is able to delete itself but clients cannot.
        if (other.tag.Equals("PowerUp"))
        {
            powerUp p = other.GetComponent<powerUp>();
            p.accept(GetComponent<PlayerState>());
            Destroy(other.gameObject); //Destroys the local instance of the power up. This should occur on all the other clients locally as well.
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
        /*
         *We need to find a new way to check for all these booleans, cannot be that 100 power ups 
         *then must code for 100 conditions.
         * 
         */ 
        
        if (isMove && speed!=0) //check for speed = 0 since stunned or disabled can result in such a state.
        {
            transform.position = Vector3.Lerp((transform.position + transform.forward * Time.deltaTime*(float)speed), transform.position, Time.deltaTime * 3.0f);
        }
        else
        {
            transform.Rotate(lockPos, 90 * Time.deltaTime, lockPos);
            transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);
        }

    }

    //check collision
    //provide impulse that makes players seem like they ricochet off each other.
    //Currently has a bug. Other players might not actually be removed from the scene. Check.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="GamePlayer")
        {
            ContactPoint point = collision.contacts[0];
            Vector3 normal = point.normal;
            Vector3 impulse = Vector3.Reflect(transform.forward, normal);
            rb.AddForce(impulse * 5, ForceMode.Impulse);
 
            PlayerController other = collision.gameObject.GetComponent<PlayerController>();
            if (other.getCanStun())
            {
                ps.getStunnedPlayerTemp();
            }
            
        }
        //else if (collision.gameObject.tag == "PowerUp")
        //{
        //    speed += 2;
        //    Destroy(collision.gameObject);
        //}

        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    isAlive = false;
        //    Destroy(gameObject);
        //}
    }

    public void setSpeed(double newSpeed)
    {
        speed = newSpeed;
    }

    public double getSpeed()
    {
        return speed;
    }

    public void setIsAlive(bool alive)
    {
        isAlive = alive;
    }

    public bool getIsAlive()
    {
        return isAlive;
    }

    public void setCanStun(bool stun)
    {
        canStun = stun;
    }

    public bool getCanStun()
    {
        return canStun;
    }

    public void setSize(double size)
    {
        sizeScale = size;
    }

    public double getSize()
    {
        return sizeScale;
    }
    public void setMass(double mass)
    {
        massScale = mass;
    }
    public double getMass()
    {
        return massScale;
    }

}
