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

    // Update is called once per frame
    private void changeToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    public void ExitButton(int mainMenuID)
    {
        changeToScene(mainMenuID);

    }
    public void RematchButton(int gameSceneID)
    {
        changeToScene(gameSceneID);
    }
}
