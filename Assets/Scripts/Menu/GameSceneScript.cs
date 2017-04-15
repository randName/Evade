using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameSceneScript : NetworkBehaviour
{
    public Camera mainCam;
    public Camera loadingCam;
    public Canvas joinMenu;
    public Canvas exitMenu;
    public Button rematchButton; //unofficially disabled.
    public Button exitButton;
    public GameManager gm;
    private NetworkManager nm;

    void Start()
    {
        mainCam.gameObject.SetActive(false);
        loadingCam.gameObject.SetActive(true);
        findAndSet(false, "Playing");
        findAndSet(false, "Loading");
        findAndSet(false, "Exiting");
        findAndSet(true, "Joining");
        //nm = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        
    }




    void findAndSet(bool active, string objectName) //find a sub-component of the menu and set its state.
    {
        GameObject comp = joinMenu.transform.Find(objectName).gameObject;
        comp.SetActive(active);
    }

    public void hostPress()
    {
        findAndSet(false, "Joining");
        findAndSet(true, "Loading");

        //NetworkClient host = nm.StartHost(); //after starting host...?
    }

    // JoinMenu
    public void JoinPress() //on pressing the join button, the menu disappears and goes to the loading screen
    {
        findAndSet(false, "Joining");
        findAndSet(true, "Loading");

        //NetworkClient client = nm.StartClient(); //need to put arguments here.
    }

    public void allJoined()
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
    }

    public void ExitButton(int mainMenuID)
    {
        
        changeToScene(mainMenuID);

    }
    public void RematchButton(int gameSceneID)
    {
        
        gm.resetAll();
        changeToScene(gameSceneID);
    }
    public void cancelPress(int changeScene)
    {
        changeToScene(changeScene);
    }
    public void changeToScene(int changeScene)
    {
        SceneManager.LoadScene(changeScene);
    }

}

