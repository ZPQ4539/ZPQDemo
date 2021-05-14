using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 外观模式
/// </summary>
/// 
public enum SceneType
{
    gameType,
    UIType
}

public class PBDG_GameMain : Singletion<PBDG_GameMain>
{
    //所有的物体
    public Dictionary<string, List<GameObject>> AllBuding = new Dictionary<string, List<GameObject>>();
    //所有选中的游戏物体
    public List<IBuding> AllChooseBuding = new List<IBuding>();

    public SceneType sceneType;

    public void Add(GameObject temp, string name)
    {
        if (AllBuding.ContainsKey(name))
        {
            AllBuding[name].Add(temp);
        }
        else
        {
            AllBuding.Add(name, new List<GameObject>() { temp });
        }

    }

    public void Remove(string name, GameObject game)
    {
        if (AllBuding.Count <= 0)
        {
            return;
        }
        if (AllBuding.ContainsKey(name))
        {
            foreach (var item in AllBuding[name])
            {
                if (item == game)
                {
                    AllBuding[name].Remove(item);
                    break;
                }
            }
            //if (AllBuding[name].Count <=  0)
            //{
            //    AllBuding.Remove(name);
            //}
        }
        m_player.AddMoney(50);
    }




    #region 系统

    //Tips 系统
    private TipsSystem m_TipsSystem = null;
    //切换UI状态系统 
    private SwitchSystem m_SwitchSystem = null;
    //兵营系统
    private CampSystem m_campSystem = null;
    //工兵系统
    private ArsenalSystem m_arsenalSystem = null;


    #endregion

    #region  界面UI

    private TipsPanel m_tipsPanel = null;                                //tipsUI
    private MainPanel m_mainPanel = null;                                //主界面UI
    private MainMenuPanel m_mainMenuPanel = null;                        //主菜单UI
    private MainMenuRightPanel m_mainMenuRightPanel = null;              //主菜单侧边栏UI
    private ReadAssetPanel m_readAssetPanel = null;                      //读档菜单UI
    private ReadAssetRightPanel m_readAssetRightPanel = null;            //读档菜单侧边栏UI
    private ChooseRolePanel m_ChooseRolePanel = null;                    //选择人物菜单UI
    private GameMenuRightPanel m_gameMenuRightPanel = null;              //游戏侧边栏UI
    private GameStatePanel m_statePanel = null;                          //游戏主菜单UI

    #endregion

    //主玩家
    public Player m_player = null;


    /// <summary>
    /// 初始化
    /// </summary>
    public void Initinal()
    {
        //辅助系统
        m_TipsSystem = new TipsSystem(this);
        m_SwitchSystem = new SwitchSystem(this);
        m_arsenalSystem = new ArsenalSystem(this);
        //界面
        m_tipsPanel = new TipsPanel();




        m_TipsSystem.Initialize();
        m_arsenalSystem.Initialize();


        //第一次打开才进行加载以下界面
        if (m_player == null)
        {
            m_mainPanel = new MainPanel(this);
            m_mainMenuPanel = new MainMenuPanel(this);
            m_mainMenuRightPanel = new MainMenuRightPanel(this);
            m_readAssetPanel = new ReadAssetPanel(this);
            m_readAssetRightPanel = new ReadAssetRightPanel(this);
            m_ChooseRolePanel = new ChooseRolePanel(this);
            m_gameMenuRightPanel = new GameMenuRightPanel(this);

            //界面状态系统初始化
            m_SwitchSystem.Initialize();
            //注册界面状态
            AddUIState();

            //主界面初始化
            m_mainPanel.Begin();
        }
        else
        {
            //进入游戏后加载兵营系统 以及  UI
            m_campSystem = new CampSystem(this);
            m_statePanel = new GameStatePanel(this);
            //初始化
            m_campSystem.Initialize();
            m_statePanel.Begin();
        }

    }

    // 釋放遊戲系統
    public void Release()
    {
        //系统释放
        m_TipsSystem.Release();

        //界面释放
        m_tipsPanel.End();
        m_arsenalSystem.Release();

        if (m_player == null)
        {
            m_SwitchSystem.Release();
            m_mainPanel.End();
        }
        else
        {
            if (m_statePanel != null && m_campSystem != null)
            {
                m_campSystem.Release();
                m_statePanel.End();
            }
        }

        //if (m_player != null)
        //{
        //    m_mainPanel = null;
        //    m_mainMenuPanel = null;
        //    m_mainMenuRightPanel = null;
        //    m_readAssetPanel = null;
        //    m_readAssetRightPanel = null;
        //    m_ChooseRolePanel = null;
        //    m_gameMenuRightPanel = null;
        //}
        AllBuding.Clear();
        AllChooseBuding.Clear();

    }
    public float GameTime = 200;
    // 更新
    public void Update()
    {
        //系统更新
        m_TipsSystem.Update();
        //UI更新
        m_tipsPanel.Update();

        if (AllBuding.ContainsKey("建筑物") && m_player != null && sceneType == SceneType.gameType)
        {
            if (AllBuding["建筑物"].Count <= 0)
            {
                m_player = null;
                UIManager.Instance.OpenUI<TipsControl>("TipsPanel");
                //OpenTipsUI("游戏胜利");
                return;
            }
            if (m_arsenalSystem.AllSolider.Count <= 0 && m_player.GetMoney() <= 0)
            {
                m_player = null;
                UIManager.Instance.OpenUI<TipsControl>("TipsPanel");
                //OpenTipsUI("你输了,破产了呀！ 再来一次玩一次吧 快来玩啊大爷");
                return;
            }
            GameTime -= Time.deltaTime;
            if (GameTime <= 0)
            {
                m_player = null;
                UIManager.Instance.OpenUI<TipsControl>("TipsPanel");
               // OpenTipsUI("你输了,破产了呀！ 再来一次玩一次吧 快来玩啊大爷");
                return;
            }

        }


       
        m_arsenalSystem.Update();

        //获取玩家输入
        GetPlayerInput();

        if (m_player == null)
        {
            m_SwitchSystem.Update();
            m_mainPanel.Update();
        }
        else
        {
            if (m_statePanel != null)
            {
                CommandSystem.Instance.RunCommand();
                m_campSystem.Update();
                m_statePanel.Update();
            }
        }

       


    }

    #region Tips

    /// <summary>
    /// 通过此方法通知tips系统 找到提示对应消息
    /// </summary>
    /// <param name="Id"></param>
    public void SetContent(string Id)
    {
        m_TipsSystem.Notification(Id);
    }
    /// <summary>
    /// 通过此方法通知UI 进行更新显示  tips
    /// </summary>
    /// <param name="content"></param>
    public void OpenTipsUI(string content)
    {
        //m_tipsPanel.SettipsText(content);
        //m_tipsPanel.OpenUIPanel();
    }

    #endregion
    #region UI状态切换

    /// <summary>
    /// 初始化UI部分状态
    /// </summary>
    public void AddUIState()
    {
        m_SwitchSystem.AddUIPanelState(UIPanelState.MainMenu, m_mainMenuPanel);
        m_SwitchSystem.AddUIPanelState(UIPanelState.MainMenu, m_mainMenuRightPanel);
        m_SwitchSystem.AddUIPanelState(UIPanelState.ChooseAseet, m_readAssetPanel);
        m_SwitchSystem.AddUIPanelState(UIPanelState.ChooseAseet, m_readAssetRightPanel);
        m_SwitchSystem.AddUIPanelState(UIPanelState.ChooseMenu, m_ChooseRolePanel);
        m_SwitchSystem.AddUIPanelState(UIPanelState.ChooseMenu, m_readAssetRightPanel);
        m_SwitchSystem.AddUIPanelState(UIPanelState.Null, m_gameMenuRightPanel);
    }
    /// <summary>
    /// 获取主控UI界面
    /// </summary>
    public MainPanel GetMainPanel()
    {
        if (m_mainPanel != null)
        {
            return m_mainPanel;
        }
        return null;

    }
    /// <summary>
    /// ui状态显示
    /// </summary>
    /// <param name="iUIPanel"></param>
    /// <param name="StateName"></param>
    public void SetUIStateShow(List<IUIPanel> iUIPanel, string StateName)
    {
        if (m_mainPanel != null)
        {
            m_mainPanel.SetUIShow(iUIPanel, StateName);
        }
    }
    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="uIPanelState"></param>
    public void SetUIState(UIPanelState uIPanelState)
    {
        m_SwitchSystem.SetUIState(uIPanelState);
    }
    /// <summary>
    /// 返回上一级
    /// </summary>
    public void BackUpState()
    {
        m_SwitchSystem.BackUpState();
    }

    #endregion
    #region 进入游戏

    /// <summary>
    /// 设置主要玩家
    /// </summary>
    public void SetPlayer(string yourname, string country, Color color)
    {
        if (m_player == null)
        {
            m_player = new Player(yourname, country, color);
        }
        else
        {
            m_player.Set(yourname, country, color);
        }
    }
    /// <summary>
    /// 加载到游戏场景
    /// </summary>
    public void LoadNewScene()
    {
        if (m_player != null)
        {
           
            SceneStateController.Instance.SetState("GameScene", true);
        }
    }
    #endregion
    #region  获取玩家输入

    private Vector3 mousePositon;
    private GameObject tager;
    private Vector3 formtager;

    /// <summary>
    /// 获取玩家输入
    /// </summary>
    private void GetPlayerInput()
    {
        if (tager == null)
        {
            tager = GameObject.Find("Tager");
            if (tager != null)
            {
                formtager = Camera.main.transform.position - tager.transform.position;
            }

        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                mousePositon = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (mousePositon.x >= 0.6f)
                {
                    tager.transform.Translate(Vector3.right);
                }
                if (mousePositon.x <= 0.1f)
                {
                    tager.transform.Translate(Vector3.left);
                }

                if (mousePositon.y >= 0.9)
                {
                    tager.transform.Translate(Vector3.forward);
                }
                if (mousePositon.y <= 0.1f)
                {
                    tager.transform.Translate(Vector3.back);
                }

                Camera.main.transform.position = formtager + tager.transform.position;
            }
        }
    }


    #endregion

    /// <summary>
    /// 建造建筑物
    /// </summary>
    /// <param name="name"></param>
    public void CreatBuding(string name)
    {
        if (m_campSystem != null)
        {
            m_campSystem.CreatBuding(name);
        }
    }
    /// <summary>
    /// 获取兵营数据进行显示
    /// </summary>
    /// <returns></returns>
    public List<ICamp> GetCampList()
    {
        return m_campSystem.GetCampInit();
    }
    /// <summary>
    /// 兵营数据变化更新显示
    /// </summary>
    public void RefreshShowCamp()
    {
        if (m_statePanel != null)
        {
            m_statePanel.RefreshShow();
        }
    }

    public ICamp GetICamp(string text)
    {
        return m_campSystem.GetCamp(text);
    }


    public void AddSolider(ISolider soliders)
    {
        m_arsenalSystem.AddSolider(soliders);
    }

    /// <summary>
    /// 选框攻击选中的 
    /// </summary>

    public void Choose(List<GameObject> games)
    {
        if (games.Count < 0)
        {
            return;
        }
        foreach (var item in games)
        {
            foreach (var it in m_arsenalSystem.GetSolider())
            {
                if (it.GetGameObject() == item)
                {
                    AllChooseBuding.Add(it);
                }
            }
        }
    }

    public RaycastHit hit;

    public void OnClickMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Move(hit.point);
        }
    }

    public void Move(Vector3 pos)
    {
        foreach (var item in AllChooseBuding)
        {
            IBuilderAI builder = item.GetAI();
            if (builder != null)
            {
                builder.MoveTo(pos);
            }
        }
    }

    public void SetText(int money)
    {
        m_statePanel.SetText(money);
    }

    public void SetTime(float time)
    {
        if (m_statePanel != null)
        {
            m_statePanel.SetTime(time);
        }
     
    }
}
