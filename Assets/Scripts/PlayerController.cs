using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//PlayerController controls the local movement of the playerObject
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

    [SyncVar] //updates the clients on whether a player is dead or not
    private bool isAlive;

    [SyncVar] //updates all clients on whether this player wishes to move forward
    private bool isMove;

    [SyncVar] //updates all clients on whether this player desires to use a power up
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
            
            //this portion tells the buttons that this player controller script is the local, correct version to control
            mvScript.pc = this;
            puScript.pc = this;
            cds.pc = this;
            //set initial power up image to nothing
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
        //if the player is not alive (out of the platform) explode the player object
        if (!isAlive)
        {

            expl.transform.position = transform.position;
            Instantiate(expl);
            Destroy(gameObject);
        }
        //using the powerUp
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
    //adding the number of dead players to the game manager
    void OnDestroy()
    {
        gm.addDeadCounter();
    }
    //checking the object that the player collides into
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

    void FixedUpdate() //Unity's physics update
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
    //force is directed opposite to the point that the players collide
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="GamePlayer")
        {
            ContactPoint point = collision.contacts[0];
            Vector3 dir = point.point - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * 5* (float)collision.gameObject.GetComponent<PlayerController>().getSpeed()/2 * (float)collision.gameObject.GetComponent<PlayerController>().getMass()/2, ForceMode.Impulse);
            
            
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

    private void holdPowerUp(powerUp pu) //clears the any held power ups and update the held power up with one picked up.
    {
        clearPowerUps();
        heldPowerUp = Instantiate(pu,transform, false);
        
    }

    private void clearPowerUps() //removes all power up objects in the player object.
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
    
    public void setSpeed(double newSpeed) //set the player speed
    {
        speed = newSpeed;
    }

    public double getSpeed() //get the player speed
    {
        return speed;
    }



    [Command] //tells server to set player isAlive status.
    public void CmdSetIsAlive(bool alive)
    {
        isAlive = alive;
    }

    [ClientRpc] //server tells clients to set player isAlive status
    public void RpcSetIsAlive(bool alive)
    {
        isAlive = alive;
    }
    //Set isAlive status in general
    public void setIsAlive(bool alive)
    {
        CmdSetIsAlive(alive);
    }

    public bool getIsAlive() //get this player's isAlive status
    {
        return isAlive;
    }

    public void setCanStun(bool stun) //this function enables a player to stun another player
    {
        canStun = stun;
    }

    public bool getCanStun() //check if a player can stun another player
    {
        return canStun;
    }

    public void setSize(double size) //change the size of the player
    {
        sizeScale = size;
    }

    public double getSize() //check the size of the player
    {
        return sizeScale;
    }
    public void setMass(double mass) //change the mass of the player
    {
        massScale = mass;
    }
    public double getMass() //check the mass of the player
    {
        return massScale;
    }

}
