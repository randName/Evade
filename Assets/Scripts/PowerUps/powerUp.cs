using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public abstract class powerUp : NetworkBehaviour
{
    public abstract void accept(PlayerState player);
}








