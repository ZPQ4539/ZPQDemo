using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 兵营系统
/// </summary>
public class CampSystem : IGameSystem
{
    //所有兵营的游戏物体
    public List<GameObject> AllCampGame = new List<GameObject>();
    //所有的兵营
    public List<ICamp> campsList = new List<ICamp>();
    //当前要创建的兵营
    private GameObject campNow;
    //射线位置
    RaycastHit hit;

    public CampSystem(PBDG_GameMain pBDG_Game):base(pBDG_Game)
    {
        //数据可以是在配置表中取

        //兵营
        Camp camp1 = new Camp("Camp", 5, 2000, "Camp", "RookieCamp", false, "工兵");
        Camp camp2 = new Camp("Camp", 5, 2000, "Camp", "RookieCamp", false, "坦克");
        //所有数据先初始化
        campsList.Add(camp1);
        campsList.Add(camp2);

        for (int i = 0; i < 10; i++)
        {
            IAssetFactory factory = PBDGFactory.GetAssetFactory();
            GameObject temp  = factory.CreatBuding("CaptiveCamp");
            temp.transform.position = new Vector3(Random.Range(-50, 50), 3.7f, Random.Range(-50, 50));
            pBDG_Game.Add(temp, "建筑物");
        }
    }


    /// <summary>
    /// 建造兵营
    /// </summary>
    public void CreatBuding(string name)
    {
        if (campNow != null)
        {
            return;
        }
        IAssetFactory factory = PBDGFactory.GetAssetFactory();
        campNow = factory.CreatBuding(name);
       // m_PBDGame.Add(campNow, "建筑物");
    }



    //初始化
    public override void Initialize()
    {

    }
    //释放
    public override void Release()
    {

    }

    public override void Update()
    {
        FollowMouse();
        OnClickMouse();
    }
    //建造物跟随鼠标
    private void FollowMouse()
    {
        if (campNow == null)
        {
            return;
        }
        campNow.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,20));
    }
    /// <summary>
    /// 创建出来点击后放置到地面上
    /// </summary>
    private void OnClickMouse()
    {
        if (campNow != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray,out hit))
                {
                    bool isFlag =  isDown(hit.point);
                    if (isFlag)
                    {
                        campNow.transform.position = hit.point;

                        //查找对应的兵营
                        ICamp camp = FindCamp(campNow.transform.name);
                        //设置游戏物体
                        camp.SetGame(campNow);
                        camp.SetIsDown(true);


                        AllCampGame.Add(campNow);
                        //刷新显示
                        m_PBDGame.RefreshShowCamp();
                        //当前建筑物已经抵达地面
                        campNow = null;
                    }
                    else
                    {
                        Debug.LogError("目前位置不可以建造");
                    }
                   
                }
            }
        }
        else
        {
            return;
        }
    }

    private ICamp FindCamp(string name)
    {
        foreach (var item in campsList)
        {
            if (item.GetName() == name)
            {
                return item;
            }
        }
        return null;
    }
    /// <summary>
    /// 可以建造范围
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private bool isDown(Vector3 pos)
    {
        foreach (var item in m_PBDGame.AllBuding["建筑物"])
        {
            if (Vector3.Distance(item.transform.position,pos)<= 4)
            {
                return false;
            }
        }
        return true;
    }
    //获取兵营数据
    public List<ICamp> GetCampInit()
    {
        return campsList;
    }

    public ICamp GetCamp(string name)
    {
        switch (name)
        {
            case "工兵":
                return FindCamporType(name);
            case "坦克":
                return FindCamporType(name);
            default:
                break;
        }
        return null;
    }

    private ICamp FindCamporType(string name)
    {
        foreach (var item in campsList)
        {
            if (item.GetCampType() == name)
            {
                return item;
            }
        }
        return null;
    }
}
