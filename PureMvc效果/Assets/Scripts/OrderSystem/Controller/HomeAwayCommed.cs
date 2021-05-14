using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class HomeAwayCommed : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        HomeProxy homeProxy = Facade.RetrieveProxy(HomeProxy.NAME) as HomeProxy;
        if (notification.Type == "GetHouse")
        {
            Order order = notification.Body as Order;

            foreach (var item in order.menus)
            {
                if (item.iselected)
                {
                    if (homeProxy.GetHouse(item.id)== null)
                    {
                        Debug.LogError("对不起您要住的房间客满");
                    }
                    else
                    {
                        HomeItem home = homeProxy.GetHouse(item.id);
                        home.state = 1;
                        SendNotification(OrderSystemEvent.ReFreshHouseitem, home);
                    }
                  
                }
            }
        }
        if (notification.Type == "Plus")
        {
            HomeItem home = (HomeItem)notification.Body;
            home.state++;
            SendNotification(OrderSystemEvent.ReFreshHouseitem, home);
        }
        if (notification.Type == "Over")
        {
            HomeItem home = (HomeItem)notification.Body;
            home.state = 0;
            SendNotification(OrderSystemEvent.ReFreshHouseitem, home);
        }
    }
}
