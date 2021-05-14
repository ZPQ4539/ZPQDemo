using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 场景状态控制
public class SceneStateController : Singletion<SceneStateController>
{
    /// <summary>
    /// 存放所有可以切换的类型 在一开始进行初始化完毕 
    /// </summary>
    Dictionary<string,ISceneState> SceneStateDic = new Dictionary<string, ISceneState>();
    //添加一个状态,状态类必须继承 ISceneState
    public T AddState<T>(string LoadSceneName) where T:ISceneState,new()
    {
        if (!SceneStateDic.ContainsKey(LoadSceneName))
        {
            SceneStateDic.Add(LoadSceneName,new T());
        }
        return SceneStateDic[LoadSceneName] as T;
    }
    //移除一个状态
    public void RemoveState(string LoadSceneName)
    {
        if (SceneStateDic.ContainsKey(LoadSceneName))
        {
            SceneStateDic.Remove(LoadSceneName);
        }
    }
    //查找的方法 如果不存在则自动添加一个状态，进行返回 
    public T FindState<T>(string LoadSceneName) where T : ISceneState,new()
    {
        if (SceneStateDic.ContainsKey(LoadSceneName))
        {
            return SceneStateDic[LoadSceneName] as T;
        }
        return AddState<T>(LoadSceneName);
    }
    //查找的方法 如果不存在则返回空，进行返回 
    public ISceneState FindState(string LoadSceneName)
    {
        if (SceneStateDic.ContainsKey(LoadSceneName))
        {
            return SceneStateDic[LoadSceneName];
        }
        return null;
    }



    private ISceneState m_State;
    //是否完成加载场景
    private bool m_bRunBegin = false;
    //异步加载场景
    private  AsyncOperation operation = null;
    //加载的场景
    private string m_NextSceneName;
    //加载过度场景的名称
    private string m_LoadSceneName;


    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="LoadSceneName"></param>
    /// <param name="isLoadScene"></param>
    public void SetState(string LoadSceneName,bool isLoadScene = false)
    {
        m_bRunBegin = false;
        //如果上一次的状态还存在 则先释放
        if (m_State != null)
        {
            m_State.StateEnd();
        }
        //先保存一下要切换到那个场景
        m_NextSceneName = LoadSceneName;
        //判断是否要进行加载过渡
        if (isLoadScene)
        {
            LoadScene(m_LoadSceneName);
            m_State = FindState(m_LoadSceneName);
            if (m_State !=  null)
            {
                m_State.LoadTransitionScene(m_NextSceneName);
            }
        }
        else
        {
            LoadScene(m_NextSceneName);
            m_State = FindState(m_NextSceneName);
        }

        if (m_State == null)
        {
            Debug.LogError("切换状态失败");
        }
    }
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="LoadSceneName"></param>
    private void LoadScene(string LoadSceneName)
    {
        operation = SceneManager.LoadSceneAsync(LoadSceneName);
    }
    /// <summary>
    /// 更新
    /// </summary>
    public void StateUpdate()
    {
        if (operation != null && operation.isDone == false)
        {
            return;
        }

        if (operation != null)
        {
            if (operation.isDone && m_bRunBegin == false)
            {
                m_State.StateBegin();
                m_bRunBegin = true;
            }
        }
        if (m_State != null)
        {
            m_State.StateUpdate();
        }
    }
    /// <summary>
    /// 设置过渡场景状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="LoadSceneName"></param>
    /// <returns></returns>
    public T SetLoadScene<T>(string LoadSceneName) where T:ISceneState,new()
    {
        ISceneState temp = FindState(LoadSceneName);
        if (temp == null)
        {
            temp = AddState<T>(LoadSceneName);
        }
        m_LoadSceneName = LoadSceneName;
        return temp as T;
    }
}
