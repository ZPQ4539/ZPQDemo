using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class View
{
    List<ViewBase> AllView = new List<ViewBase>();

    public virtual void Initialize()
    {
        foreach (var item in AllView)
        {
            item.Init();
        }
    }

    public virtual void Update()
    {
        foreach (var item in AllView)
        {
            item.Update();
        }
    }

    public virtual void End()
    {
        foreach (var item in AllView)
        {
            item.End();
        }
    }

    public void Add(ViewBase controlBase)
    {
        if (!AllView.Contains(controlBase))
        {
            AllView.Add(controlBase);
        }
    }
}
