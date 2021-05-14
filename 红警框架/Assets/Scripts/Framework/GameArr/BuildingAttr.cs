using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttr : BaseAttr
{
    private int m_MaxHp;     //最大hp
    private float m_Attack;  //最大攻击力

    public BuildingAttr(int maxHp, float attack)
    {
        m_MaxHp = maxHp;
        m_Attack = attack;
    }

    public override float GetMaxAttack()
    {
        return m_Attack;
    }

    public override int GetMaxHp()
    {
        return m_MaxHp;
    }
}
