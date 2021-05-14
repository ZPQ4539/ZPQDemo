using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局工厂模式接口
/// </summary>
public abstract class IAssetFactory
{   /// <summary>
///  建造建筑物
/// </summary>
/// <param name="name"></param>
/// <returns></returns>
    public abstract GameObject CreatBuding(string name);
    /// <summary>
    /// 建造角色
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public abstract GameObject CreatSolider(string path, string name);
    /// <summary>
    /// 创建资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public abstract GameObject Creat(string path, string name);
}
