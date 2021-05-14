using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBuildParam: IBuildingsParam
{

}

public class SoldierBuilder : IBuildingsBuilder
{
    private SoldierBuildParam m_buildParam = null;
    private PBDG_GameMain m_pBDG = null;
    public override void AddAI()
    {
        SoldierAI theAI = new SoldierAI(m_buildParam.NewBuding);
        m_buildParam.NewBuding.SetAI(theAI);
    }

    public override void AddPBDGSystem(PBDG_GameMain pBDG_Game)
    {
        m_pBDG = pBDG_Game;
        //throw new System.NotImplementedException();
        //可以在这指定 策略
     

    }

    public override void LoadAsset()
    {
        IAssetFactory Factory = PBDGFactory.GetAssetFactory();
        GameObject soliderGameObject = Factory.CreatSolider("prefab",m_buildParam.AssetName);
        soliderGameObject.transform.position = m_buildParam.Position;
        soliderGameObject.gameObject.name = "Soldier";
        m_buildParam.NewBuding.SetGameObject(soliderGameObject);


       
        if (m_pBDG != null)
        {
            m_pBDG.AddSolider(m_buildParam.NewBuding as ISolider);
            m_pBDG.Add(soliderGameObject,"工兵");
        }

    }

    public override void SetBuildingAttr()
    {
        IAttrFactory Factroy = PBDGFactory.GetAttrFactory();
        int attrID = m_buildParam.AttrID;

       
        SoldierAttr soldierAttr = Factroy.GetSoldierAttr(attrID);

        

        m_buildParam.NewBuding.SetAttr(soldierAttr);
    }

    public override void SetBuildParam(IBuildingsParam buildingsParam)
    {
        m_buildParam = buildingsParam as  SoldierBuildParam;
    }
}
