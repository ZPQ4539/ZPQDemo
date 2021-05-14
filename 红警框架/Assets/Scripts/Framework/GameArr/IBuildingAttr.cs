using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色数值界面
/// </summary>
public abstract class IBuildingAttr
{
    protected BaseAttr m_BaseAttr = null;  //基本数据
    protected int m_NowHP = 0;             //当前血量

    public void SetBaseAttr(BaseAttr baseAttr)
    {
        m_BaseAttr = baseAttr;
    }

    public BaseAttr GetBaseAttr()
    {
        return m_BaseAttr;
    }
    /// <summary>
    /// 设定数值计算策略
    /// </summary>
    public void SetAttStrategy()
    {

    }
    //获取当前的hp
    public int GetNowHP()
    {
        return m_NowHP;
    }
    //获取最大hp
    public virtual int GetMaxHp()
    {
        return m_BaseAttr.GetMaxHp();
    }


}
