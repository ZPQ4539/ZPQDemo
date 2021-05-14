using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrderSystem
{
    public class HomeMenuProxy : Proxy
    {
        public new const string NAME = "HomeMenuProxy";

        public IList<MenuItem> HomeMenus
        {
            get
            {
                return (IList<MenuItem>)base.Data;
            }
        }

        public HomeMenuProxy() :base(NAME,new List<MenuItem>())
        {
            AddMenu(new MenuItem(1, "大床房", 399, true));
            AddMenu(new MenuItem(2, "双床房", 599, true));
            AddMenu(new MenuItem(3, "三床房", 499, true));
            AddMenu(new MenuItem(4, "情侣套房", 1314, true));
            AddMenu(new MenuItem(5, "总统套房", 10000, true));

        }


        public void AddMenu(MenuItem homeMenuItem)
        {
            if (!HomeMenus.Contains(homeMenuItem))
            {
                HomeMenus.Add(homeMenuItem);
            }
        }

        public void Remove(MenuItem homeMenuItem)
        {
            if (HomeMenus.Contains(homeMenuItem))
            {
                HomeMenus.Remove(homeMenuItem);
            }
        }


        public void OutOfStock(MenuItem homeMenuItem)
        {
            foreach (var item in HomeMenus)
            {
                if (item.id == homeMenuItem.id)
                {
                    item.instock = false;
                    return;
                }
            }
        }
    }
}


