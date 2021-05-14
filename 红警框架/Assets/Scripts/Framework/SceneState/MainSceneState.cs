using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主场景
/// </summary>
public class MainSceneState : ISceneState
{
    public MainSceneState()
    {
        StateName = "MainScene";
    }
    /// <summary>
    /// 状态一开始调用
    /// </summary>
    public override void StateBegin()
    {
        //初始化游戏外观模式
        PBDG_GameMain.Instance.Initinal();
    }
    /// <summary>
    /// 状态结束后调用
    /// </summary>
    public override void StateEnd()
    {
        base.StateEnd();
        //场景结束后释放
        PBDG_GameMain.Instance.Release();
    }
    /// <summary>
    /// 更新
    /// </summary>
    public override void StateUpdate()
    {
        //更新
        PBDG_GameMain.Instance.Update();
    }
}
