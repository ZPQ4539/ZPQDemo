using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : IAIState
{
    private IBuding m_Buider = null;
    private GameObject temp;


    public void SetAttackPosition(GameObject temp)
    {
        this.temp = temp;
    }

    public override void Update(List<GameObject> Targer)
    {
        if (temp != null)
        {
            m_BuilderAI.PlayerEffect();
            m_BuilderAI.Attack(temp);
        }
    }
}
