using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    
    
    private int roundCount;
    List<GameObject> playerKeeper = new List<GameObject>(); //this will hold the player object instances.

    bool startGame;
    bool gameStarted = false;
    GameObject powerUpGeneratorSpawner;

    void Start()
    {
        //hardcoded test for now
        setPlayerMax(2);
        Debug.Log("RoundCount"+ getRoundCount());
        powerUpGeneratorSpawner = GameObject.Find("PowerUpGeneratorSpawner");
    }

    void Update()
    {
        Debug.Log(getPlayerCount());
        if (readyToStart() &&!gameStarted)
        {
            PowerUpGeneratorSpawner pugs = powerUpGeneratorSpawner.GetComponent<PowerUpGeneratorSpawner>();
            pugs.setGameStart(startGame);
            gameStarted = true;
        }
    }

    public int getPlayerCount() //check for current player count
    {
        return playerKeeper.Count;
    }

    public int getRoundCount()
    {
        return roundCount;
    }

    void setPlayerMax(int max) //host sets the number of players that should join the game.
    {
        roundCount = max; 
    }

    public void addPlayer(GameObject newPlayer) //add a player if the player is ready to play.
    {
        playerKeeper.Add(newPlayer); 
    }

    bool readyToStart()
    {
        if (roundCount == getPlayerCount())
        {
            startGame = true;
            
        }
        else
        {
            startGame = false;
            
        }

        return startGame;
    }

    void resetAll()
    {
        gameStarted = false;
        startGame = false;
    }
}
