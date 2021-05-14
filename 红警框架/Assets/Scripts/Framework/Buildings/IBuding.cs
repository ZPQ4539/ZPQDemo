using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 建筑物接口 ，工兵与坦克
/// </summary>
public abstract class IBuding
{
    //建筑物名称
    protected string m_Name = "";
    //建筑物模型
    protected GameObject m_GameObject = null;
    //声音组件
    protected AudioSource m_Audio = null;
    //寻路组件
    protected NavMeshAgent m_Navmesh = null;
    //显示图片
    protected string m_Icon;
    //模型名称
    protected string m_AssetName;
    //是否销毁
    protected bool m_isDie = false;
    //创建CD
    protected float m_Cd = 0;
    //创建位置
    protected Transform m_position;


    //数据
    protected IBuildingAttr m_AttrBute = null;
    //AI
    protected IBuilderAI m_BuilderAI = null;

    protected GameObject TargetEnemy = null;


    #region 设置游戏物体

    //设置游戏模型
    public void SetGameObject(GameObject theGameObject)
    {
        m_GameObject = theGameObject;
        //获取声音组件
        m_Audio = m_GameObject.GetComponent<AudioSource>();
        //获取导航组件
        m_Navmesh = m_GameObject.GetComponent<NavMeshAgent>();
        m_Navmesh.enabled = false;
        GetGameObject().transform.Find("FlamethrowerCartoonyBlue").GetComponent<ParticleSystem>().Stop();
    }
    //取得unity 模型
    public GameObject GetGameObject()
    {
        return m_GameObject;
    }

    #endregion

    //释放
    public void Release()
    {
        if (m_GameObject != null)
        {
            GameObject.Destroy(m_GameObject);
        }
    }

    //获取名称
    public string GetName()
    {
        return m_Name;
    }


    //取得目前的数据值
    public IBuildingAttr GetAttr()
    {
        return m_AttrBute;
    }


    public void Update(List<GameObject> temps)
    {
        if (m_BuilderAI != null)
        {
            m_BuilderAI.Update(temps);
        }

        if (TargetEnemy != null)
        {
            m_Cd += Time.deltaTime;
            if (m_Cd >= 5)
            {
                GameObject.Destroy(TargetEnemy);
                PBDG_GameMain.Instance.Remove("建筑物",TargetEnemy);
                m_BuilderAI.ChangeAIState<IdleAIState>("IdleAIState");
                TargetEnemy = null;
                m_Cd = 0;
            }
            if (m_Cd == 0)
            {
                StopPlayerEffect();
            }
        }
    }

    #region 移动

    //移动
    public void MoveToTager(Vector3 tager)
    {
        m_Navmesh.enabled = true;
        m_Navmesh.isStopped = false;

        StopPlayerEffect();
        m_Navmesh.SetDestination(tager);
    }



    //导航停止
    public void StopMove()
    {
        m_Navmesh.isStopped = true;
    }

    //获取位置
    public Vector3 GetPosition()
    {
        return m_GameObject.transform.position;
    }

    #endregion


 #region 设置AI

    public void SetAI(IBuilderAI builderAI)
    {
        m_BuilderAI = builderAI;
    }

    public IBuilderAI GetAI()
    {
        if (m_BuilderAI != null)
        {
            return m_BuilderAI;
        }
        return null;
    }

    #endregion


    /// <summary>
    /// 攻击目标点
    /// </summary>
    /// <param name="Target"></param>
    public void Attack(GameObject Target)
    {
        TargetEnemy = Target;
        m_GameObject.transform.LookAt(TargetEnemy.transform.position);
    }

    /// <summary>
    /// 被攻击
    /// </summary>
    /// <param name="attack"></param>
    public abstract void UnderAttack(IBuding attack);

    /// <summary>
    /// 是炮还是子弹
    /// </summary>
    public void SetWeapon()
    {

    }
    //死亡
    public void Killed()
    {

    }


    /// <summary>
    /// 播放声音
    /// </summary>
    public void PlayerSound(string ClipName)
    {
        //IAssetFactory Factory = PBDGFactory.GetAssetFactory();


        if (m_Audio != null)
        {
            m_Audio.Play();
        }

    }
    /// <summary>
    /// 播放特效
    /// </summary>
    public void PlayerEffect()
    {
        GetGameObject().transform.Find("FlamethrowerCartoonyBlue").GetComponent<ParticleSystem>().Play();
    }
    public void StopPlayerEffect()
    {
        GetGameObject().transform.Find("FlamethrowerCartoonyBlue").GetComponent<ParticleSystem>().Stop();
    }

    public void SetNav()
    {
        if (m_Navmesh != null)
        {
            m_Navmesh.enabled = true;
        }
   
    }

    public virtual void SetAttr(IBuildingAttr buildingAttr)
    {
        m_AttrBute = buildingAttr;
    }
}
