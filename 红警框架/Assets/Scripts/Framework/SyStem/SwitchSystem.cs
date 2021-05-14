using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum UIPanelState
{
    Null,          //无状态
    MainMenu,      //主菜单状态
    ChooseMenu,    //选择人物状态
    ChooseAseet,   //选择存档状态
    GameState      //游戏开始状态
}

/// <summary>
/// 界面切换状态系统
/// </summary>
public class SwitchSystem : IGameSystem
{
    //当前UI状态  以及UI状态绑定其他界面
    private UIPanelState m_UpUIState;                        //上一次状态
    private UIPanelState m_UIState = UIPanelState.Null;  //当前状态

    Dictionary<UIPanelState, List<IUIPanel>> UIStateDic = new Dictionary<UIPanelState, List<IUIPanel>>();
    public SwitchSystem(PBDG_GameMain pBDG_Game):base(pBDG_Game)
    {
       
    }

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="uIPanelState"></param>
    public void SetUIState(UIPanelState uIPanelState)
    {
        if (this.m_UIState != UIPanelState.Null)
        {
            //保存切换之前的状态
           
        }
        this.m_UpUIState = m_UIState;

        //切换状态
        this.m_UIState = uIPanelState;
        m_PBDGame.SetUIStateShow(FindState(this.m_UIState), this.m_UIState.ToString());
    }

    /// <summary>
    /// 添加状态绑定
    /// </summary>
    /// <param name="uIPanelState"></param>
    /// <param name="uIPanel"></param>
    public void AddUIPanelState(UIPanelState uIPanelState,IUIPanel uIPanel)
    {
        if (UIStateDic.ContainsKey(uIPanelState))
        {
            UIStateDic[uIPanelState].Add(uIPanel);
        }
        else
        {
            UIStateDic.Add(uIPanelState, new List<IUIPanel>() {uIPanel});
        }
    }
    /// <summary>
    /// 移除状态绑定
    /// </summary>
    /// <param name="uIPanelState"></param>
    public void RemoveState(UIPanelState uIPanelState)
    {
        if (UIStateDic.ContainsKey(uIPanelState))
        {
            UIStateDic.Remove(uIPanelState);
        }
    }

    private List<IUIPanel> FindState(UIPanelState uIPanelState)
    {
        if (UIStateDic.ContainsKey(uIPanelState))
        {
            return UIStateDic[uIPanelState];
        }
        return null;
    }

    public void BackUpState()
    {
        if (m_UpUIState !=  UIPanelState.Null)
        {
            SetUIState(m_UpUIState);
        }
    }
}
