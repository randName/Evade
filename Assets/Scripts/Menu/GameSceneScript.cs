using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameSceneScript : MonoBehaviour
{
    public Camera mainCam;
    public Camera loadingCam;
    public Canvas joinMenu;
    public Canvas exitMenu;
    public Button rematchButton; //unofficially disabled.
    public Button exitButton;
    public GameObject clickerSound;
    public GameManager gm;
    public NetworkProperties np;
    public NetworkManager nm;


    void Start() //on start, the default loading page is enabled, network connection is also established
    {
        mainCam.gameObject.SetActive(false);
        loadingCam.gameObject.SetActive(true);
        
        findAndSet(true, "Loading");
        findAndSet(false, "Playing");
        findAndSet(false, "Exiting");
        
        np = GameObject.Find("NetworkProperties").GetComponent<NetworkProperties>();
        nm.networkAddress = np.roomIP;
        if ( np.isHost )
        {
            nm.StartHost();
        }
        else
        {
            nm.StartClient();
        }
    }



    void findAndSet(bool active, string objectName) //find a sub-component of the menu and set its state.
    {
        
        GameObject comp = joinMenu.transform.Find(objectName).gameObject;
        comp.SetActive(active);
    }

    public void hostPress() //upon joining, loading screen is set.
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

    public void allJoined() //this function switches the game menu to the playing state and allows all players to start playing
    {
        
        findAndSet(false, "Loading");
        loadingCam.gameObject.SetActive(false);
        findAndSet(true, "Playing");
        mainCam.gameObject.SetActive(true);
        
    }

    public void endGame() //Called from game manager, brings up the end game menu and removes buttons.
    {
        findAndSet(false, "Playing");
        findAndSet(true, "Exiting");
        Text text = joinMenu.transform.Find("Exiting").Find("ExitMenu").Find("WinText").GetComponent<Text>();
        text.text = gm.winner;
        
    }

    public void ExitButton(int mainMenuID) //goes back to the main menu and resets the current game state
    {
        playButtonPress();
        gm.resetAll();
        changeToScene(mainMenuID);

    }
    public void RematchButton(int gameSceneID) //goes to the new game scene and resets the current game state
    {
        playButtonPress();
        gm.resetAll();
        changeToScene(gameSceneID);
    }
    public void cancelPress(int changeScene) //when cancel button is pressed, change to desired scene
    {
        playButtonPress();
        changeToScene(changeScene);
    }
    public void changeToScene(int changeScene) //tells scene manager to load the desired scene
    {
        SceneManager.LoadScene(changeScene);
    }
    public void playButtonPress() //plays audio clip for each button
    {
        Instantiate(clickerSound);
    }

}

