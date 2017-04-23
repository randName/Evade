using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : powerUp
{
    override
    public void accept(PlayerState player)
    {
        player.increaseSpeedTemp();
    }
}