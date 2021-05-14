using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息中心
/// </summary>
public class MessManager
{
    public Dictionary<string, Action<Notification>> ComDic = new Dictionary<string, Action<Notification>>();
    private Action<Notification> action;

    public void AddCom(string name,Action<Notification> action)
    {
        if (ComDic.ContainsKey(name))
        {
           // Debug.LogError("已经添加过该命令了");
            return;
        }
        ComDic.Add(name,action);
    }
    private Action<Notification> Find(string name)
    {
        if (ComDic.ContainsKey(name))
        {
            return ComDic[name];
        }
        return null;
    }

    public void RemoveCom(string name)
    {
        if (ComDic.ContainsKey(name))
        {
            ComDic.Remove(name);
        }
    }

    public void NotiFication(string name,Notification notification)
    {
        action = Find(name);
        if (action != null)
        {
            //Execute(notification);
        }
        else
        {
            Debug.LogError("消息执行失败");
        }
    }

}

public class Notification
{
    public object[] obj;
    public Notification(params object[] obj)
    {
        this.obj = obj;
    }
}

