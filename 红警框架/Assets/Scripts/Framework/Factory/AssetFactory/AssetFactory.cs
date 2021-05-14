using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源工厂  用于创建一些物体或者其他的东西
/// </summary>
public class AssetFactory : IAssetFactory
{
    //资源路径
    public const string BuildingPath = "prefab";
    //public const string 

    /// <summary>
    /// 对外提供创建接口
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public override GameObject Creat(string path,string name)
    {
        return Instancetiate(path,name);
    }

    /// <summary>
    ///  创建建筑物
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override GameObject CreatBuding(string name)
    {
        return Creat(BuildingPath, name);   
    }
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public override GameObject CreatSolider(string path, string name)
    {
        return Creat(path,name);
    }




    /// <summary>
    /// 通过ResourcesManager 来进行resource加载 这样的话可以在当前存一份对应的资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="AssetName"></param>
    /// <returns></returns>
    private GameObject Instancetiate(string path,string AssetName)
    {
        GameObject prefab = ResourcesManager.Instance.Load<GameObject>(path, AssetName, true);
        if (prefab == null)
        {
            return null;
        }
       GameObject temp =  GameObject.Instantiate(prefab);
        temp.name = AssetName;
        return temp;
        
    }

}
