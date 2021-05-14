/*=========================================
* Author: Administrator
* DateTime:2021年2月25日08:22:43
* Description:$safeprojectname$
==========================================*/

using PureMVC.Patterns;
using UnityEngine;
using System.Collections.Generic;


namespace OrderSystem
{
    //客房部代理
    public class HomeProxy : Proxy
    {
        public new const string NAME = "HomeProxy";

        public IList<HomeItem> Homes
        {
            get
            {
                return (IList<HomeItem>)base.Data;
            }
        }

        public HomeProxy() :base(NAME,new List<HomeItem>())
        {
            AddHome(new HomeItem(1,"大床房",0));
            AddHome(new HomeItem(2,"双床房",0));
            AddHome(new HomeItem(3,"三床房",0));
            AddHome(new HomeItem(4,"情侣套房",0));
            AddHome(new HomeItem(5,"总统套房",0));
        }

        public void AddHome(HomeItem item)
        {
            if (Homes.Count < 5)
            {
                Homes.Add(item);
            }
            UpdateHome(item);
        }


        public void UpdateHome(HomeItem item)
        {
            Debug.Log(item.state);
            for (int i = 0; i < Homes.Count; i++)
            {
                if (Homes[i].id == item.id)
                {
                    Homes[i] = item;
                    Homes[i].state = item.state;
                    Homes[i].HomeType = item.HomeType;
                    SendNotification(OrderSystemEvent.ADD_GUEST,Homes[i]);
                    return;
                }
            }
        }

        public HomeItem  GetHouse(int id)
        {
            List<HomeItem> temp = new List<HomeItem>();

            foreach (var item in Homes)
            {
                if (item.id == id && item.state != 1)
                {
                 
                    return item;
                }
            }
            return null ;
        }
    }
}



