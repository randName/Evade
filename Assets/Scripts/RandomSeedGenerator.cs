using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RandomSeedGenerator : NetworkBehaviour {
    [SyncVar]
    int randomSeed;
	// Use this for initialization
	void Start () {

        randomSeed = Random.Range(0, 100);
	}

    public int getSeed()
    {
        return randomSeed;
    }
}
