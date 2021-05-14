using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源加载管理类
/// </summary>
public class ResourcesManager : Singletion<ResourcesManager>
{
    Dictionary<string, FileAsset> ResDic = new Dictionary<string, FileAsset>();

    /// <summary>
    /// 加载资源 保存到一起进行管理，全局资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="isRemove"></param>
    /// <returns></returns>
    public T Load<T>(string path,string name,bool isRemove) where T:Object
    {
        if (!ResDic.ContainsKey(name))
        {
            string tempPath = path + "/" + name;
            T tempAssets = Resources.Load<T>(tempPath);
            if (tempAssets == null)
            {
                Debug.LogError("没有找到该资源");
                return null;
            }
            ResDic.Add(name,new FileAsset(tempAssets,isRemove));
        }
        return ResDic[name].obj as T;
    }
   
    /// <summary>
    /// 清除标记后的资源，谨慎操作
    /// </summary>
    public void RemveAll()
    {
        List<string> removeNames = new List<string>();
        foreach (var item in ResDic)
        {
            if (item.Value.isRemove)
            {
                removeNames.Add(item.Key);
            }
        }

        for (int i = 0; i < removeNames.Count; i++)
        {
            if (ResDic.ContainsKey(removeNames[i]))
            {
                ResDic.Remove(removeNames[i]);
            }
        }

        Debug.LogWarning("清除部分标记资源完成");
    }
    /// <summary>
    /// 查找资源
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Find<T>(string name) where T:Object
    {
        if (ResDic.ContainsKey(name))
        {
            return ResDic[name].obj as T;
        }
        else
        {
            Debug.LogError("未找到该资源,未加载,或者是被标记清理掉了");
            return null;
        }
    }

}
/// <summary>
/// 资源类
/// </summary>
public class FileAsset
{
    //资源文件
    public object obj;
    //是否标记删除  //切换状态后
    public bool isRemove = true;

    public FileAsset(object obj, bool isRemove)
    {
        this.obj = obj;
        this.isRemove = isRemove;
    }
}
