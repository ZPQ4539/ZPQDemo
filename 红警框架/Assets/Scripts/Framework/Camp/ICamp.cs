using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 兵营接口
/// </summary>
public abstract class ICamp
{
    protected string m_name;            //名字
    protected int m_cd;                 //cd
    protected int m_money;              //需要花费钱
    protected string m_next;            //下面串联的营地
    protected string m_image;           //显示的图片
    protected bool m_isDown;            //是否允许下个显示
    protected string m_camptype;        //兵营类型

    public float m_nowcd = 0;           //当前CD


    protected GameObject m_GameObject;                                 //游戏物体
    protected Vector3 m_Position;                                      //生成位置                               
    protected List<ICommend> m_Commend = new List<ICommend>();         //训练命令

    protected ICamp(string name, int cd, int money, string next, string image, bool isDown, string camptype)
    {
        m_name = name;
        m_cd = cd;
        m_money = money;
        m_next = next;
        m_image = image;
        m_isDown = isDown;
        m_camptype = camptype;
    }
    //返回训练金额
    public virtual int GetTrainMoney()
    {
        return m_money;
    }
    //是否安放
    public void SetIsDown(bool isDown)
    {
        this.m_isDown = isDown;
    }
    //获取名称
    public string GetName()
    {
        return m_name;
    }
    //获取图片的名称
    public string GetImage()
    {
        return m_image;
    }
    //是否显示
    public bool GetIsShow()
    {
        return m_isDown;
    }
    //获取下一个显示的营地
    public string GetNextName()
    {
        return m_next;
    }
    //获取兵营的类型
    public string GetCampType()
    {
        return m_camptype;
    }
    /// <summary>
    /// 训练命令执行
    /// </summary>
    public abstract void Train();
    /// <summary>
    /// 设置营地游戏物体
    /// </summary>
    /// <param name="theGame"></param>
    public void SetGame(GameObject theGame)
    {
        this.m_GameObject = theGame;
        m_Position = theGame.transform.Find("pos").transform.position;
    }
}
