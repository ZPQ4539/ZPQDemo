using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrFactory : IAttrFactory
{
    private Dictionary<int, BaseAttr> m_SoldierAttrDB = null;

    public AttrFactory()
    {
        InitSoldierAttr();

    }

    public override SoldierAttr GetSoldierAttr(int Attr)
    {
        if (m_SoldierAttrDB.ContainsKey(Attr) == false)
        {
            Debug.LogError("未找到数据");
            return null;
        }

        SoldierAttr soldierAttr = new SoldierAttr();
        soldierAttr.SetSoldierAttr(m_SoldierAttrDB[Attr]);
        return soldierAttr;
    }

    private void InitSoldierAttr()
    {
        m_SoldierAttrDB = new Dictionary<int, BaseAttr>();

        m_SoldierAttrDB.Add(1,new BuildingAttr(100,5));
    }
}
