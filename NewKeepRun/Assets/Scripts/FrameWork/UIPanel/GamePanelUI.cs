using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanelUI : UIbase
{
    private Button btn_Back;       //重新玩本关卡
    private Button btn_Home;       //返回到主场景
    private Button btn_Next;       //下一关
    private Button btn_View;       //看视频双倍
    private Text Text_money;       //过关奖励
    private Image image_advertising; //广告位

    private Text Text_moneyUp;       //吃到的金币

    private MainState mainState;    //主状态

    private Transform DestroyContent;
    private GameObject overPanel;

    int money;

    private void OnEnable()
    {
        overPanel = transform.Find("OverPanel").gameObject;
        btn_Back = transform.Find("OverPanel/btn_back").GetComponent<Button>();
        btn_Home = transform.Find("OverPanel/btn_Home").GetComponent<Button>();
        btn_Next = transform.Find("OverPanel/btn_Next").GetComponent<Button>();
        btn_View = transform.Find("OverPanel/btn_View").GetComponent<Button>();
        Text_money = transform.Find("OverPanel/money").GetComponent<Text>();
        image_advertising = transform.Find("OverPanel/Advertising").GetComponent<Image>();
        Text_moneyUp = transform.Find("Moeny/Text").GetComponent<Text>();

        DestroyContent = GameObject.Find("Destroy").transform;


    }

    void Start()
    {
        btn_Back.onClick.AddListener(OnClickBack);
        btn_Home.onClick.AddListener(() =>
        {
            MessManager.Instance.RemoveListener(0001, NextButtonShow);
            PassDate.Instance.playerInit.money += money;
            SceneManager.LoadScene(0);
           
        });
        btn_Next.onClick.AddListener(OnClickNext);
        btn_View.onClick.AddListener(() => { });
        MessManager.Instance.AddListener(0001, NextButtonShow);
        MessManager.Instance.AddListener(0002, RefreshMoney);
    }
    public void RefreshMoney(Notification notification)
    {
        if (Text_moneyUp != null)
        {
            Text_moneyUp.text = ((int)notification.objs[0]).ToString();
        }

    }

    void OnClickBack()
    {
        mainState = StateContorl.Instance.Find<MainState>("MainState");
        mainState.onBegin();

        for (int i = 0; i < DestroyContent.childCount; i++)
        {
            GameObject.Destroy(DestroyContent.GetChild(i).gameObject);
        }

        mainState.ChanagerMap(PassDate.Instance.playerInit.NowPass);

        overPanel.SetActive(false);
        PassDate.Instance.playerInit.money += money;

        CloseUI();
    }
    void OnClickNext()
    {
        if (PassDate.Instance.playerInit.NowPass >= PassDate.Instance.AllPass.Count -1)
        {
            MessManager.Instance.RemoveListener(0001, NextButtonShow);
            PassDate.Instance.playerInit.money += money;
            SceneManager.LoadScene(0);
        }
        else
        {
            PassDate.Instance.playerInit.NowPass++;

            mainState = StateContorl.Instance.Find<MainState>("MainState");
            mainState.onBegin();

            for (int i = 0; i < DestroyContent.childCount; i++)
            {
                GameObject.Destroy(DestroyContent.GetChild(i).gameObject);
            }

            mainState.ChanagerMap(PassDate.Instance.playerInit.NowPass);

            overPanel.SetActive(false);
            PassDate.Instance.playerInit.money += money;

        }


        CloseUI();
        MessManager.Instance.RemoveListener(0001, NextButtonShow);
        PassDate.Instance.playerInit.money += money;
    }

    void NextButtonShow(Notification notification)
    {
        btn_Next.gameObject.SetActive((bool)notification.objs[0]);
    }

    public void RefreshShow()
    {
        PassInit passInit = PassDate.Instance.passInit;
        money = (passInit.award + int.Parse(Text_moneyUp.text));

        Text_money.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
