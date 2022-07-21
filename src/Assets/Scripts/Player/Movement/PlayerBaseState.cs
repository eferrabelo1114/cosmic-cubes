using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    public abstract void EnterState();
    public abstract PlayerBaseState UpdateState();
    public abstract void ExitState();
    public abstract string GetState();
}
