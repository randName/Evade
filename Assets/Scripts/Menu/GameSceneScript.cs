using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneScript : MonoBehaviour
{
    public Camera mainCam;
    public Camera loadingCam;
    public Canvas joinMenu;

    void Start()
    {
        mainCam.gameObject.SetActive(false);
        loadingCam.gameObject.SetActive(true);
        findAndSet(false, "Playing");
        findAndSet(false, "Loading");
        findAndSet(true, "Joining");
        
    }

    void findAndSet(bool active, string objectName) //find a sub-component of the menu and set its state.
    {
        GameObject comp = joinMenu.transform.Find(objectName).gameObject;
        comp.SetActive(active);
    }
    // JoinMenu
    public void JoinPress() //on pressing the join button, the menu disappears
    {
        findAndSet(false, "Joining");
        findAndSet(true, "Loading");
    }

    public void allJoined()
    {
        findAndSet(false, "Loading");
        loadingCam.gameObject.SetActive(false);
        findAndSet(true, "Playing");
        mainCam.gameObject.SetActive(true);
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

