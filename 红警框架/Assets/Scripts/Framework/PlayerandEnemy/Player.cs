using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家接口
/// </summary>
public class Player : IPlayer
{
    private int m_Moeny = 1000;

    public Player(string yourname,string country,Color color):base(yourname, country,color)
    {

    }

    public override void Set(string yourname, string country,Color color)
    {
        this.yourname = yourname;
        this.yourcountry = country;
        this.color = color;
    }

    public int GetMoney()
    {
        return m_Moeny;
    }

    public void SetMoeny(int money)
    {
        m_Moeny -= money;
        PBDG_GameMain.Instance.SetText(m_Moeny);
    }

    public void AddMoney(int money)
    {
        m_Moeny += money;
        PBDG_GameMain.Instance.SetText(m_Moeny);
    }
}
