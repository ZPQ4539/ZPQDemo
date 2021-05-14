using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelUI : UIbase
{

    private Transform shopitemContent;

    private Button btn_buy;
    private Button btn_buyView;
    private RawImage showRole;
    private Text money;

    private Button btn_Close;

    List<Shopitem> shopitems = new List<Shopitem>();
    List<Button> Btn_Temps = new List<Button>();           //所有商品组件


    public Action closeAction;
    public Action<float> rotionChanager;                          //旋转
    public Action<Shopitem> chanagerRole;                         //切换角色
    public Action<Shopitem> BuyShop;

    private Shopitem nowChooseShop;                              //当前选中要购买的道具


    void Start()
    {
        shopitems = ShowDate.Instance.ShopItems;                                       //获取商品数据

        shopitemContent = transform.Find("Scroll View/Viewport/Content");

        btn_Close = transform.Find("btn_close").GetComponent<Button>();
        btn_buy = transform.Find("btn_Buy").GetComponent<Button>();
        btn_buyView = transform.Find("btn_BuyView").GetComponent<Button>();
        showRole = transform.Find("ShowRole").GetComponent<RawImage>();
        money = transform.Find("Moeny/Text").GetComponent<Text>();
        btn_Close.onClick.AddListener(() => { closeAction(); });


        btn_buy.onClick.AddListener(BuyShops);
        btn_buyView.onClick.AddListener(() => { });


        //显示商品
        ShopitemInit();
        //金钱初始化
        RefreshMoney(PassDate.Instance.playerInit.money);
       
    }
    void BuyShops()
    {
        BuyShop(nowChooseShop);
        RefreshMoney(PassDate.Instance.playerInit.money);
    }


    void RefreshMoney(int money)
    {
        this.money.text = money.ToString();
    }

    /// <summary>
    /// 商城初始化
    /// </summary>
    void ShopitemInit()
    {

        for (int i = 0; i < shopitemContent.childCount; i++)
        {
            Btn_Temps.Add(shopitemContent.GetChild(i).GetComponent<Button>());
        }

        foreach (var item in shopitems)
        {
            if (item.id - 1 < Btn_Temps.Count)
            {
                ShowItemImage(Btn_Temps[item.id - 1].gameObject, shopitems[item.id -1]);
            }
            else
            {
                GameObject temp = GameObject.Instantiate(AssetManager.Instance.Load("Prefab/", "Shopitem"), shopitemContent, false);
                Button btn_temp = temp.GetComponent<Button>();
                btn_temp.name = item.id.ToString();
                Btn_Temps.Add(btn_temp);
                btn_temp.onClick.AddListener(() => { RefreshShowRole(item);});


                ShowItemImage(Btn_Temps[item.id - 1].gameObject, shopitems[item.id - 1]);
            }
        }
    }
    private void RefreshShowRole(Shopitem shopitem)
    {
        btn_buy.transform.Find("money").GetComponent<Text>().text = shopitem.price.ToString();
        chanagerRole(shopitem);
        nowChooseShop = shopitem;
    }


    private void ShowItemImage(GameObject temp,Shopitem shopitem)
    {
        temp.GetComponent<Image>().sprite = AssetManager.Instance.LoadSprite("Image/",shopitem.icon);
    }


    float beginPos;
    float tempPos;

    float upPos = 0;  //增量

    void Update()
    {
        if (showRole.IsActive())
        {
          
            if (Input.GetMouseButtonDown(0))
            {
                beginPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
                tempPos = beginPos;
            }

            if (Input.GetMouseButton(0))
            {
                tempPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

                if (tempPos != beginPos)
                {
                    if (tempPos > beginPos)
                    {
                        upPos++;

                    }
                    else
                    {
                        upPos--;
                    }
                    beginPos = tempPos;

                    rotionChanager.Invoke(upPos);
                    beginPos = tempPos;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                rotionChanager.Invoke(-1);
                beginPos = tempPos;
            }
        }    
    }
}
