using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OrderSystem
{
    public class HomeMenuMediator :Mediator
    {
        private HomeMenuProxy menuProxy = null;
        public new const string NAME = "HomeMenuMediator";

        public HomeMenuView menuView
        {
            get
            {
                return (HomeMenuView)ViewComponent;
            }
        }

        public HomeMenuMediator(HomeMenuView view):base(NAME,view)
        {
            menuView.Submit += order => { SendNotification(OrderSystemEvent.SubHouseMenu, order);};
            menuView.Cancel += order => { SendNotification(OrderSystemEvent.EndPay, order); };
        }

        public override void OnRegister()
        {
            base.OnRegister();
            menuProxy = Facade.RetrieveProxy(HomeMenuProxy.NAME) as HomeMenuProxy;
            if (null == menuProxy)
            {
                throw new Exception(HomeMenuProxy.NAME + "is Null");
            }
            menuView.UpdateMenu(menuProxy.HomeMenus);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderSystemEvent.OpenHouseMenu);
            notifications.Add(OrderSystemEvent.SubHouseMenu);
            notifications.Add(OrderSystemEvent.EndPay);
            return notifications;
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case OrderSystemEvent.OpenHouseMenu:
                    Order order = notification.Body as Order;
                    if (null == order)
                        throw new Exception("order is null ,plase check it!");
                    menuView.UpMenu(order);
                    break;
                case OrderSystemEvent.EndPay:
                    Order order1 = notification.Body as Order;
                    if (order1 != null)
                    {
                        SendNotification(OrderSystemEvent.GET_ORDER, order1, "Exit") ;
                    }
                    break;
                case OrderSystemEvent.SubHouseMenu:
                    Order selectedOrder = notification.Body as Order;
                    menuView.SubmitMenu(selectedOrder);
                    SendNotification(OrderSystemEvent.SendHouseComClient, selectedOrder);
                    break;
            }
        }
    }
}


