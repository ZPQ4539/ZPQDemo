using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 命令模式 接口
/// </summary>
public abstract class ICommend
{
    private int cd = 0;

    //执行间隔
    public void SetCd(int cd)
    {
        this.cd = cd;
    }

    public int GetCd()
    {
        return cd;
    }

    public abstract void Execute();
}
