using OrderSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HomeView : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityAction<HomeItem> ComeBetter = null;

    private ObjectPool<HomeItemView> objectPool = null;
    private List<HomeItemView> Homes = new List<HomeItemView>();
    private Transform parent = null;

    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("Prefabs/UI/HomeItem");
        objectPool = new ObjectPool<HomeItemView>(prefab,"HomePool");
    }

    public void UpdataHome(IList<HomeItem> Homes,IList<Action<object>> objs)
    {
        for (int i = 0; i < this.Homes.Count; i++)
        {
            objectPool.Push(this.Homes[i]);
        }
        this.Homes.AddRange(objectPool.Pop(Homes.Count));

        for (int i = 0; i < this.Homes.Count; i++)
        {
            var home = this.Homes[i];
            home.transform.SetParent(parent);
            home.InitHome(Homes[i]);
            home.actionList = objs;
            home.GetComponent<Button>().onClick.RemoveAllListeners();
            home.GetComponent<Button>().onClick.AddListener(() =>
            {

            });
        }
    }

    public void UpdataState(HomeItem Home)
    {
        for (int i = 0; i < this.Homes.Count; i++)
        {
            if (this.Homes[Home.id-1])
            {
                Homes[Home.id - 1].InitHome(Home);
                return;
            }
        }
        //Homes[Home.id -1].InitHome(Home);
    }

    public void RefreshHome(IList<HomeItem> ReHomes)
    {
        for (int i = 0; i < ReHomes.Count; i++)
        {
            UpdataState(ReHomes[i]);
            
        }
    }

    //public HomeItemView GetHome(string name)
    //{
    //    foreach (HomeItemView item in Homes)
    //    {
    //        //if (item.)
    //        //{

    //        //}
    //    }
    //}
}
