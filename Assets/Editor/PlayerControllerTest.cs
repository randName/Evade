using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PlayerControllerTest
{

    [Test]
    public void SizeTest()
    {
        var PlayerController = new PlayerController();
        PlayerController.setSize(2.0);

        Assert.AreEqual(2.0, PlayerController.getSize());
    }
    [Test]
    public void SpeedTest()
    {
        var PlayerController = new PlayerController();
        PlayerController.setSize(2.0);

        Assert.AreEqual(2.0, PlayerController.getSpeed());
    }
    [Test]
    public void MassTest()
    {
        var PlayerController = new PlayerController();
        PlayerController.setMass(2.0);

        Assert.AreEqual(2.0, PlayerController.getMass());
    }
    [Test]
    public void StunTest()
    {
        var PlayerController = new PlayerController();
        PlayerController.setCanStun(true);

        Assert.AreEqual(true, PlayerController.getCanStun());
    }
}
