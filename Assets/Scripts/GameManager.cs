﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour //this script manages the game round.
{
    public string winner = "";

    private int roundCount;
    List<GameObject> playerKeeper = new List<GameObject>(); //this will hold the player object instances.
    public Canvas joinMenu;
    bool startGame; //need to make sure that all players are connected before we start the ame. 
    public bool gameStarted = false; //check that the game is in progress
    bool endGame = false;   //check to end the game
    double playerDeadCounter = 0;   //counts the number of dead players
    
    GameObject powerUpGeneratorSpawner;
    

    void Start()
    {
        
        setPlayerMax(4); //number of players that should be in the game
        Debug.Log("This game requires "+ getRoundCount() + " players");
        joinMenu.gameObject.SetActive(true);
    }
    //Unity's update function
    void Update()
    {

        if (!gameStarted && readyToStart())
        {
            forceGameStart();
        }

        //Check end game condition and call GameSceneScript endGame for UI change
        if (playerDeadCounter >= (getRoundCount() - 1)) //only one player remaining.
        {
            endGame = true;
            findWinner(); 
            joinMenu.GetComponent<GameSceneScript>().endGame();            
            //change scene to display endgame UI. 
        }

    }
    public void addDeadCounter()//increments the dead player counter 
    {
        Debug.Log("PLAYER HAS FALLEN.");
        playerDeadCounter += 1;
    }

    public void findWinner() //Find winner by looking for remaining player controller. Set winner text to color string.
    {

        Color col = FindObjectOfType<PlayerController>().gameObject.GetComponent<MeshRenderer>().material.color;
        if (col == Color.red) {
            winner = "Red";
        }
        else if (col == Color.yellow)
        {
            winner = "Yellow";
        }
        else if(col == Color.blue)
        {
            winner = "Blue";
        }
        else if(col == Color.green)
        {
            winner = "Green";
        }

        winner = winner + " player has won!";  //set winner string to this color.

    }

    public int getPlayerCount() //check for current player count
    {
        return playerKeeper.Count;
    }

    public int getRoundCount() //check the number of players that are needed to start
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

    bool readyToStart() //check if the number of players in the game is at least equal to the needed player count
    {
        if (roundCount <= getPlayerCount())
        {
            startGame = true;
            
        }
        else
        {
            startGame = false;
            
        }

        return startGame;
    }

    public void resetAll() //this function resets the game to its initial state
    {
        gameStarted = false;
        startGame = false;
        playerKeeper.Clear();
        playerDeadCounter = 0;
        endGame = false;
    }

    public void forceGameStart() //this function causes the loading menu to disappear and start the game.
    {
        joinMenu.GetComponent<GameSceneScript>().allJoined(); //make the loading screen disappear.
        powerUpGeneratorSpawner = GameObject.Find("PowerUpGeneratorSpawner");
        PowerUpGeneratorSpawner pugs = powerUpGeneratorSpawner.GetComponent<PowerUpGeneratorSpawner>();
        pugs.setGameStart(startGame); //start the game!
        foreach (GameObject player in playerKeeper) //set player colours
        {
            player.GetComponent<PlayerController>().setColours();
        }
        gameStarted = true;
    }
}
