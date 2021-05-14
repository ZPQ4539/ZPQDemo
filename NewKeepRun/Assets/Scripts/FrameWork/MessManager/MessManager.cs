using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 消息中心
/// </summary>
public class MessManager : Singletion<MessManager>
{
    Dictionary<int,Action<Notification>> MessDic = new Dictionary<int, Action<Notification>>();

    public void AddListener(int Id,Action<Notification> action)
    {
        if (MessDic.ContainsKey(Id))
        {
            MessDic[Id] += action;
        }
        else
        {
            MessDic.Add(Id, action);
        }
    }

    public void RemoveListener(int Id,Action<Notification> action)
    {
        if (MessDic.ContainsKey(Id))
        {
            MessDic[Id] -= action;
            if (MessDic[Id] == null)
            {
                MessDic.Remove(Id);
            }
        }
    }

    public void BroadCast(int id,Notification notification)
    {
        if (MessDic.ContainsKey(id))
        {
            MessDic[id].Invoke(notification);
        }
    }
}

public class Notification
{
    public object[] objs;

    public Notification(params object[] obj)
    {
        this.objs = obj;
    }
}
