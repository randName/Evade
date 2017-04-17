using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject quitMenu;
    public GameObject joinMenu;
    public GameObject hostMenu;
    public GameObject startSelectionMenu;
    public GameObject clickerSound;
    public Button startButton;
    public Button exitButton;
    public Button aboutButton;
    public NetworkProperties networkProperties;
    

    void Start()
    {
        // get components
        startButton = startButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();

        aboutButton = aboutButton.GetComponent<Button>();
        // get quitMenu
        quitMenu.SetActive(false);
        hostMenu.SetActive(false);
        joinMenu.SetActive(false);
        startSelectionMenu.SetActive(false);

    }
    //Open up only one menu at a time. Closes all other menus.
    private void enableMenu(GameObject menu)
    {
        playButtonPress();
        hostMenu.SetActive(false);
        joinMenu.SetActive(false);
        startSelectionMenu.SetActive(false);
        quitMenu.SetActive(false);
        menu.SetActive(true);

    }
    //When host button is pressed on select.
    public void selectHost()
    {
        enableMenu(hostMenu);
    }
    //When join button is pressed on select.
    public void selectJoin()
    {
        enableMenu(joinMenu);
    }
    //When start button is pressed.
    public void enterSelection()
    {
        enableMenu(startSelectionMenu);
    }
    //Close all menus when cancel is pressed.
    public void closeAllMenus()
    {
        playButtonPress();
        hostMenu.SetActive(false);
        joinMenu.SetActive(false);
        startSelectionMenu.SetActive(false);
        quitMenu.SetActive(false);
        
    }

    public void hostGame()
    {
        playButtonPress();
        changeToScene(1);
    }

    //TODO: add logic to host game and join game.
    public void joinGame()
    {
        networkProperties.getHostIP();
        playButtonPress();
        changeToScene(1);
    }
    
    public void openExit()
    {
        enableMenu(quitMenu);
    }
    public void AboutButton()
    {
        playButtonPress();
        // TODO about
        //enableMenu(aboutMenu);
    }


    // ExitMenu
    public void NoPress()
    {
        closeAllMenus();
    }

    public void YesPress()
    {
        playButtonPress();
        Application.Quit();
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
