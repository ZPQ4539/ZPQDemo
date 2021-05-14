using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : Singletion<AssetManager>
{
    Dictionary<string, object> AllAsset = new Dictionary<string, object>();

    public GameObject Load(string path,string name)
    {
        if (AllAsset.ContainsKey(name))
        {
            return AllAsset[name] as GameObject;
        }
        GameObject temp = Resources.Load<GameObject>(path + name);
        if (temp != null)
        {
            AllAsset.Add(name,temp);
            return temp;
        }
        return null;
    }

    public AudioClip LoadAudio(string path,string name)
    {
        if (AllAsset.ContainsKey(name))
        {
            return AllAsset[name] as AudioClip;
        }
        AudioClip temp = Resources.Load<AudioClip>(path + name);
        if (temp != null)
        {
            AllAsset.Add(name, temp);
            return temp;
        }
        return null;
    }

    public Sprite LoadSprite(string path,string name)
    {
        if (AllAsset.ContainsKey(name))
        {
            return AllAsset[name] as Sprite;
        }
        Sprite temp = Resources.Load<Sprite>(path + name);
        if (temp != null)
        {
            AllAsset.Add(name, temp);
            return temp;
        }
        return null;
    }
}
