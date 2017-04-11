using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PowerUpVisitor : MonoBehaviour {
    public abstract void visitPowerUp(SpeedBoost speedboost);
    public abstract void visitPowerUp(IncreaseSize increaseSize);
    public abstract void visitPowerUp(StunNextPlayer stunner);
}
