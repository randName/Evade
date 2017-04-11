using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StunNextPlayer : powerUp
{
    override
    public void accept(PlayerState player)
    {
        player.stunNextPlayerTemp();
    }
}