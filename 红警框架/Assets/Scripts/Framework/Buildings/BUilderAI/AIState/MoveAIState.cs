using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAIState : IAIState
{
    private GameObject Attackposition;

    public override void SetAttackPoition(GameObject attackPosition)
    {
        Attackposition = attackPosition;
    }

    public override void Update(List<GameObject> Targer)
    {
        if (Attackposition != null)
        {
            if (m_BuilderAI.TargetInAttackRange(Attackposition.transform.position))
            {
                m_BuilderAI.MoveTo(m_BuilderAI.GetPosition() + m_BuilderAI.GetGame().transform.forward);
                m_BuilderAI.ChangeAIState<AttackAIState>("AttackAIState");
                if (m_BuilderAI.GetIStaste() is AttackAIState)
                {
                    AttackAIState attackAI = m_BuilderAI.GetIStaste() as AttackAIState;
                    attackAI.SetAttackPosition(Attackposition);
                    return;
                }
            } 
            m_BuilderAI.MoveTo(Attackposition.transform.position);
        }
      
    }
}
