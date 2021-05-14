using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 命令系统 继承于单利模式
/// </summary>
public class CommandSystem : Singletion<CommandSystem>
{
    //所有命令
    private List<ICommend> AllCommand = new List<ICommend>();
    //当前Cd
    private float m_NowCd = 0;

    /// <summary>
    /// 添加命令
    /// </summary>
    /// <param name="commend"></param>
    public void AddCommand(ICommend commend)
    {
        AllCommand.Add(commend);
    }
    /// <summary>
    /// 移除命令
    /// </summary>
    public void RemoveLasetCommand()
    {
        if (AllCommand.Count == 0)
        {
            return;
        }
        AllCommand.RemoveAt(AllCommand.Count - 1);
    }
    /// <summary>
    /// 执行命令
    /// </summary>
    public void RunCommand()
    {
        if (AllCommand.Count == 0)
        {
            return;
        }
        if (m_NowCd <= 0)
        {
            //执行对应命令
            AllCommand[0].Execute();
            m_NowCd = AllCommand[0].GetCd();
            AllCommand.RemoveAt(0);
            
        }
        else
        {
            m_NowCd -= Time.deltaTime;
        }


       
    }
}
