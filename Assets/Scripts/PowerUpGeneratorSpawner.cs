using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

//ADD A BARRIER SO THAT ALL CLIENTS HAVE TO BE CONNECTED BEFORE GAME CAN "START" THIS
public class PowerUpGeneratorSpawner : NetworkBehaviour {
    public GameObject powerUpGenerator;
    private int numberOfGenerators = 5; //set this to determine the maximum number of generators in the level.
    private Vector3[] locations;

    [SyncVar]
    int randomSeed;
    [SyncVar]
    bool gameStart;


    void Update()
    {
        if (gameStart)
        {
            executeAll();
            gameStart = false;
        }
    }

    void executeAll()
    {
        setRandomSeed();
        locations = generateGeneratorLocations(randomSeed);

        for(int i=0; i<numberOfGenerators; i++)
        {
            SpawnGenerator(i);
        }//spawn n number of powerupgenerators in the map space.
    }

    public void setRandomSeed() //get a random seed value on start.
    {
        System.DateTime now = new System.DateTime();
        randomSeed = (int)now.Ticks;
    }

    public void setGameStart(bool isGameStarting) //let another script toggle the gameStart variable.
    {
        gameStart = isGameStarting;
    }

    Vector3[] generateGeneratorLocations(int seed)
    {

        Random.InitState(seed);
        Vector3[] loc = new Vector3[numberOfGenerators];
        for (int i=0; i < numberOfGenerators; i++)
        {
            loc[i] = new Vector3(Random.Range(-7, 9), 1, -Random.Range(10, 15)); //get a random position to spawn
        }
        return loc;
    }
    //it spawns on the client's version now, however the random locations are not synchronized.
    void SpawnGenerator(int number)
    {
        Vector3 position = locations[number];
        //Vector3 position = new Vector3(Random.Range(-7, 9), 1, -Random.Range(10,15));
        if (!Physics.CheckSphere(position, (float)0.1))
        {
            GameObject powerUpSpawner = GameObject.Instantiate(powerUpGenerator); //create a powerUpSpawner
            powerUpSpawner.transform.position = position; //move powerUpSpawner to intended position
            NetworkServer.Spawn(powerUpSpawner); //try to spawn it on network.
            
        }
    }
}
