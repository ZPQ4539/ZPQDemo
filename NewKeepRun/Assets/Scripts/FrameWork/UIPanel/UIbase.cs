using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// UI基类
/// </summary>
public class UIbase:MonoBehaviour
{
    public string UIName;         //UI名称

    public virtual void InitDate()
    {

    }


    public virtual void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
