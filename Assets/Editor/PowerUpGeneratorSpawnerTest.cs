

using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PowerUpGeneratorSpawnerTest
{

    [Test]
    public void SeedTest()
    {
        //Arrange
        var Spawner = new PowerUpGeneratorSpawner();
        Spawner.RpcSetRandomSeed(4);

        Assert.AreEqual(4, Spawner.getRandomSeed());

    }
}
