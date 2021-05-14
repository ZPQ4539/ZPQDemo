using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色AI
/// </summary>
public abstract class IBuilderAI
{
    //所有的状态
    private Dictionary<string,IAIState> AllState = new Dictionary<string, IAIState>();

    //角色
    protected IBuding m_buding = null;
    //攻击范围
    protected float m_AttackREange = 5;
    //角色AI状态
    protected IAIState m_AIState = null;

    protected const float Attack_Cool_Down = 1f;
    protected float m_CoolDown = Attack_Cool_Down;


    public IBuilderAI(IBuding buding)
    {
        m_buding = buding;
    }

    public virtual void ChangeAIState<T>(string State) where T : IAIState,new()
    {
        if (AllState.ContainsKey(State))
        {
            m_AIState = AllState[State];
        }
        else
        {
            AllState.Add(State, new T());
            AllState[State].SetBuilderAI(this);
            m_AIState = AllState[State];
        }
    }

    public IAIState GetIStaste()
    {
        return m_AIState;
    }


    public bool TargetInAttackRange(Vector3 Target)
    {
        if (Vector3.Distance(m_buding.GetPosition(),Target)<= m_AttackREange)
        {
            return true;
        }
        return false;
    }


    public void MoveTo(Vector3 position)
    {
        m_buding.MoveToTager(position);
    }

    public void StopMove()
    {
        m_buding.StopMove();
    }


    public void Update(List<GameObject> temp)
    {
        if (m_AIState != null)
        {
            m_AIState.Update(temp);
        }
    }

    public Vector3 GetPosition()
    {
        return m_buding.GetPosition();
    }

    public void PlayerEffect()
    {
        m_buding.PlayerEffect();
    }

    public void Attack(GameObject temp)
    {
        m_buding.Attack(temp);
    }

    public GameObject GetGame()
    {
        return m_buding.GetGameObject();
    }

}
