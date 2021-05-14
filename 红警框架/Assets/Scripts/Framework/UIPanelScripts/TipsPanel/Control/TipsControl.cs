using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsControl : ControlBase
{
    public TipsPanel tipsPanel;
    public TipsModel tipsModel;

    public override void Init()
    {
        tipsPanel = new TipsPanel();
        tipsModel = new TipsModel();

        tipsPanel.m_prefab = prefabPanel;
        SetModelAndView(tipsPanel, tipsModel);

        tipsPanel.m_confirm = OnConfirmClick;
        tipsPanel.m_cancel = OnCancelClick;
    }

    public override void Update()
    {

    }

    public override void End()
    {

    }

    public void OnConfirmClick()
    {
        CloseUI();
    }

    public void OnCancelClick()
    {
        CloseUI();
    }


    public void ShowContent(string text)
    {
        tipsPanel.ShowContent(text);
    }
}
