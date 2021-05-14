using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制层  持有 v 层 和 m 层 进行逻辑处理
/// </summary>
public class Control:Singletion<Control>
{
    Dictionary<string,ControlBase> ALLCollcer = new Dictionary<string,ControlBase>();

    public View view;        
    public Model model;

    public virtual void Initialize()
    {

        view = new View();
        model = new Model();

       


        foreach (var item in ALLCollcer.Values)
        {
            item.Init();
        }
        model.Initialize();
        view.Initialize();


    }

    public virtual void Update()
    {
        foreach (var item in ALLCollcer.Values)
        {
            item.Update();
        }


        model.Update();
        view.Update();
    }

    public virtual void End()
    {
        foreach (var item in ALLCollcer.Values)
        {
            item.End();
        }


        model.End();
        view.End();
    }

    public void Add(string UIPanel,ControlBase controlBase)
    {
        if (!ALLCollcer.ContainsKey(UIPanel))
        {
            ALLCollcer.Add(UIPanel,controlBase);
        }
    }
    public T Find<T>(string UIPanel)where T:ControlBase,new()
    {
        if (ALLCollcer.ContainsKey(UIPanel))
        {
            return ALLCollcer[UIPanel] as T;
        }
        Add(UIPanel, new T());
        return ALLCollcer[UIPanel] as T;
    }

    public void AddViewAndModel(ViewBase view,ModelBase model)
    {
        this.model.Add(model);
        this.view.Add(view);
    }
}
