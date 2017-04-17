using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class NetworkProperties : MonoBehaviour {

    public Text roomCode;
    public InputField roomInput;
    public string roomIP;
    private byte[] ipbytes;

    void Start () {
        DontDestroyOnLoad(this);
        ipbytes = IPAddress.Parse(Network.player.ipAddress).GetAddressBytes();
        roomCode.text = getRoomCode();
    }

    public string getRoomCode()
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

    public void getHostIP()
    {
        string code = roomInput.text;
        byte[] nibs = new byte[4];
        for (byte i = 0; i < 4; i++) nibs[i] = (byte)((char)code[i] - 'A');
        int upper = (nibs[0] << 4) + nibs[1];
        int lower = (nibs[2] << 4) + nibs[3];
        roomIP = ipbytes[0].ToString() + '.' + ipbytes[1].ToString() + '.' + upper.ToString() + '.' + lower.ToString();
    }
}
