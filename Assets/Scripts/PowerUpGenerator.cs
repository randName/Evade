
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    public GameObject prefab;
    private float previousRecordedTime = 0;
    private bool spawning;
    private PowerUpFactory puf; //future power up factory. Instantiate and generate power ups here.
    private GameObject nextPowerUp;
    void Start()
    {
        //set spawning to true whenever the game starts (not necessarily on client run.)
        spawning = true;
    }

    public void trySpawning()
    {
        if (!Physics.CheckSphere(transform.position, (float)0.1)){ 
            
            if (Time.time - previousRecordedTime > 5 && spawning)
            {
                spawnPowerUp();
                previousRecordedTime = Time.time;
            }
        }
        
        
        //if game ends, set spawning to false.

    }
    
    public void Update()
    {
        trySpawning();
    }
    
    public void spawnPowerUp()
    {
        /* TO DO: Add in various kinds of prefabs to be spawned (if power ups have different models)
         * 
         */
        Vector3 position = transform.position;
        GameObject powerUp = (GameObject)Instantiate(prefab); //create power up in the world
        //NetworkServer.Spawn(powerUp); //spawn it on the network server.
        powerUp.transform.position = position; //move power up spawned to position.
        
    }
}
