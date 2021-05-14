using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAIState : IAIState
{
    public override void Update(List<GameObject> gameObjects)
    {
        foreach (var item in gameObjects)
        {
            if (Vector3.Distance(m_BuilderAI.GetPosition(),item.transform.position)<= 10)
            {
                m_BuilderAI.ChangeAIState<MoveAIState>("MoveAIState");
                if (m_BuilderAI.GetIStaste() is MoveAIState)
                {
                    MoveAIState temp = m_BuilderAI.GetIStaste() as MoveAIState;
                    temp.SetAttackPoition(item);
                }
                break;
            }
        }
    }
}
