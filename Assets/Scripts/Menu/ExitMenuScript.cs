using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ExitMenuScript : MonoBehaviour {
    public Canvas exitMenu;
    public Button rematchButton;
    public Button exitButton;

    // Use this for initialization
    void Start () {
        
        exitMenu = exitButton.GetComponent<Canvas>();
        rematchButton = rematchButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        
    }

    //this function causes the game to load another scene
    private void changeToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    //this function is attached to the exit button and returns to main menu scene
    public void ExitButton(int mainMenuID)
    {
        
        changeToScene(mainMenuID);

    }

    //this function is attached to the rematch button and restarts the game scene
    public void RematchButton(int gameSceneID)
    {
        changeToScene(gameSceneID);
    }
}
