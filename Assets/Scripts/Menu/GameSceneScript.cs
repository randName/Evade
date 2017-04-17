using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Net;

public class GameSceneScript : NetworkBehaviour
{
    public Camera mainCam;
    public Camera loadingCam;
    public Canvas joinMenu;
    public Canvas exitMenu;
    public Button rematchButton; //unofficially disabled.
    public Button exitButton;
    public GameObject clickerSound;
    public GameManager gm;

    void Start()
    {
        mainCam.gameObject.SetActive(false);
        loadingCam.gameObject.SetActive(true);
        findAndSet(true, "Loading");
        findAndSet(false, "Playing");
        findAndSet(false, "Exiting");
        
    }

    void findAndSet(bool active, string objectName) //find a sub-component of the menu and set its state.
    {
        GameObject comp = joinMenu.transform.Find(objectName).gameObject;
        comp.SetActive(active);
    }

    public void hostPress()
    {
        playButtonPress();
        findAndSet(false, "Joining");
        findAndSet(true, "Loading");

    }

    // JoinMenu
    public void JoinPress() //on pressing the join button, the menu disappears and goes to the loading screen
    {
        playButtonPress();
        findAndSet(false, "Joining");
        findAndSet(true, "Loading");

    }

    public void allJoined() //if player joins network before pressing the Join button on UI, we end up with the loading text on the game screen.
        //One solution that I thought of would be to create a clearAll function but I have cameras involved as well which means I have to do abit more...
    {
        findAndSet(false, "Loading");
        loadingCam.gameObject.SetActive(false);
        findAndSet(true, "Playing");
        mainCam.gameObject.SetActive(true);
    }

    public void endGame() //Called from game manager
    {
        findAndSet(false, "Playing");
        findAndSet(true, "Exiting");
        Text text = joinMenu.transform.Find("Exiting").Find("ExitMenu").Find("WinText").GetComponent<Text>();
        text.text = gm.winner;
        
    }

    public void ExitButton(int mainMenuID)
    {
        playButtonPress();
        gm.resetAll();
        changeToScene(mainMenuID);

    }
    public void RematchButton(int gameSceneID)
    {
        playButtonPress();
        gm.resetAll();
        changeToScene(gameSceneID);
    }
    public void cancelPress(int changeScene)
    {
        playButtonPress();
        changeToScene(changeScene);
    }
    public void changeToScene(int changeScene)
    {
        SceneManager.LoadScene(changeScene);
    }
    public void playButtonPress()
    {
        Instantiate(clickerSound);
    }

}

