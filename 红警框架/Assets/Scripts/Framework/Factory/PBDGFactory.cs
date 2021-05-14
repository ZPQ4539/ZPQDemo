using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全部工厂
/// </summary>
public static class PBDGFactory
{
    //资源工厂
    private static IAssetFactory AssetFactory = null;
    //角色工厂
    private static IBuidingFactory soliderFactory = null;
    //数据工厂
    private static IAttrFactory AttrFactory = null;


    /// <summary>
    /// 创建资源工厂
    /// </summary>
    /// <returns></returns>
    public static IAssetFactory GetAssetFactory()
    {
        if (AssetFactory == null)
        {
            AssetFactory = new AssetFactory();
        }
        return AssetFactory;
    }

    /// <summary>
    /// 游戏工兵工厂
    /// </summary>
    /// <returns></returns>
    public static IBuidingFactory GetBuidingFactory()
    {
        if (soliderFactory == null)
        {
            return soliderFactory = new BuidingFactory();
        }
        return soliderFactory;
    }

    public static IAttrFactory GetAttrFactory()
    {
        if (AttrFactory == null)
        {
            return AttrFactory = new AttrFactory();
        }
        return AttrFactory;
    }
}
