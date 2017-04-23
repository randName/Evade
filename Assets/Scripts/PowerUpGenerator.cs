
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpGenerator : NetworkBehaviour
{
    public GameObject prefab; 
    private float previousRecordedTime = 0;
    private bool spawning;
    private PowerUpFactory puf = new PowerUpFactory(); //future power up factory. Instantiate and generate power ups here.
    private GameObject nextPowerUp;
    private PowerUpGeneratorSpawner pugs;

    int randomSeed;
    private string[] possiblePowerUps = new string[] { "SpeedBoost", "IncreaseSize", "StunNextPlayer", "IncreaseMass" }; //create a new powerup, put the powerup script into a normal object, convert it.


    void Start()
    {
        //set spawning to true whenever the game starts (not necessarily on client run.)
        spawning = true;
        pugs = GameObject.Find("PowerUpGeneratorSpawner").GetComponent<PowerUpGeneratorSpawner>();
        randomSeed = pugs.getRandomSeed();
        Random.InitState(randomSeed);
    }

    public void trySpawning()
    {
        if (!Physics.CheckSphere(transform.position, (float)0.3)){ 
            
            if (Time.time - previousRecordedTime > 5 && spawning)
            {
                spawnPowerUp();
                previousRecordedTime = Time.time;
            }
        }

        else
        {
            previousRecordedTime = Time.time;
        }
        
        
        //if game ends, set spawning to false.

    }
    
    public void Update()
    {
        trySpawning();
    }

    
    public void spawnPowerUp()
    {
        
        Vector3 position = transform.position + new Vector3(0,(float)0.2,0);
        GameObject pup = (GameObject)Instantiate(prefab); //create power up in the world
        //NetworkServer.Spawn(powerUp); //spawn it on the network server.
        pup.transform.position = position; //move power up spawned to position.
        int r = Random.Range(0, possiblePowerUps.Length);
        puf.getPowerUp(possiblePowerUps[r],pup);
        

    }


}
