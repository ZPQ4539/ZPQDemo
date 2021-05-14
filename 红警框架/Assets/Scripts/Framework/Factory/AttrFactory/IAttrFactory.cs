using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//数值工厂
public abstract class IAttrFactory
{
    public abstract SoldierAttr GetSoldierAttr(int AttrID);
}
