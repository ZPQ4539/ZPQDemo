using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadAssetRightPanel : IUIPanel
{

    public ReadAssetRightPanel(PBDG_GameMain pBDG_GameMain) : base(pBDG_GameMain)
    {

    }

    private MainPanel m_MainPanel = null;

    //开始游戏菜单
    private Button m_State;
    //关闭游戏菜单
    private Button m_back;



    /// <summary>
    /// 设置主控UI
    /// </summary>
    /// <param name="mainPanel"></param>
    public void SetMainPanel()
    {
        m_MainPanel = pBDG_Game.GetMainPanel();
        if (m_MainPanel != null)
        {
            prefabPanel = m_MainPanel.m_RightPanel.transform.Find("ButtonChoose").gameObject;

            m_State = prefabPanel.transform.Find("btn_state").GetComponent<Button>();
            m_back = prefabPanel.transform.Find("btn_back").GetComponent<Button>();

            m_State.onClick.AddListener(() =>
            {
                //SceneStateController.Instance.SetState("LoadScene", "MainScene");
                pBDG_Game.LoadNewScene();
            });
            m_back.onClick.AddListener(() =>
            {
                pBDG_Game.BackUpState();
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
