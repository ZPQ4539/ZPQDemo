using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 界面接口 提供一些方法
/// </summary>
public  class IUIPanel
{
    //PBDG
    public PBDG_GameMain pBDG_Game;
    //画布
    public Transform m_canvas;
    //UI界面
    public GameObject prefabPanel;

    public string m_PaneName = "IUIPanel";          //界面名称
    public IUIPanel(PBDG_GameMain pBDG_Game)
    {
        this.pBDG_Game = pBDG_Game;
        m_canvas = GameObject.Find("Canvas").transform;
    }
    public IUIPanel()
    {
        m_canvas = GameObject.Find("Canvas").transform;
    }


    public virtual void Begin()
    {
        m_canvas = GameObject.Find("Canvas").transform;
    }

    public virtual void End()
    {

    }

    public virtual void Update()
    {

    }

    //打开UI界面
    public virtual void OpenUIPanel()
    {
        prefabPanel.SetActive(true);
    }

    public virtual void CloseUIPanel()
    {
        prefabPanel.SetActive(false);
    }
}
