using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMass : powerUp
{
    override
    public void accept(PlayerState player)
    {
        player.increaseMassTemp();
    }
}

