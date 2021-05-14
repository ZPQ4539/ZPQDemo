using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI 状态界面
/// </summary>
public class MainState : IState
{
    private PlayerControl playerControl;                           //玩家控制器
    public BuildingBase chooseRoleInit;                            //选择的人物数据

    private Canvas canvas;                                        //Canvas
    public LoadPanelUI loadUI;                                    //加载界面
    public MainPanelUI mainUI;                                    //主界面
    public ShopPanelUI shopUI;                                    //商城
    public TipsPanelUI tipsPanel;                                 //tips


    private PlayerInit playerInit;                               //玩家数据

    private List<GameObject> BeginPos = new List<GameObject>();  //起始位置

    private GameObject shopShow;  //商品舞台
    private Dictionary<int,GameObject> shopShowList =  new Dictionary<int, GameObject>(); //所有模型
    private GameObject shopShowPrefab; //当前舞台模型


    private Dictionary<int, GameObject> tempPrefabRole = new Dictionary<int, GameObject>();

    public void StratEnd()
    {
        shopShowPrefab = null;
        tempPrefabRole.Clear();
        shopShowList.Clear();
    }


    public override void Start()
    {
        StratEnd();
        

        Statename = "MainState";
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        playerControl = GameObject.Find("PlayerControl").GetComponent<PlayerControl>();

        shopShow = GameObject.Find("ShopShowPos");
        shopShow.gameObject.SetActive(false);

        if (playerInit == null)            //人物数据初始化
        {
            //string temp = PlayerPrefs.GetString("playerInit");
            string temp = "";

            if (temp.Length <= 0)
            {

                playerInit = new PlayerInit();
                playerInit.AddMySelf(0,"player");     //添加我拥有的角色

                playerInit.money = 500;               //当前的金钱      
                playerInit.NowPass = 1;             //当前关卡
                playerInit.chooseNowRoleIndex = 0;  //选择的人物ID

            }
            else
            {
                string[] AllInit = temp.Split('|');

                playerInit = new PlayerInit();
                playerInit.money = int.Parse(AllInit[0]);
                playerInit.NowPass = int.Parse(AllInit[1]);
                playerInit.chooseNowRoleIndex = int.Parse(AllInit[2]);

                for (int i = 3; i < AllInit.Length; i++)
                {
                    playerInit.AddMySelf(int.Parse(AllInit[i].Split(':')[0]), AllInit[i].Split(':')[1]);
                }
            }
            PassDate.Instance.playerInit = playerInit;
        }
        ShowDate.Instance.ShopInit();  //商城数据初始化


        if (loadUI == null)
        {
            loadUI = canvas.transform.Find("LoadPanelUI").GetComponent<LoadPanelUI>();
        }
        if (mainUI == null)
        {
            mainUI = canvas.transform.Find("MainPanelUI").GetComponent<MainPanelUI>();
        }
        if (shopUI == null)
        {
            shopUI = canvas.transform.Find("ShopPanelUI").GetComponent<ShopPanelUI>();
        }
        if (tipsPanel == null)
        {
            tipsPanel = canvas.transform.Find("TipsPanelUI").GetComponent<TipsPanelUI>();
        }

        loadUI.isPlay = true;                                            //可以允许开始游戏

        loadUI.LoadEnd = LoadMainScene;                                  //加载结束后执行方法

        shopUI.closeAction = CloseShop;                                  //添加商城关闭方法
        shopUI.rotionChanager = RotionChanager;                          //旋转
        shopUI.chanagerRole = ChanagerShopItem;                          //切换显示的道具
        shopUI.BuyShop = BuyShop;                                        //购买


        mainUI.Beginaction = onBegin;                                    //点击开始游戏后执行的方法
        mainUI.OpenShop = OpenShop;                                      //设置点击事件



        ChanagerMap(0);                                 //创建显示地图
        CreatRoleInit();                                //创建已经角色

        mainUI.SetPlayerInit(playerInit, playerControl, BeginPos);        //给ui 设置属性
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    public void onBegin()
    {
        for (int i = 0; i < playerControl.transform.childCount; i++)
        {
             GameObject.Destroy(playerControl.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < PassDate.Instance.Floors.transform.childCount; i++)
        {
            GameObject.Destroy(PassDate.Instance.Floors.transform.GetChild(i).gameObject);
        }

        mainUI.CloseUI();
        loadUI.OpenUI();

        loadUI.LoadEnd = LoadGameScene;
    }

    public void ChanagerMap(int passID) //更换地图以及关卡
    {
        PassDate.Instance.CreatMap(passID);  //创建地图
        playerControl.Init();                //初始化玩家信息

        BeginPos.Clear();
        GameObject temp = GameObject.Find("PosContent");

        for (int i = 0; i < temp.transform.childCount; i++)
        {
            BeginPos.Add(temp.transform.GetChild(i).gameObject);
        }


        //重置
        playerControl.isGameOver = false;    //游戏是否结束
        playerControl.isPlay = false;        //是否可以开始游戏 

        playerControl.isSpeed = 5;           //速度回归

        playerControl.transform.position = BeginPos[0].transform.position;   //控制器回归

        MessManager.Instance.BroadCast(1000, new Notification(0, true));           //相机回归
    }

    /// <summary>
    /// 经过Load 后加载关卡
    /// </summary>
    public void LoadGameScene()
    {
        loadUI.CloseUI();

        ChanagerMap(playerInit.NowPass);

        BuildingBase buildingBase = new BuildingBase();
        buildingBase.Modelname = playerInit.MyRole[playerInit.chooseNowRoleIndex]; 
        buildingBase.x = BeginPos[0].transform.position.x;
        buildingBase.y = BeginPos[0].transform.position.y;
        buildingBase.z = BeginPos[0].transform.position.z;

        buildingBase.sx = 1;
        buildingBase.sy = 1;
        buildingBase.sz = 1;

        playerControl.CreatJues(buildingBase);

        StateContorl.Instance.LoadNewState<MainGameState>("MainGameState");
    }
    public void LoadMainScene()
    {
        loadUI.CloseUI();
        mainUI.OpenUI();
    }


    /// <summary>
    /// 根据数据 创建不同的角色
    /// </summary>
    public void CreatRoleInit()
    {

        Dictionary<int,string> RoleName = playerInit.MyRole;  //创建我拥有的

   

        foreach (var item in RoleName)
        {
            if (tempPrefabRole.ContainsKey(item.Key))
            { 
                continue;
            }

            if (item.Key == playerInit.chooseNowRoleIndex)
            {
                CreatRole(item.Key,item.Value,playerControl.transform,BeginPos[0].transform);
            }else if (item.Key < playerInit.chooseNowRoleIndex)
            {
                CreatRole(item.Key,item.Value, playerControl.transform, BeginPos[2].transform);
            }else if (item.Key > playerInit.chooseNowRoleIndex)
            {
                CreatRole(item.Key,item.Value, playerControl.transform, BeginPos[1].transform);
            }
        }
    }
    private void CreatRole(int id,string name,Transform content,Transform pos)
    {
        GameObject tempPreafb = GameObject.Instantiate(AssetManager.Instance.Load("model/", name), playerControl.transform, false);
        tempPreafb.transform.position = pos.position;
        tempPreafb.transform.rotation = pos.rotation;

        tempPrefabRole.Add(id,tempPreafb);
    }

    //打开关闭商城
    public void OpenShop()
    {
        shopUI.OpenUI();
        shopShow.gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        shopUI.CloseUI();
        shopShow.gameObject.SetActive(false);
    }
    //切换商品
    public void ChanagerShopItem(Shopitem shopitem)
    {
        if (shopShowPrefab != null)
        {
            shopShowPrefab.gameObject.SetActive(false);
        }

        if (shopShowList.ContainsKey(shopitem.id))
        {
            shopShowPrefab = shopShowList[shopitem.id];
            shopShowPrefab.SetActive(true);
        }
        else
        {
            
            GameObject temp = GameObject.Instantiate(AssetManager.Instance.Load("model/", shopitem.nameID), shopShow.transform,false);
            temp.transform.position = shopShow.transform.Find("playerPos").transform.position;
            shopShowList.Add(shopitem.id, temp);

            for (int i = 0; i < temp.transform.childCount; i++)
            {
                temp.transform.GetChild(i).gameObject.layer = 5;
                for (int j = 0; j < temp.transform.GetChild(i).transform.childCount; j++)
                {
                    temp.transform.GetChild(i).transform.GetChild(j).gameObject.layer = 5;
                }
            }
            temp.layer = 5;
            shopShowPrefab = temp;
            shopShowPrefab.SetActive(true);
        }
    }

    float upPos = 0;
    float timeSpeed = 3;

    /// <summary>
    /// 商品 旋转
    /// </summary>
    /// <param name="x"></param>
    private void RotionChanager(float x)
    {
        if (x == -1)
        {
            timeSpeed = 3;
            upPos = 0;
            shopShow.gameObject.transform.rotation = new Quaternion(0,180,0,0);
            return;
        }
        timeSpeed -= Time.deltaTime;
        if (x > upPos)
        {
            shopShow.gameObject.transform.Rotate(-Vector3.up * timeSpeed);
        }
        else
        {
            shopShow.gameObject.transform.Rotate(Vector3.up * timeSpeed);
        }
        upPos = x;

    }

    private void BuyShop(Shopitem shopitem)
    {
        if (playerInit.MyRole.ContainsKey(shopitem.id))
        {
            tipsPanel.SetContent("已购买",false);
            tipsPanel.OpenUI();
            return;
        }

        if (playerInit.money >= shopitem.price)
        {
            playerInit.AddMySelf(shopitem.id, shopitem.nameID);
            tipsPanel.SetContent("购买成功", false);
            playerInit.money -= shopitem.price;
            shopitem.isbool = true;
            CreatRoleInit();

            mainUI.SetPlayerInit(playerInit,playerControl,BeginPos);
        }
        else
        {
            tipsPanel.SetContent("购买失败", true);
        }
        tipsPanel.OpenUI();
    }


    public override void Update()
    {

    }

    public override void End()
    {

    }
}
