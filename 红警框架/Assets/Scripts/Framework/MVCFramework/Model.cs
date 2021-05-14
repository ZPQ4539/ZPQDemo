using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Model
{
    List<ModelBase> ALLModel = new List<ModelBase>();


    public virtual void Initialize()
    {
        foreach (var item in ALLModel)
        {
            item.Init();
        }
    }

    public virtual void Update()
    {
        foreach (var item in ALLModel)
        {
            item.Update();
        }
    }

    public virtual void End()
    {
        foreach (var item in ALLModel)
        {
            item.End();
        }
    }

    public void Add(ModelBase controlBase)
    {
        if (!ALLModel.Contains(controlBase))
        {
            ALLModel.Add(controlBase);
        }
    }
}
