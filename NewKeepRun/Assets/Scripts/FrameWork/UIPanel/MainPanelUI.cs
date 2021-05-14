using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MoveType
{
    Null,
    Left,
    Right,

}

public class MainPanelUI : UIbase
{
    public Transform contentPass;    //关卡显示    
    public Transform LeftorRight;

    public Text money;               //玩家金钱


    public Button btn_left;          //向左切换角色
    public Button btn_right;         //向右切换角色

    public Button btn_begin;         //开始游戏
    public Button btn_Shop;          //商城



    private PlayerInit playerInit;       //玩家数据
    private PlayerControl playerControl; //玩家控制类 


    private List<GameObject> AllRole = new List<GameObject>();   //所有人物
    private List<GameObject> beginPos = new List<GameObject>();  //切换位置

    private MoveType moveType;                 //是否在切换中

    private Animator mainAnim;                 //主角动画
    private Animator UpOrDownAnim;             //切换角色动画

    private GameObject NowRole = null;         //主角
    private GameObject UpOrDownPrefab = null;  //切换的角色



    public Action Beginaction;                 //点击开始按钮执行方法
    public Action OpenShop;                   //点击打开商城
 
    public void SetPlayerInit(PlayerInit playerInit, PlayerControl playerControl,List<GameObject> BeginPos)
    {
        this.playerInit = playerInit;
        this.playerControl = playerControl;
        this.beginPos = BeginPos;

        if (contentPass == null)
        {
            contentPass = transform.Find("NowPass").transform;
        }
        if (LeftorRight == null)
        {
            LeftorRight = transform.Find("RoleShowImage").transform;
        }

        //更新显示
        RefreshInit();
        AllRole.Clear();

        for (int i = 0; i < playerControl.transform.childCount; i++)
        {
            AllRole.Add(playerControl.transform.GetChild(i).gameObject);
        }


        btn_left.onClick.AddListener(LeftOnClick);
        btn_right.onClick.AddListener(RightOnClick);
        btn_begin.onClick.AddListener(OnBegin);
        btn_Shop.onClick.AddListener(()=> { OpenShop();});

    }
    public void RefreshInit()
    {
        //关卡显示
        for (int i = 0; i < contentPass.childCount; i++)
        {
            if (i >= PassDate.Instance.AllPass.Count - 1)
            {
                //contentPass.GetChild(i).transform.Find("Text").GetComponent<Text>().text = (i + 1).ToString();
            }
            else
            {
                contentPass.GetChild(i).transform.Find("Text").GetComponent<Text>().text = (playerInit.NowPass + i).ToString();
            }
        }

        money.text = playerInit.money.ToString();  //金钱显示

        for (int i = 1; i < playerInit.MyRole.Count; i++)
        {
            LeftorRight.transform.GetChild(i).gameObject.SetActive(true);
            LeftorRight.transform.GetChild(playerInit.chooseNowRoleIndex).transform.Find("check").gameObject.SetActive(true);
        }

    }
    void LeftOnClick()
    {
        if (playerInit.chooseNowRoleIndex < AllRole.Count - 1 && moveType == MoveType.Null)
        {
            mainAnim = AllRole[playerInit.chooseNowRoleIndex].GetComponent<Animator>();
            NowRole = AllRole[playerInit.chooseNowRoleIndex];

           playerInit.chooseNowRoleIndex++;
            moveType = MoveType.Left;

            UpOrDownAnim = AllRole[playerInit.chooseNowRoleIndex].GetComponent<Animator>();
            UpOrDownPrefab = AllRole[playerInit.chooseNowRoleIndex];


            mainAnim.SetBool("Run", true);
            UpOrDownAnim.SetBool("Run", true);

          
            NowRole.transform.LookAt(beginPos[2].transform.position);
            UpOrDownPrefab.transform.LookAt(NowRole.transform);
        }
    }
    void RightOnClick()
    {
        if (playerInit.chooseNowRoleIndex > 0 && moveType == MoveType.Null)
        {

            mainAnim = AllRole[playerInit.chooseNowRoleIndex].GetComponent<Animator>();
            NowRole = AllRole[playerInit.chooseNowRoleIndex];

            playerInit.chooseNowRoleIndex--;
            moveType = MoveType.Right;

            UpOrDownAnim = AllRole[playerInit.chooseNowRoleIndex].GetComponent<Animator>();
            UpOrDownPrefab = AllRole[playerInit.chooseNowRoleIndex];

            mainAnim.SetBool("Run", true);
            UpOrDownAnim.SetBool("Run", true);

            NowRole.transform.LookAt(beginPos[1].transform.position);
            UpOrDownPrefab.transform.LookAt(NowRole.transform);
        }
    }
    void Move()
    {
        switch (moveType)
        {
            case MoveType.Left:
                MoveLeft();
                break;
            case MoveType.Right:
                MoveRight();
                break;
            case MoveType.Null:
                break;
            default:
                break;
        }
    }
    void MoveLeft()
    {
        NowRole.transform.position = Vector3.MoveTowards(NowRole.transform.position, beginPos[2].transform.position, Time.deltaTime * 3);
        UpOrDownPrefab.transform.position = Vector3.MoveTowards(UpOrDownPrefab.transform.position, beginPos[0].transform.position, Time.deltaTime * 3);

        if (Vector3.Distance(UpOrDownPrefab.transform.position, beginPos[0].transform.position) <= 0.1f)
        {
            UpOrDownPrefab.transform.rotation = beginPos[0].transform.rotation;
            UpOrDownPrefab.transform.position = beginPos[0].transform.position;

            NowRole.transform.position = beginPos[2].transform.position;

            moveType = MoveType.Null;

            mainAnim.SetBool("Run", false);
            UpOrDownAnim.SetBool("Run", false);
            NowRole = UpOrDownPrefab;  //交互选择的人物
        }
    }
    void MoveRight()
    {
        NowRole.transform.position = Vector3.MoveTowards(NowRole.transform.position, beginPos[1].transform.position, Time.deltaTime * 3);
        UpOrDownPrefab.transform.position = Vector3.MoveTowards(UpOrDownPrefab.transform.position, beginPos[0].transform.position, Time.deltaTime * 3);

        if (Vector3.Distance(UpOrDownPrefab.transform.position, beginPos[0].transform.position) <= 0.1f)
        {
            UpOrDownPrefab.transform.rotation = beginPos[0].transform.rotation;
            UpOrDownPrefab.transform.position = beginPos[0].transform.position;

            NowRole.transform.position = beginPos[1].transform.position;

            moveType = MoveType.Null;

            mainAnim.SetBool("Run", false);
            UpOrDownAnim.SetBool("Run", false);


            NowRole = UpOrDownPrefab;  //交互选择的人物
        }
    }

    void OnBegin()
    {
        Beginaction();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
