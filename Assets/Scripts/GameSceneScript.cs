using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneScript : MonoBehaviour
{

    public Canvas joinMenu;
    void Start()
    {
        joinMenu.enabled = true;
    }
    // JoinMenu
    public void JoinPress()
    {
        joinMenu.enabled = false;
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

