using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//DO HOST/JOIN UI ASAP.
//RECODE POWER UP ROTATIONS
//ADD NEW POWER UP MODELS
//FIX END GAME STATE

// TODO: write monkeyrunner script to test for random inputs.
// TODO: add a "no move" state for players before the game starts
// TODO: implement the game end state to complete flow.
public class PlayerController : NetworkBehaviour //PlayerState sets the local variables while PlayerController updates the game model on the server.
{
    Rigidbody rb;
    PlayerState ps;
    GameManager gm;

    private MovementButton mvScript;
    private PowerUpButton puScript;
    private ChangeDisplayScript cds;
    private Button movementBtn;
    private Button powerUpBtn;
    private powerUp heldPowerUp;
    public GameObject pickUpSound;
    public GameObject expl;
    public GameObject colliSound;
    public GameObject dizzy;
    public GameObject usePuSound;
    float lockPos = 0;
    //ClientRPC is called from server to update clients.
    //Cmd is called from client to update server

    private double speed = 2;

    private double sizeScale = 40;

    private double massScale = 1;

    private bool canStun;

    [SyncVar]
    private bool isAlive;

    [SyncVar]
    private bool isMove;

    [SyncVar]
    private bool usePowerUp;

    

    void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<PlayerState>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        gm.addPlayer(gameObject);
        setUsePowerUp(false);

        //assign the buttons at runtime.
        if (isLocalPlayer)
        {
            GameObject joinMenu = GameObject.Find("User Interface");
            movementBtn = joinMenu.transform.Find("JoinMenu").Find("Playing").Find("BtnMovement").GetComponent<Button>();
            powerUpBtn = joinMenu.transform.Find("JoinMenu").Find("Playing").Find("BtnPowerup").GetComponent<Button>();

            mvScript = movementBtn.GetComponent<MovementButton>();
            puScript = powerUpBtn.GetComponent<PowerUpButton>();
            cds = powerUpBtn.GetComponent<ChangeDisplayScript>();

            mvScript.pc = this;
            puScript.pc = this;
            cds.pc = this;

            cds.updateSprite(null);

        }

    }
    public void setColours() //called when game starts to set the colour of each player individually for each client.
    {
        int quadrant = ((this.transform.position.x > 0) ? 2 : 0) + ((this.transform.position.z > -10) ? 1 : 0);
        Color startCol;
        switch (quadrant)
        {
            case 0:
                startCol = Color.red;
                break;
            case 1:
                startCol = Color.green;
                break;
            case 2:
                startCol = Color.blue;
                break;
            case 3:
                startCol = Color.yellow;
                break;
            default:
                startCol = Color.white;
                break;
        }
        GetComponent<MeshRenderer>().material.color = startCol;
    }


    void Update()
    {
        //if (isLocalPlayer) //this test shows that the non-local boolean change is not received.
        //{
        //    Debug.Log("Local "+ usePowerUp);
        //}

        //if (!isLocalPlayer)
        //{
        //    Debug.Log("Non-local " + usePowerUp);
        //}
        if (!isAlive)
        {

            expl.transform.position = transform.position;
            Instantiate(expl);
            Destroy(gameObject);
        }

        if (usePowerUp)
        {
            if (heldPowerUp!=null)
            {
                try
                {
                    cds.updateSprite(null);
                }
                catch(System.NullReferenceException e)
                {
                    
                }
                heldPowerUp.accept(ps);
                usePuSound.transform.position = transform.position;
                Instantiate(usePuSound);
                clearPowerUps();
                heldPowerUp = null;
                
            }
        }

        
        if (!isLocalPlayer)
        {
            return;
        }
        
    }

    void OnDestroy()
    {
        gm.addDeadCounter();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PowerUp"))
        {
            holdPowerUp(other.GetComponent<powerUp>()); //hold a copy of the power up in hand.
            if (isLocalPlayer)
            {
                cds.updateSprite(heldPowerUp);
                pickUpSound.transform.position = transform.position;
                Instantiate(pickUpSound);
            }
            Destroy(other.gameObject); //Destroys the local instance of the power up. This should occur on all the other clients locally as well.
        }
        else if(other.tag.Equals("Out of Bounds"))// update later
        {
            setIsAlive(false);
        }
    }

    void FixedUpdate()
    {
        
        transform.localScale = (float)sizeScale * Vector3.one; //update size
        rb.mass = (float)massScale; //update mass


        if (isMove && speed!=0) //check for speed = 0 since stunned or disabled can result in such a state.
        {
           
            transform.position = Vector3.Lerp((transform.position + transform.forward * Time.deltaTime*(float)speed), transform.position, Time.deltaTime * 3.0f);
        }
        else
        {
            transform.Rotate(lockPos, 135 * Time.deltaTime, lockPos);
            transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);
        }
        //this statement goes after all the transforms
        if (!isLocalPlayer)
        {
            return;
        }

    }


    //provide impulse that makes players seem like they ricochet off each other.
    //sideways collisions are slightly buggy.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="GamePlayer")
        {
            ContactPoint point = collision.contacts[0];
            Vector3 dir = point.point - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * 5* (float)collision.gameObject.GetComponent<PlayerController>().getSpeed()/2 * (float)collision.gameObject.GetComponent<PlayerController>().getMass()/2, ForceMode.Impulse);
            //Vector3 impulse = Vector3.Reflect(transform.forward, point.normal);
            //rb.AddForce(impulse * 5, ForceMode.Impulse);
            
            colliSound.transform.position = transform.position;
            Instantiate(colliSound);
            
            PlayerController other = collision.gameObject.GetComponent<PlayerController>();
            if (other.getCanStun())
            {
                dizzy.transform.position = transform.position;
                Instantiate(dizzy);
                ps.getStunnedPlayerTemp();
            }
            
        }

    }

    private void holdPowerUp(powerUp pu)
    {
        clearPowerUps();
        heldPowerUp = Instantiate(pu,transform, false);
        
    }

    private void clearPowerUps()
    {
        foreach(powerUp power in gameObject.GetComponentsInChildren<powerUp>())
        {
            Destroy(power.gameObject);
        }
    }
    //Below are the getter and setter functions for the player controller variables.
    [Command] 
    public void CmdSetUsePowerUp(bool val) //Client updates server of its power up usage.
    {
        usePowerUp = val;
    }

    [ClientRpc]
    public void RpcSetUsePowerUp(bool val) //Server updates all clients of all players' power up usage
    {
        usePowerUp = val;
    }

    public void setUsePowerUp(bool val) //Generic set power up usage function
    {
        if (isLocalPlayer)
        {
            CmdSetUsePowerUp(val);
        }

    }
    [Command]
    public void CmdSetIsMove(bool val) //Client updates server of its desire to move
    {
        isMove = val;
    }
    [ClientRpc]
    public void RpcSetIsMove(bool val) //Server updates all clients of each player's desire to move
    {
        isMove = val;
    }


    public void setIsMove(bool val) //Generic set desire to move function
    {
        if (isLocalPlayer)
        {
            CmdSetIsMove(val);
        }

    }
    
    public void setSpeed(double newSpeed)
    {
        speed = newSpeed;
    }

    public double getSpeed()
    {
        return speed;
    }



    [Command]
    public void CmdSetIsAlive(bool alive)
    {
        isAlive = alive;
    }

    [ClientRpc]
    public void RpcSetIsAlive(bool alive)
    {
        isAlive = alive;
    }

    public void setIsAlive(bool alive)
    {
        CmdSetIsAlive(alive);
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
