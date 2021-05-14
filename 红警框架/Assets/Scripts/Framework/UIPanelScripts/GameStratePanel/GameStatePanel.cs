using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameStatePanel : IUIPanel
{
    private Text allText;
    private Text time;
    private Transform content;
    private Transform Typecontent;
    private Text showTime;

    public ICamp camp;


    public GameStatePanel(PBDG_GameMain pBDG_Game) : base(pBDG_Game)
    {

    }

    public override void Begin()
    {
        prefabPanel = m_canvas.transform.Find("MainPanel/RightMain/GameMenu").gameObject;
        allText = m_canvas.transform.Find("MainPanel/RightMain/text_title").GetComponent<Text>();
        content = prefabPanel.transform.Find("Scroll View_Content/Viewport/Content").transform;
        Typecontent = prefabPanel.transform.Find("Typecontent").transform;
        showTime = prefabPanel.transform.Find("Text").transform.GetComponent<Text>();
        time = m_canvas.transform.Find("time").transform.GetComponent<Text>();
        RefreshShow();

        for (int i = 0; i < content.childCount; i++)
        {
            Button tempBtn = content.GetChild(i).GetComponent<Button>();
            tempBtn.onClick.AddListener(() =>
            {
                pBDG_Game.CreatBuding(tempBtn.name);
            });
        }

        for (int i = 0; i < Typecontent.childCount; i++)
        {
            Button tempBtn = Typecontent.GetChild(i).GetComponent<Button>();
            tempBtn.onClick.AddListener(() => 
            {
                OnTErainBtnClick(tempBtn.transform.Find("Text").GetComponent<Text>().text);
                //Debug.LogError(tempBtn.name);
            });
        }

        PBDG_GameMain.Instance.sceneType = SceneType.gameType;
        GetMoney();


    }
    //刷新显示
    public void RefreshShow()
    {
        //先获取所有兵营的信息
        List<ICamp> camps = pBDG_Game.GetCampList();
        for (int i = 0; i < camps.Count;)
        {
            GameObject temp = null;
            if (content.childCount > 0 && content.childCount - 1 >= i)
            {
                temp = content.GetChild(i).gameObject;
            }
            else
            {
                IAssetFactory Factory = PBDGFactory.GetAssetFactory();
                temp = Factory.Creat("prefab", "image_camp");
                temp.transform.SetParent(content, false);

                Button tempBtn = temp.GetComponent<Button>();
                tempBtn.onClick.AddListener(() =>
                {
                    pBDG_Game.CreatBuding(tempBtn.name);
                });
            }
            if (temp != null)
            {
                temp.name = camps[i].GetName();
                temp.GetComponent<Image>().sprite = ResourcesManager.Instance.Load<Sprite>("Sprites", camps[i].GetImage(), false);
            }

            if (camps[i].GetIsShow())
            {
                i++;
            }
            else
            {
                break;
            }
        }
    }

    public override void End()
    {

    }
    public override void Update()
    {
        time.text = pBDG_Game.GameTime.ToString();
    }

  

    private void OnTErainBtnClick(string text)
    {
        if (camp == null)
        {
            camp = pBDG_Game.GetICamp(text);
        }
        if (camp != null)
        {
            //添加命令
            camp.Train();
        }
    }

    public void SetText(int money)
    {
        allText.text = money.ToString();
    }

    public void GetMoney()
    {
        allText.text = pBDG_Game.m_player.GetMoney().ToString();
    }

    public void SetTime(float time)
    {
        showTime.text = "小兵使用时间倒计时" + Mathf.Round(time).ToString() + "秒";
    }

}
