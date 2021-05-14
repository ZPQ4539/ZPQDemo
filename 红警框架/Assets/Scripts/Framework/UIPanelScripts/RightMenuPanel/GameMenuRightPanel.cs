using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuRightPanel : IUIPanel
{

    public GameMenuRightPanel(PBDG_GameMain pBDG_GameMain) : base(pBDG_GameMain)
    {

    }

    private MainPanel m_MainPanel = null;

    /// <summary>
    /// 设置主控UI
    /// </summary>
    /// <param name="mainPanel"></param>
    public void SetMainPanel()
    {
        m_MainPanel = pBDG_Game.GetMainPanel();
        if (m_MainPanel != null)
        {
            prefabPanel = m_MainPanel.m_RightPanel.transform.Find("GameMenu").gameObject;
        }
    }

    public override void Begin()
    {
        if (m_MainPanel == null)
        {
            SetMainPanel();
            OpenUIPanel();
        }
        else
        {
            OpenUIPanel();
        }

    }

    public override void End()
    {
        CloseUIPanel();
    }

    public override void Update()
    {

    }
}
