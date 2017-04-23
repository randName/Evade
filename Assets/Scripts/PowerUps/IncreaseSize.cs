using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : powerUp
{
    override
    public void accept(PlayerState player)
    {
        player.increaseSizeTemp();
    }
}