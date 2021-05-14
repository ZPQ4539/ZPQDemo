using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色命令
/// </summary>
public class SoliderCommand : ICommend
{
    //所有创建出来的角色
     List<ISolider> soliders = new List<ISolider>();

    private Vector3 m_Position;  //出现位置
    public SoliderCommand(Vector3 Position)
    {
        this.m_Position = Position;
    }
    public override void Execute()
    {
        IBuidingFactory Factory = PBDGFactory.GetBuidingFactory();
        ISolider solider = Factory.CreatSolider(m_Position);

      
       // PBDG_GameMain.Instance.AddSolider(solider);
    }
}
