using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加载过渡场景
/// </summary>
public class LoadSceneState : ISceneState
{
    //加载界面
    private LoadPanel loadPanel;
    //加载速度
    private float m_speed;
    //加载类型
    private LoadSceneType m_sceneType;
    //加载进度
    private float m_LoadNumber;
    //加载到
    private string m_NextSceneName;


    public LoadSceneState()
    {
        StateName = "LoadScene";
        //设置初始状态
        SetNextSceneSpeedandType();
    }

    /// <summary>
    /// 状态一开始调用
    /// </summary>
    public override void StateBegin()
    {
        loadPanel = new LoadPanel();
        loadPanel.Begin();

        if (loadPanel != null)
        {
            loadPanel.SetNextSceneState(m_NextSceneName, m_sceneType, m_speed);
        }
    }
    /// <summary>
    /// 状态结束后调用
    /// </summary>
    public override void StateEnd()
    {
        base.StateEnd();
        if (loadPanel != null)
        {
            loadPanel.End();
        }
        m_LoadNumber = 0;
    }
    /// <summary>
    /// 更新
    /// </summary>
    public override void StateUpdate()
    {
        if (loadPanel != null)
        {
            m_LoadNumber = loadPanel.GetNowIndexNumber();
            loadPanel.Update();
            if (m_LoadNumber >= 99)
            {
                SceneStateController.Instance.SetState(m_NextSceneName, false);
            }
           
        }
    }
    /// <summary>
    /// 对外提供设置加载状态方式
    /// </summary>
    /// <param name="type"></param>
    /// <param name="speed"></param>
    public void SetNextSceneSpeedandType(LoadSceneType type = LoadSceneType.One,float speed = 30)
    {
        m_sceneType = type;
        m_speed = speed;
    }

    public override void LoadTransitionScene(string NextSceneName)
    {
        m_NextSceneName = NextSceneName;
    }

}
