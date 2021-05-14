/*=========================================
* Author: Administrator
* DateTime:2017/6/20 19:20:37
* Description:$safeprojectname$
==========================================*/



using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OrderSystem
{
    public class HomeMedator : Mediator
    {
        private HomeProxy homeProxy = null;
        public new const string NAME = "HomeMediator";

        private HomeView View
        {
            get { return (HomeView)ViewComponent; }
        }

        public HomeMedator(HomeView view) : base(NAME, view)
        {

        }

        public override void OnRegister()
        {
            base.OnRegister();
            homeProxy = Facade.RetrieveProxy(HomeProxy.NAME) as HomeProxy;
            if (null == homeProxy)
            {
                throw new Exception("获取 + " + HomeProxy.NAME + "代理失败");
            }

            IList<Action<object>> actionList = new List<Action<object>>()
            {
                item => {

                },
                item => {

                    SendNotification(OrderSystemEvent.FindHouse,item,"Plus");
                },
                item => {

                    SendNotification(OrderSystemEvent.FindHouse,item,"Over");
                }
            };
            View.UpdataHome(homeProxy.Homes, actionList);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notification = new List<string>();
            notification.Add(OrderSystemEvent.SendHouseComClient);
            notification.Add(OrderSystemEvent.ReFreshHouseitem);
            return notification;
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case OrderSystemEvent.SendHouseComClient:
                    Order order = notification.Body as Order;
                    if (order != null)
                    {
                        foreach (var item in order.menus)
                        {
                            if (item.iselected != false)
                            {
                                SendNotification(OrderSystemEvent.FindHouse, order, "GetHouse");
                                return;
                            }
                        }
                        Debug.LogError("您没有选择房间");
                    }
                    break;
                case OrderSystemEvent.ReFreshHouseitem:
                    HomeItem homeItem = notification.Body as HomeItem;
                    View.UpdataState(homeItem);
                    break;
            }
        }
    }
}


