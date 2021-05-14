using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderSystem : IGameSystem
{

    public BuilderSystem(PBDG_GameMain pBDG_Game) : base(pBDG_Game)
    {

    }
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Release()
    {
        base.Release();
    }

    public override void Update()
    {
        base.Update();
    }

    public void Construct(IBuildingsBuilder theBuilder)
    {
        theBuilder.AddPBDGSystem(m_PBDGame);
        theBuilder.LoadAsset();
        theBuilder.AddAI();
        theBuilder.SetBuildingAttr();
      
    }
}
