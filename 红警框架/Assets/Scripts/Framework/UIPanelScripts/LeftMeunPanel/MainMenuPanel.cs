using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : IUIPanel
{

    public MainMenuPanel(PBDG_GameMain pBDG_GameMain) : base(pBDG_GameMain)
    {

    }

    private MainPanel m_MainPanel = null;
    private Image m_Background;

    /// <summary>
    /// 设置主控UI
    /// </summary>
    /// <param name="mainPanel"></param>
    public void SetMainPanel()
    {
        m_MainPanel = pBDG_Game.GetMainPanel();
        if (m_MainPanel != null)
        {
            prefabPanel = m_MainPanel.m_MenuPanel.transform.Find("LeftMainMenu").gameObject;
            m_Background = prefabPanel.transform.Find("Background").GetComponent<Image>();
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
