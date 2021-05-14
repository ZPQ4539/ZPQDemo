using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI 状态界面
/// </summary>
public abstract class IAIState
{
    protected IBuilderAI m_BuilderAI = null;

    public void SetBuilderAI(IBuilderAI builderAI)
    {
        m_BuilderAI = builderAI;
    }

    public virtual void SetAttackPoition(GameObject attackPosition)
    {

    }

    public abstract void Update(List<GameObject> Targer);

    public virtual void MoveTarget(Vector3 pos)
    {
        m_BuilderAI.MoveTo(pos);
    }
}
