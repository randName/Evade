using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class GameManagerTest : MonoBehaviour {

    [Test]
    public void PlayerCountTest()
    {
        var Player1 = new GameObject();
        var Player2 = new GameObject();
        var GameManager = new GameManager();
        GameManager.addPlayer(Player1);
        GameManager.addPlayer(Player2);

        Assert.AreEqual(2, GameManager.getPlayerCount());
    }

}
