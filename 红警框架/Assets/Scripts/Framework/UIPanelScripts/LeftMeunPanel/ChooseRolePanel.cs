using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRolePanel : IUIPanel
{
    public ChooseRolePanel(PBDG_GameMain pBDG_GameMain) : base(pBDG_GameMain)
    {

    }

    private MainPanel m_MainPanel = null;
    private Image m_Background;

    //你的名字
    private InputField m_YourName;
    //选择国家
    private GameObject m_countryContent;
    //颜色
    private GameObject m_ColorContent;

    private Color Mycolor;
    private string country;




    /// <summary>
    /// 设置主控UI
    /// </summary>
    /// <param name="mainPanel"></param>
    public void SetMainPanel()
    {
        m_MainPanel = pBDG_Game.GetMainPanel();
        if (m_MainPanel != null)
        {
            prefabPanel = m_MainPanel.m_MenuPanel.transform.Find("ChooseFlagMenu").gameObject;
            m_Background = prefabPanel.transform.GetComponent<Image>();

            m_YourName = prefabPanel.transform.Find("myJues/Input_Name").GetComponent<InputField>();
            m_YourName.text = "player";

            m_countryContent = prefabPanel.transform.Find("myJues/countryContent").gameObject;
            m_ColorContent = prefabPanel.transform.Find("myJues/ColorContent").gameObject;

            DropDownInit(m_countryContent, false);
            DropDownInit(m_ColorContent, true);

            pBDG_Game.SetPlayer(m_YourName.text, "美国", Color.white);
        }
    }

    public override void Begin()
    {
        if (m_MainPanel == null)
        {
            SetMainPanel();
            OpenUIPanel();
        }
        else
        {
            OpenUIPanel();
        }
    }

    public override void End()
    {
        CloseUIPanel();
    }

    public override void Update()
    {

    }

    /// <summary>
    /// 下拉式列表
    /// </summary>
    private void DropDownInit(GameObject content,bool isColor)
    {
        Image imageChoose = content.transform.Find("image_choose").GetComponent<Image>();
        Toggle toggle = content.transform.Find("btn_Down").GetComponent<Toggle>();
        GameObject downImage = content.transform.Find("content").gameObject;
        GameObject contentitem = downImage.transform.Find("Scroll View/Viewport/Content").gameObject;


        toggle.onValueChanged.AddListener((flag) =>
        {
            downImage.SetActive(flag);
        });

        for (int i = 0; i < contentitem.transform.childCount; i++)
        {
            Button btn = contentitem.transform.GetChild(i).GetComponent<Button>();

            btn.onClick.AddListener(() => 
            {
                if (isColor)
                {
                    imageChoose.color = btn.transform.Find("image_icon").GetComponent<Image>().color;
                    Mycolor = imageChoose.color;
                }
                else
                {
                    imageChoose.sprite = btn.transform.Find("image_icon").GetComponent<Image>().sprite;
                    country = btn.transform.Find("text_name").GetComponent<Text>().text;
                }
                toggle.isOn = !toggle.isOn;

                Debug.Log($"选择国家：{country}  选择颜色:{Mycolor.ToString()}");
                pBDG_Game.SetPlayer(m_YourName.text,country,Mycolor);
            });
        }
        
    }
}
