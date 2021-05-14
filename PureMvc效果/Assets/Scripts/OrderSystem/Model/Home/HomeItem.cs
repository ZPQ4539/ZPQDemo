using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum H_homeState
{
    WaitClient = 0,  //等待顾客住房
    NowSleep = 1,    //正在住房
    BackHouse = 2    //退房
}

public class HomeItem
{
    //客房 Id;
    public int id { get; set; }
    //客房类型
    public string HomeType { get; set; }
    //状态
    public int state { get; set; }

    public HomeItem(int id,string HomeType,int state)
    {
        this.id = id;
        this.HomeType = HomeType;
        this.state = state;
    }

    public override string ToString()
    {
        return id + "号" + HomeType + returnState(state);
    }

    private string returnState(int state)
    {
        if (state.Equals(0))
        {
            return "等待顾客住房";
        }
        if (state.Equals(1))
        {
            return "顾客正在住房";
        }
        if (state.Equals(2))
        {
            return "退房";
        }
        return null;
    }
}
