using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public Canvas quitMenu;
    public Button startButton;
    public Button exitButton;
    public Button aboutButton;

    void Start()
    {
        // get components
        quitMenu = quitMenu.GetComponent<Canvas>();
        startButton = startButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();

        aboutButton = aboutButton.GetComponent<Button>();
        // get quitMenu
        quitMenu.enabled = false;

    }
    // MainMenu
    public void ExitButton()
    {
        quitMenu.enabled = true;
        startButton.enabled = false;
        exitButton.enabled = false;
        aboutButton.enabled = false;

    }
    public void AboutButton()
    {
        // TODO about
    }
    public void StartLevel(int changeScene)
    {
        changeToScene(changeScene);
    }

    // ExitMenu
    public void NoPress()
    {
        quitMenu.enabled = false;
        startButton.enabled = true;
        exitButton.enabled = true;
        aboutButton.enabled = true;
    }

    public void YesPress()
    {
        Application.Quit();
    }
    public void changeToScene(int changeScene)
    {
        SceneManager.LoadScene(changeScene);
    }


}
