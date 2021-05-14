using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单利模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singletion<T> where T: new()
{
    private static T instance;
    private static object obj = new object();

    public static T Instance
    {
        get
        {
            if (instance ==null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
}
