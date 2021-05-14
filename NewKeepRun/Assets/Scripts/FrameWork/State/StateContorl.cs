using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateContorl : Singletion<StateContorl>
{
    public Dictionary<string, IState> StateList = new Dictionary<string, IState>();
    public IState NowState;      //当前状态

    /// <summary>
    /// 更新状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    public void LoadNewState<T>(string name) where T : IState,new()
    {
        if (NowState != null)
        {
            NowState.End();
        }
        NowState = Find<T>(name);
        NowState.Start();
        //Debug.LogError("状态切换" + NowState.Statename);
    }

    public T Find<T>(string name) where T : IState,new()
    {
        if (StateList.ContainsKey(name))
        {
            return StateList[name] as T;
        }
        StateList.Add(name,new T());
        return StateList[name] as T;
    }

    public void Update()
    {
        if (NowState != null)
        {
            NowState.Update();
        }
    }
}
