using UnityEngine;
using System.Collections;

// 場景狀態
public class ISceneState
{
	// 狀態名稱
	private string m_StateName = "ISceneState";
	public string StateName
	{
		get{ return m_StateName; }
		set{ m_StateName = value; }
	}
	// 開始
	public virtual void StateBegin()
	{}

	// 結束
	public virtual void StateEnd()
	{
		//释放部分资源
		ResourcesManager.Instance.RemveAll();
	}

	// 更新
	public virtual void StateUpdate()
	{}

	/// <summary>
	/// 过渡场景使用
	/// </summary>
	public virtual void LoadTransitionScene(string NextSceneName)
    {
		//如果有条件可以在子类中重写该方法
		SceneStateController.Instance.SetState(NextSceneName);
    }

}
