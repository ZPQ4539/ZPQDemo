using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuRightPanel : IUIPanel
{

    public MainMenuRightPanel(PBDG_GameMain pBDG_GameMain) : base(pBDG_GameMain)
    {

    }

    private MainPanel m_MainPanel = null;

    //开始游戏菜单
    private Button m_State;
    //关闭游戏菜单
    private Button m_Close;
    //读取存档菜单
    private Button m_Read;
    //设置菜单
    private Button m_Set;



    /// <summary>
    /// 设置主控UI
    /// </summary>
    /// <param name="mainPanel"></param>
    public void SetMainPanel()
    {
        m_MainPanel = pBDG_Game.GetMainPanel();
        if (m_MainPanel != null)
        {
            prefabPanel = m_MainPanel.m_RightPanel.transform.Find("ButtonContent").gameObject;

            m_State = prefabPanel.transform.Find("btn_state").GetComponent<Button>();
            m_Read = prefabPanel.transform.Find("btn_read").GetComponent<Button>();
            m_Close = prefabPanel.transform.Find("btn_close").GetComponent<Button>();
            m_Set = prefabPanel.transform.Find("btn_set").GetComponent<Button>();

            m_State.onClick.AddListener(() =>
            {
                // SceneStateController.Instance.SetState("LoadScene", "MainScene");
                pBDG_Game.SetUIState(UIPanelState.ChooseMenu);
            });
            m_Close.onClick.AddListener(() =>
            {
                pBDG_Game.SetContent("0000");
            });
            m_Read.onClick.AddListener(() =>
            {
                pBDG_Game.SetUIState(UIPanelState.ChooseAseet);
            });
            m_Set.onClick.AddListener(() =>
            {

            });
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
