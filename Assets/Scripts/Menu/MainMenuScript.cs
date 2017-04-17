﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Net;

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
    public Text roomCode;
    public InputField roomInput;
    private NetworkManager nm;
    private byte[] ipbytes;
    

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

        nm = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        ipbytes = IPAddress.Parse(Network.player.ipAddress).GetAddressBytes();
        roomCode.text = getRoomCode();
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
        NetworkClient host = nm.StartHost();
        changeToScene(1);
    }

    //TODO: add logic to host game and join game.
    public void joinGame()
    {
        nm.networkAddress = getHostIP(roomInput.text);
        NetworkClient client = nm.StartClient();
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

    string getRoomCode()
    {
        byte[] nb = new byte[4];
        for (byte i = 0; i < 4; i++)
        {
            byte b = ipbytes[2 + i / 2];
            if (i % 2 == 0)
            {
                b = (byte)((b & 0xF0) >> 4);
            }
            else
            {
                b &= 0x0F;
            }
            nb[i] = (byte)(b + (byte)'A');
        }
        return System.Text.Encoding.ASCII.GetString(nb);
    }

    string getHostIP(string code)
    {
        byte[] nibs = new byte[4];
        for (byte i = 0; i < 4; i++) nibs[i] = (byte)((char)code[i] - 'A');
        int upper = (nibs[0] << 4) + nibs[1];
        int lower = (nibs[2] << 4) + nibs[3];
        return ipbytes[0].ToString() + '.' + ipbytes[1].ToString() + '.' + upper.ToString() + '.' + lower.ToString();
    }
}
