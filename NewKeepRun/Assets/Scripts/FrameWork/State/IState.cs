using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public abstract class IState
{
    public string Statename;
    public abstract void Start();
    public abstract void Update();
    public abstract void End();
}
