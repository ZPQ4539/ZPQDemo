using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : IState
{
    private PlayerControl playerControl;                      //玩家控制器
    private List<GameObject> BeginPos = new List<GameObject>();  //起始位置


    //进入游戏后的游戏界面
    public GamePanelUI gamepanelUI;   //进入游戏后界面
    public GameObject OverPanel;      //结束界面

    private Canvas canvas;

    private PlayerInit playerInit;                            //玩家数据

    private AudioSource audioSource;
    public override void Start()
    {
        playerInit = PassDate.Instance.playerInit;

        Statename = "MainGameState";
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        gamepanelUI = canvas.transform.Find("GamePanelUI").GetComponent<GamePanelUI>();
        OverPanel = gamepanelUI.transform.Find("OverPanel").gameObject;
        playerControl = GameObject.Find("PlayerControl").GetComponent<PlayerControl>();

        audioSource = playerControl.transform.GetComponent<AudioSource>();

        GameObject temp = GameObject.Find("PosContent");
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            BeginPos.Add(temp.transform.GetChild(i).gameObject);
        }


        gamepanelUI.OpenUI();
        MessManager.Instance.AddListener(0000, OpenOverPanel);
        MessManager.Instance.AddListener(2000, RefreshPass);
    }
    /// <summary>
    /// 重新开始关卡
    /// </summary>
    public void RefreshPass(Notification notification)
    {
        for (int i = 0; i < playerControl.transform.childCount; i++)
        {
            GameObject.Destroy(playerControl.transform.GetChild(i).gameObject);
        }
        playerControl.transform.position = BeginPos[0].transform.position;


        BuildingBase buildingBase = new BuildingBase();
        buildingBase.Modelname = PassDate.Instance.AllRoleModelName[playerInit.chooseNowRoleIndex];
        buildingBase.x = BeginPos[0].transform.position.x;
        buildingBase.y = BeginPos[0].transform.position.y;
        buildingBase.z = BeginPos[0].transform.position.z;

        buildingBase.sx = 1;
        buildingBase.sy = 1;
        buildingBase.sz = 1;

        playerControl.CreatJues(buildingBase);

        playerControl.isGameOver = false;

        gamepanelUI.OpenUI();

        ChanagerMap(playerInit.NowPass);
    }
    public void ChanagerMap(int passID) //更换地图以及关卡
    {
        PassDate.Instance.Clear();
        PassDate.Instance.CreatMap(passID);
        playerControl.Init();            //初始化玩家信息
    }


    public void OpenOverPanel(Notification notification)
    {
        OverPanel.gameObject.SetActive(true);
        gamepanelUI.RefreshShow();

        audioSource.clip = AssetManager.Instance.LoadAudio("Audio/", "Se_UI_LvUp");

        audioSource.Stop();
        audioSource.Play();

    }
    public void CloseOverPanel()
    {
        OverPanel.gameObject.SetActive(false);
    }


    public override void Update()
    {

    }

    public override void End()
    {
        MessManager.Instance.RemoveListener(0000, OpenOverPanel);
        MessManager.Instance.RemoveListener(2000, RefreshPass);
    }
}
