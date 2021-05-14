using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadAssetPanel : IUIPanel
{

    public ReadAssetPanel(PBDG_GameMain pBDG_GameMain) : base(pBDG_GameMain)
    {

    }

    private MainPanel m_MainPanel = null;
    private Image m_Background;
    private Transform Assetcontent;

    /// <summary>
    /// 设置主控UI
    /// </summary>
    /// <param name="mainPanel"></param>
    public void SetMainPanel()
    {
        m_MainPanel = pBDG_Game.GetMainPanel();
        if (m_MainPanel != null)
        {
            prefabPanel = m_MainPanel.m_MenuPanel.transform.Find("ChooseAseetGame").gameObject;
            m_Background = prefabPanel.transform.GetComponent<Image>();
            Assetcontent = prefabPanel.transform.Find("Scroll View_Archive/Viewport/Content");
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

    public virtual void Update()
    {

    }
}
