using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 持有 m v  以及UI的预制体
/// </summary>
public abstract class ControlBase
{
    public ModelBase model;
    public ViewBase view;

    public GameObject prefabPanel;

    /// <summary>
    /// 绑定 View 与 model;
    /// </summary>
    public void SetModelAndView(ViewBase view,ModelBase model)
    {
        this.view = view;
        this.model = model;
        Control.Instance.AddViewAndModel(view,model);
    }

    public void SetPrefab(GameObject prefab)
    {
        this.prefabPanel = prefab;
    }

    public abstract void Init();
    public abstract void End();
    public abstract void Update();

    /// <summary>
    /// 有控制层 控制UI 界面显影
    /// </summary>
    public void OpenUI()
    {
        if (prefabPanel != null)
        {
            prefabPanel.SetActive(true);
        }
       
    }


    public void CloseUI()
    {
        if (prefabPanel != null)
        {
            prefabPanel.SetActive(false);
        }
    }
}
