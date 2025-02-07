using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using RotaryHeart.Lib.SerializableDictionary;
[System.Serializable]
public class MonoFSM
{

    public List<MonoBaseState> stateList;//×´Ì¬ÁÐ±í

    private MonoBaseState curState;
    private MonoBaseState defaultState;


    public void InitDefaultState(int StateID)
    {
        if (StateID < 0 || StateID >= stateList.Count) return;

        curState = stateList[StateID];
        curState.OnEnter();
    }
    public MonoBaseState GetState(int stateID)
    {
        if (stateID < 0 || stateID >= stateList.Count) return null;
        return stateList[stateID];
    }
    public MonoBaseState GetCurrentState()
    {
        return curState;
    }
    public virtual void SwitchState(MonoBaseState nextState)
    {
        if (curState == nextState) return;
        curState.OnExit();
        curState = nextState;
        curState.OnEnter();
    }
    public void SwitchState(int nextstateID)
    {
        if (nextstateID < 0 || nextstateID >= stateList.Count) return;

        SwitchState(stateList[nextstateID]);

    }

    public void Update()
    {
        curState.OnUpdate();
    }
    public void FixedUpdate()
    {
        curState.OnFixedUpdate();
    }

}


