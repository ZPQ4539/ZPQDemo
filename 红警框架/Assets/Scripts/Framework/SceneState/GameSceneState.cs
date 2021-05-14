using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneState : ISceneState
{
    public GameSceneState()
    {
        StateName = "GameSceneState";
    }

    public override void StateBegin()
    {
        PBDG_GameMain.Instance.Initinal();
    }

    public override void StateEnd()
    {
        PBDG_GameMain.Instance.Release();
    }

    public override void StateUpdate()
    {
        PBDG_GameMain.Instance.Update();
    }
}
