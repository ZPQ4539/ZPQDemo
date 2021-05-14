using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : Singletion<ObserverManager>
{
    public Dictionary<string, List<Observer>> ObserverDic = new Dictionary<string, List<Observer>>();

    public void Add(string Id,Observer observer)
    {
        if (ObserverDic.ContainsKey(Id))
        {
            ObserverDic[Id].Add(observer);
        }
        else
        {
            ObserverDic.Add(Id, new List<Observer>() { observer });
        }
    }

    public void Remove(string Id,Observer observer)
    {
        if (ObserverDic.ContainsKey(Id))
        {
            ObserverDic[Id].Remove(observer);
            if (ObserverDic[Id].Count <= 0)
            {
                ObserverDic.Remove(Id);
            }
        }

    }

    public void Notification(string id)
    {
        if (ObserverDic.ContainsKey(id))
        {
            foreach (var item in ObserverDic[id])
            {
                item.Execute();
            }
        }
    }




}
