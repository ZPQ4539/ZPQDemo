using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑物建造工厂
/// </summary>
public class BuidingFactory : IBuidingFactory
{
    private BuilderSystem m_builderSystem = new BuilderSystem(PBDG_GameMain.Instance);

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public override ISolider CreatSolider(Vector3 pos)
    {
        if (PBDG_GameMain.Instance.m_player.GetMoney() < 100)
        {
            return null;
        }
        else
        {
            PBDG_GameMain.Instance.m_player.SetMoeny(100);
        }


        SoldierBuildParam soldierBuildParam = new SoldierBuildParam();
        soldierBuildParam.NewBuding = new Solider();

        soldierBuildParam.AssetName = "Rookie";
        soldierBuildParam.Position = pos;
        soldierBuildParam.AttrID = 1;

        SoldierBuilder soldierBuilder = new SoldierBuilder();
        soldierBuilder.SetBuildParam(soldierBuildParam);


        m_builderSystem.Construct(soldierBuilder);

        return soldierBuildParam.NewBuding as ISolider;
    }
}
