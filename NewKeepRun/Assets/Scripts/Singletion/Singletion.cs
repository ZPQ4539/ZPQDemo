using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单利模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singletion<T> where T:new()
{
    private static T instancce;
    private static object obj = new object();

    public static T Instance
    {
        get
        {
            if (instancce == null)
            {
                lock (obj)
                {
                    instancce = new T();
                }
                
            }
            return instancce;
        }
    }
    
}
