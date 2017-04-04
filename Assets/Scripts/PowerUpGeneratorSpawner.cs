using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpGeneratorSpawner : NetworkBehaviour {
    public GameObject powerUpGenerator;
	// Use this for initialization
	void Start () {

        for(int i = 0; i < 5; i++)
        {
            CmdSpawnGenerator();
        }
		//spawn n number of powerupgenerators in the map space.
        
	}
    [Command]
    void CmdSpawnGenerator()
    {
        
        Vector3 position = new Vector3(Random.Range(-7, 9), 1, -Random.Range(10,15)); //get a random position to spawn
        if (!Physics.CheckSphere(position, (float)0.1))
        {
            GameObject powerUpSpawner = GameObject.Instantiate(powerUpGenerator); //create a powerUpSpawner
            powerUpSpawner.transform.position = position; //move powerUpSpawner to intended position
            NetworkServer.Spawn(powerUpSpawner); //try to spawn it on network.
            
        }
    }
}
