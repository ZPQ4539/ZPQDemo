using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : ICamp
{
   

    //通过构造函数赋值
    public Camp(string name,int cd,int money,string next,string image,bool isDown, string type) :base(name, cd, money, next,image,isDown,type)
    {

    }

  
    /// <summary>
    /// 生成工兵
    /// </summary>
    public override void Train()
    {
        //生成角色命令执行角色命令
        SoliderCommand soliderCommand = new SoliderCommand(m_Position);
        soliderCommand.SetCd(m_cd);
        //添加命令
        CommandSystem.Instance.AddCommand(soliderCommand);
    }

   
}
