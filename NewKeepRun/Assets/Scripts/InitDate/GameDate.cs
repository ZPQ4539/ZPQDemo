using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;


/// <summary>
/// 玩家数据
/// </summary>
public class PlayerInit
{
    public int NowPass;                                        //当前关卡
    public int money;                                          //金钱
    public string chooseNowRole;                               //选择的当前角色
    public int chooseNowRoleIndex;                             //选择的Id  0 为默认
    public Dictionary<int, string> MyRole = new Dictionary<int, string>();


    public void AddMySelf(int key, string name)
    {
        MyRole.Add(key, name);
    }

}

/// <summary>
/// 关卡数据
/// </summary>
public class PassInit
{
    public int id;                                             //关卡ID

    public PassType passType;                                  //关卡类型
    public int award;                                          //通关奖励

    public List<Barrier> barriers = new List<Barrier>();        //障碍物
    public List<Drink> drinks = new List<Drink>();              //饮品
    public List<Floor> floors = new List<Floor>();              //地板
    public bool isShow = false;                                 //是否显示

}
/// <summary>
/// 地板
/// </summary>
public class Floor : BuildingBase
{

}
/// <summary>
/// 障碍物
/// </summary>
public class Barrier : BuildingBase
{
    public BarrierType barriertype;                                  //类型
}
/// <summary>
/// 饮料
/// </summary>
public class Drink : BuildingBase
{
    public DrinkType drinkType;                               //类型
}
/// <summary>
/// 基类
/// </summary>
public class BuildingBase
{
    public int ID;                                  //ID

    public string Modelname;                        //模型名称

    public float x;                                 //位置
    public float y;
    public float z;

    public float sx;                                //大小
    public float sy;
    public float sz;

    public float zoffect;                           //偏移量
    public float yoffect;
}


/// <summary>
/// 关卡类型
/// </summary>
public enum PassType
{
    Null,                                                     //无
    Run,                                                      //表现分模式
    Home                                                      //醉酒模式
}
/// <summary>
/// 饮品类型
/// </summary>
public enum DrinkType
{
    酒,
    饮料,
    金币,
}
/// <summary>
/// 障碍物类型
/// </summary>
public enum BarrierType
{
    障碍物,
    家,
    床,
}

/// <summary>
/// 所有关卡
/// </summary>
public class PassDate : Singletion<PassDate>
{
    public Dictionary<int, PassInit> AllPass = new Dictionary<int, PassInit>();   //所有关卡数据
    public PassInit passInit;                                                     //当前关卡
    public List<string> AllRoleModelName = new List<string>();                    //所有的角色
    public PlayerInit playerInit;                                                 //玩家信息

    //实体
    public List<DrinkPlayer> AllDrinkPlayer = new List<DrinkPlayer>();         //所有的饮品金币
    public List<BarrierPlayer> AllBarrierPlayers = new List<BarrierPlayer>();  //所有墙体以及家或者 床


    public GameObject Floors = null;
    public GameObject DrinkContents = null;
    public GameObject WallContents = null;

    public void Add(int id, PassInit map)
    {
        if (!AllPass.ContainsKey(id))
        {
            AllPass.Add(id, map);
        }
    }
    public void Remove(int id)
    {
        if (!AllPass.ContainsKey(id))
        {
            AllPass.Remove(id);
        }
    }
    public PassInit Find(int id)
    {
        if (AllPass.ContainsKey(id))
        {
            return AllPass[id];
        }
        return null;
    }

    /// <summary>
    /// 游戏模式下 读取
    /// </summary>
    public void PlayerRead()
    {
        Clear();
        InitMap();

        TextAsset temp = Resources.Load<TextAsset>("Plugins/Pass");
        if (temp != null)
        {
            string tempJson = temp.text;
            AllPass = JsonConvert.DeserializeObject<Dictionary<int, PassInit>>(tempJson);
        }

        if (AllPass.Count > 0)
        {
            //  Debug.LogError("加载配置表完成");
        }
    }
    /// <summary>
    /// 创建地图
    /// </summary>
    /// <param name="PassId"></param>
    public void CreatMap(int PassId)
    {
        Clear();
        InitMap();

        passInit = Find(PassId);
        if (passInit != null)
        {
            foreach (var item in passInit.floors)
            {
                GameObject temp = GameObject.Instantiate(AssetManager.Instance.Load("model/", item.Modelname), Floors.transform, false);
                temp.transform.name = item.Modelname;
                SetPos(temp, item.x, item.y, item.z, item.sx, item.sy, item.sz);
            }
            foreach (var item in passInit.drinks)
            {
                DrinkPlayer drink = new DrinkPlayer(item);
                drink.CreatPrefab(DrinkContents.transform);
                AllDrinkPlayer.Add(drink);
            }
            foreach (var item in passInit.barriers)
            {
                BarrierPlayer barrier = new BarrierPlayer(item);
                barrier.CreatPrefab(WallContents.transform);
                AllBarrierPlayers.Add(barrier);
            }
        }
        //  Debug.LogError("地图配置完成");
    }
    /// <summary>
    /// 设置 位置
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="sx"></param>
    /// <param name="sy"></param>
    /// <param name="sz"></param>
    public void SetPos(GameObject temp, float x, float y, float z, float sx, float sy, float sz)
    {
        temp.transform.position = new Vector3(x, y, z);
        temp.transform.localScale = new Vector3(sx, sy, sz);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void InitMap()
    {
        Floors = GameObject.Find("Floors");
        if (Floors == null)
        {
            Floors = new GameObject();
            Floors.name = "Floors";
        }

        WallContents = GameObject.Find("WallContents");
        if (WallContents == null)
        {
            WallContents = new GameObject();
            WallContents.name = "WallContents";
        }


        DrinkContents = GameObject.Find("DrinkContents");
        if (DrinkContents == null)
        {
            DrinkContents = new GameObject();
            DrinkContents.name = "DrinkContents";
        }
    }
    /// <summary>
    /// 清空操作
    /// </summary>
    public void Clear()
    {
        AllDrinkPlayer.Clear();
        AllBarrierPlayers.Clear();

        if (Floors != null)
        {
            for (int i = 0; i < Floors.transform.childCount; i++)
            {
                GameObject.Destroy(Floors.transform.GetChild(i).gameObject);
            }
        }
        if (DrinkContents != null)
        {
            for (int i = 0; i < DrinkContents.transform.childCount; i++)
            {
                GameObject.Destroy(DrinkContents.transform.GetChild(i).gameObject);
            }
        }
        if (WallContents != null)
        {
            for (int i = 0; i < WallContents.transform.childCount; i++)
            {
                GameObject.Destroy(WallContents.transform.GetChild(i).gameObject);
            }
        }
        passInit = null;
    }
}

/// <summary>
/// 商城的数据
/// </summary>
public class ShowDate : Singletion<ShowDate>
{
    public List<Shopitem> ShopItems = new List<Shopitem>();   //商城中有的

    public void ShopInit()
    {
        if (ShopItems.Count > 0)
        {
            return;
        }

        Shopitem temp = new Shopitem(1, "player1", false, "player1_icon", 500);
        ShopItems.Add(temp);

        temp = new Shopitem(2, "player2", false, "player2_icon", 1000);
        ShopItems.Add(temp);

        temp = new Shopitem(3, "player3", false, "player3_icon", 1200);
        ShopItems.Add(temp);

        temp = new Shopitem(4, "player4", false, "player4_icon", 1500);
        ShopItems.Add(temp);

        temp = new Shopitem(5, "player5", false, "player5_icon", 1800);
        ShopItems.Add(temp);

        temp = new Shopitem(6, "player6", false, "player6_icon", 2000);
        ShopItems.Add(temp);

        temp = new Shopitem(7, "player7", false, "player7_icon", 2500);
        ShopItems.Add(temp);

        temp = new Shopitem(8, "player8", false, "player8_icon", 2500);
        ShopItems.Add(temp);

        temp = new Shopitem(9, "player9", false, "player9_icon", 2500);
        ShopItems.Add(temp);

        temp = new Shopitem(10, "player10", false, "player10_icon", 3500);
        ShopItems.Add(temp);


        IsSellShop();
    }
    /// <summary>
    /// 检测是否以及出售该物品了
    /// </summary>
    public void IsSellShop()
    {
        Dictionary<int, string> temp = PassDate.Instance.playerInit.MyRole;

        foreach (var item in ShopItems)
        {
            if (temp.ContainsKey(item.id))
            {
                item.isbool = true;
            }
        }

    }



    public GameObject BuyCreat(string name)
    {
        return null;
    }
}
public class Shopitem
{
    public int id;       //商城ID
    public string nameID; //商城名称
    public bool isbool; //是否出售
    public string icon; //图片
    public int price;   //价格

    public Shopitem(int id, string name, bool isbool, string icon, int price)
    {
        this.id = id;
        this.nameID = name;
        this.isbool = isbool;
        this.icon = icon;
        this.price = price;
    }
}


