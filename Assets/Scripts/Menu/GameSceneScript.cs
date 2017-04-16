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
    public GameManager gm;
    private NetworkManager nm;
    private byte[] ipbytes = IPAddress.Parse(Network.player.ipAddress).GetAddressBytes();

    void Start()
    {
        mainCam.gameObject.SetActive(false);
        loadingCam.gameObject.SetActive(true);
        findAndSet(false, "Playing");
        findAndSet(false, "Loading");
        findAndSet(false, "Exiting");
        findAndSet(true, "Joining");
        //nm = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        
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

        //InputField infi = GameObject.Find("InputField").GetComponent<InputField>();
        //infi.text = getRoomCode() + " " + getHostIP("ACEP");

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

