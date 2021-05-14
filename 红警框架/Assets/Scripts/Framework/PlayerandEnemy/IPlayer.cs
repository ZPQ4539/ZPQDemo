using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家属性接口
/// </summary>
public abstract class IPlayer
{
    public string yourname = null;                                      //名字
    public string yourcountry = null;                                   //国家
    public Color color;                                                 //营地颜色
     
    public List<IBuding> AllmyBuding = new List<IBuding>();            //玩家所有的建筑物 不包括营地

    /// <summary>
    /// 设置基本属性
    /// </summary>
    /// <param name="yourname"></param>
    /// <param name="yourcountry"></param>
    /// <param name="color"></param>
    protected IPlayer(string yourname, string yourcountry, Color color)
    {
        this.yourname = yourname;
        this.yourcountry = yourcountry;
        this.color = color;
    }

    //设置基本属性
    public abstract void Set(string yourname, string country,Color color);
}
