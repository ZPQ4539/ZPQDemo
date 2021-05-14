using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单利管理 所有UI界面
/// </summary>
public class UIManager :Singletion<UIManager>
{
    //常驻内存 所有UI
    Dictionary<string, ControlBase> UIDic = new Dictionary<string, ControlBase>();

    private Transform m_Canvas;

    public UIManager()
    {
        m_Canvas = GameObject.Find("Canvas").transform;
    }

    public void OpenUI<T>(string UIPanel)where T:ControlBase,new()
    {
        if (!UIDic.ContainsKey(UIPanel))
        {
            T temp = LoadControl<T>(UIPanel);
            if (temp != null)
            {
                UIDic.Add(UIPanel, temp);
            }
        }
       
        UIDic[UIPanel].OpenUI();
    }

    public void CloseUI(string UIPanel)
    {
        if (UIDic.ContainsKey(UIPanel))
        {
            UIDic[UIPanel].CloseUI();
        }
        else
        {
            Debug.LogError("没有UI数据");
        }

    }

    private T LoadControl<T>(string UIPanel)where T:ControlBase,new()
    {
        GameObject temp = ResourcesManager.Instance.Load<GameObject>("prefab",UIPanel,false);
        if (temp == null)
        {
            Debug.LogError("没有找到预制体");
            return default;
        }
        GameObject gameprefab = GameObject.Instantiate(temp, m_Canvas, false);

        ControlBase tempControl = null;
        if (Control.Instance.Find<T>(UIPanel) != null)
        {
            tempControl = Control.Instance.Find<T>(UIPanel);
            tempControl.SetPrefab(gameprefab);
        }
        else
        {
            Debug.LogError("error");
        }
  

        return tempControl as T;

    }
}
