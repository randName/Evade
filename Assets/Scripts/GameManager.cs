﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour //this script manages the game round.
{
    
    
    private int roundCount;
    List<GameObject> playerKeeper = new List<GameObject>(); //this will hold the player object instances.
    public Canvas joinMenu;
    bool startGame;
    bool gameStarted = false;
    bool endGame = false;
    double playerDeadCounter = 0;
    GameObject powerUpGeneratorSpawner;

    void Start()
    {
        //hardcoded test for now
        setPlayerMax(2);
        Debug.Log("This game requires "+ getRoundCount() + " players");
        
    }

    void Update()
    {
        
        if (readyToStart() &&!gameStarted)
        {
            joinMenu.GetComponent<GameSceneScript>().allJoined(); //make the loading screen disappear.
            powerUpGeneratorSpawner = GameObject.Find("PowerUpGeneratorSpawner");
            PowerUpGeneratorSpawner pugs = powerUpGeneratorSpawner.GetComponent<PowerUpGeneratorSpawner>();
            pugs.setGameStart(startGame); //start the game!
            gameStarted = true;
        }

        //Check end game condition and call GameSceneScript endGame for UI change
        if (playerDeadCounter >= (getRoundCount() - 1)) //only one player remaining.
        {
            endGame = true;
            joinMenu.GetComponent<GameSceneScript>().endGame();
            Debug.Log("GAME HAS ENDED");
            
            //change scene to display endgame UI. 
        }
        //TODO: If player rematch -> reset all players positions + states. Else switch out (this is trivial)
    }
    public void addToTheDead()//increments the dead player counter 
    {
        playerDeadCounter += 1;
    }

    public void findWinner() //TODO: Find winner by getting the only playercontroller left. Distinguish via name/colour
    {
        return;
        //GameObject winner = FindObjectOfType<PlayerController>().gameObject.name/colour <--- to be filled.
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

    public void resetAll()
    {
        gameStarted = false;
        startGame = false;
    }
}
