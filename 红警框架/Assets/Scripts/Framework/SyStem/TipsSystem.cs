using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弹窗系统
/// </summary>
public class TipsSystem : IGameSystem
{
    //存放在配置文件夹下的弹出消息内容以及消息编号
    Dictionary<string, string> MeshTipsContent = new Dictionary<string, string>();

    public TipsSystem(PBDG_GameMain pBDG_Game):base(pBDG_Game)
    {
       TextAsset tempText = ResourcesManager.Instance.Find<TextAsset>("tips");

        string[] temContent = tempText.text.Split('\n');

        foreach (var item in temContent)
        {
            MeshTipsContent.Add(item.Split(',')[0],item.Split(',')[1]);
        }
    }

    /// <summary>
    /// 通知UI 进行 弹出 错误内容
    /// </summary>
    public void Notification(string Id,Action action = null)
    {
        if (MeshTipsContent.ContainsKey(Id))
        {
            m_PBDGame.OpenTipsUI(MeshTipsContent[Id]);
            if (action != null)
            {
                action.Invoke();
            }
        }
        else
        {
           // m_PBDGame.OpenTipsUI("Error");
            Debug.LogError("未找到提示内容检查是否设置");
        }
    }
}
