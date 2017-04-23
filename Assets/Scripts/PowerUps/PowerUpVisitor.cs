using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PowerUpVisitor : MonoBehaviour { //This class enables others to implement the visit method
    public abstract void visitPowerUp(SpeedBoost speedboost);
    public abstract void visitPowerUp(IncreaseSize increaseSize);
    public abstract void visitPowerUp(StunNextPlayer stunner);
    public abstract void visitPowerUp(IncreaseMass increaseMass);
}
