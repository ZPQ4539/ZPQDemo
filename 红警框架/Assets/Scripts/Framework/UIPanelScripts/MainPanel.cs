using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏主界面
/// </summary>
public class MainPanel : IUIPanel
{
    public MainPanel(PBDG_GameMain pBDG_Game) : base(pBDG_Game)
    {

    }

    //菜单标签
    private Text m_MainStatetext;
    //状态标题
    private Text m_DownStatetext;
    //侧边栏
    public GameObject m_RightPanel;
    //状态栏
    public GameObject m_DownPanel;
    //菜单栏
    public GameObject m_MenuPanel;
    //当前显示UI面板
    public List<IUIPanel> m_NowUIPanel;



    public override void Begin()
    {
        prefabPanel = m_canvas.transform.Find("MainPanel").gameObject;
        m_RightPanel = prefabPanel.transform.Find("RightMain").gameObject;
        m_MainStatetext = m_RightPanel.transform.Find("text_title").GetComponent<Text>();

        //菜单栏
        m_MenuPanel = m_canvas.transform.Find("Menu").gameObject;
        //状态栏
         m_DownPanel = prefabPanel.transform.Find("DownMain").gameObject;
        //状态栏由本类继承
        m_DownStatetext = m_DownPanel.transform.Find("text_State").GetComponent<Text>();
        //初始化界面状态
        DownShowState();

        //第一次打开的界面
        SetBeginState();

        PBDG_GameMain.Instance.sceneType = SceneType.UIType;
    }

    public override void End()
    {

    }

    public override void Update()
    {
        //如果当前的面板不为空则执行
        if (m_NowUIPanel.Count > 0)
        {
            foreach (var item in m_NowUIPanel)
            {
                item.Update();
            }

        }
    }

    private void DownShowState(string State = "主菜单")
    {
        m_DownStatetext.text = "状态：" + State;
        m_MainStatetext.text = State;
    }

    /// <summary>
    /// 设置新的UI显示
    /// </summary>
    /// <param name="uIPanel"></param>
    public void SetUIShow(List<IUIPanel> uIPanel, string StateName)
    {
        if (m_NowUIPanel != null)
        {
            if (m_NowUIPanel.Count > 0)
            {
                foreach (var item in m_NowUIPanel)
                {
                    item.End();
                }
            }
        }
        m_NowUIPanel = uIPanel;
        foreach (var item in m_NowUIPanel)
        {
            item.Begin();
        }

        //更新状态栏
        DownShowState(StateName);
    }

    public void SetBeginState(UIPanelState uIPanel = UIPanelState.MainMenu)
    {
        pBDG_Game.SetUIState(uIPanel);
    }

    public void RefreshMoeny(Notification notification)
    {
        m_MainStatetext.text = ((int)notification.obj[0]).ToString();
    }
}
