using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class MonoBaseState : MonoBehaviour
{

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit(MonoBaseState nextState);

    public abstract void OnFixedUpdate();


}


