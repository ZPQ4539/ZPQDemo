using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色接口
/// </summary>
public abstract class ISolider:IBuding
{
    // protected int m_minXue = 0;



    public override void UnderAttack(IBuding attack)
    {
        //throw new System.NotImplementedException();
    }
    //public ISolider(GameObject theGameObject)
    //{
    //    SetGameObject(theGameObject);
    //    m_Navmesh.enabled = false;
    //}

    public abstract void DoPlayKilledSound();
    public abstract void DOShowKIlledEffect();

}
