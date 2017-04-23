using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

//ADD A BARRIER SO THAT ALL CLIENTS HAVE TO BE CONNECTED BEFORE GAME CAN "START" THIS
public class PowerUpGeneratorSpawner : NetworkBehaviour {
    public GameObject powerUpGenerator;
    private int numberOfGenerators = 5; //set this to determine the maximum number of generators in the level.
    private Vector3[] locations;
    
    int randomSeed;
    [SyncVar] //server syncs this variable with clients to ensure everyone starts at the same time.
    bool gameStart;

    void Start()
    {

        
    }
    void Update()
    {
        if (gameStart)
        {
            if(isServer) //set the random seed based off time on official game start
            {
                System.TimeSpan timeDifference = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                RpcSetRandomSeed(System.Convert.ToInt32(timeDifference.TotalSeconds));
                //RpcSetRandomSeed(5); 
                
            }
        }
    }

    void LateUpdate() //Unity updates this at the end of each update
    {
        if (gameStart) //bug occurs when gameStart is synced several times.
        {
            executeAll();
            gameStart = false;
        }
    }

    void executeAll() //Perform all spawning of generators
    {
        
        Debug.Log("Spawning with seed = " + getRandomSeed());
        locations = generateGeneratorLocations(getRandomSeed());

        for(int i=0; i<numberOfGenerators; i++)
        {
            SpawnGenerator(i);
        }//spawn n number of powerupgenerators in the map space.
    }

    [ClientRpc] //server tells clients to set the random seed to the common seed value
    public void RpcSetRandomSeed(int value) //Use time to simulate a pseudo random seed.
    {
        randomSeed = value;
        
    }

    public int getRandomSeed() //get the random seed number
    {
        return randomSeed;
    }

    public void setGameStart(bool isGameStarting) //let another script change the gameStart variable.
    {
        gameStart = isGameStarting;
    }

    Vector3[] generateGeneratorLocations(int seed) //use random to generate the locations of the generators
    {

        Random.InitState(seed);
        Vector3[] loc = new Vector3[numberOfGenerators];
        for (int i=0; i < numberOfGenerators; i++)
        {
            loc[i] = new Vector3(Random.Range(-10, 10), 1, -Random.Range(3, 17)); //get a random position to spawn
        }
        return loc;
    }
    //all random locations of the powerUps are now synchronised accross client and server
    
    void SpawnGenerator(int number) //checks if there are any objects near the to-be-spawned generator locations, if none, spawn the generators
    {
        Vector3 position = locations[number];

        if (!Physics.CheckSphere(position, (float)0.3))
        {
            GameObject powerUpSpawner = GameObject.Instantiate(powerUpGenerator); //create a powerUpSpawner
            powerUpSpawner.transform.position = position; //move powerUpSpawner to intended position
            
        }
    }


}
