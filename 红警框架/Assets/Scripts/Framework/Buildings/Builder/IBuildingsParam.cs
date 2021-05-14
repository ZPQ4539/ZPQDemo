using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBuildingsParam
{
    public IBuding NewBuding = null;
    public Vector3 Position;
    public int AttrID;
    public string AssetName;
}

/// <summary>
/// 建筑物建造者
/// </summary>
public abstract class IBuildingsBuilder
{
    public abstract void SetBuildParam(IBuildingsParam buildingsParam);

    public abstract void LoadAsset();

    public abstract void SetBuildingAttr();

    public abstract void AddAI();

    public abstract void AddPBDGSystem(PBDG_GameMain pBDG_Game);
}
