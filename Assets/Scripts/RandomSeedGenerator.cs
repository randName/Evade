using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RandomSeedGenerator : NetworkBehaviour {
    [SyncVar]
    int randomSeed;
    //[SyncVar]
    //int value;
	// Use this for initialization
	void Start () {
        
        randomSeed = Random.Range(0, 100);
	}
    //void Update()
    //{
    //    value = Random.Range(
    //}
    public int getSeed()
    {
        return randomSeed;
    }

    //public int getRandomNumber()
    //{
    //    return value; 
    //}
}
