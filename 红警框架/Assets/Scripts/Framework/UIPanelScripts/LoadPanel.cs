using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LoadSceneType
{
    One,
    Two
}
public class LoadPanel : IUIPanel
{
    public LoadPanel()
    {

    }


    //当前进度
    private float indexNumber;
    //加载方式俩种机制
    private LoadSceneType sceneType;
    //第一种加载界面
    private GameObject OneTypePanel;
    //第二种加载界面
    private GameObject TwoTypePanel;
    //显示百分比
    private Text text_LoadNumber;

    //显示进度条 后期要修改
    private Slider m_slider;


    //下个场景的名称
    private string m_NextSceneName;
    //加载速度
    private float m_Speed;
    //显示背景图
    private Image m_Background;

    private Sprite[] sprites;


    public override void Begin()
    {
        sceneType = LoadSceneType.One;
        prefabPanel = m_canvas.transform.Find("LoadPanel").gameObject;
        sprites = Resources.LoadAll<Sprite>("Background");
        OneTypePanel = prefabPanel.transform.Find("LoadPanelOne").gameObject;
        TwoTypePanel = prefabPanel.transform.Find("LoadGamePanel").gameObject;
    }
    public override void End()
    {

    }
    public override void Update()
    {
        LoadShow();
    }
    /// <summary>
    /// 显示加载进度
    /// </summary>
    private void LoadShow()
    {
        if (m_NextSceneName == null)
        {
            return;
        }
        if (m_NextSceneName != "")
        {


            indexNumber += Time.deltaTime * m_Speed;

            if (indexNumber >= 100)
            {
                indexNumber = 0;
                m_NextSceneName = null;
            }

            text_LoadNumber.text = "加载中请稍后..." + Mathf.Round(indexNumber) + "%";

            if (m_slider != null)
            {
                m_slider.value = indexNumber / 100;
            }


        }

    }
    /// <summary>
    /// 设置切换到下一个场景
    /// </summary>
    /// <param name="state"></param>
    /// <param name="LoadSceneName"></param>
    /// <param name="sceneType"></param>
    /// <param name="Speed"></param>
    public void SetNextSceneState(string LoadSceneName, LoadSceneType sceneType, float Speed)
    {
        indexNumber = 0;
        this.m_NextSceneName = LoadSceneName;
        this.sceneType = sceneType;
        this.m_Speed = Speed;

        if (m_Background != null)
        {
            m_Background.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        //切换面板状态
        ChangePanelType();
    }
    /// <summary>
    /// 切换加载状态 第一种有进度条  第二种没有
    /// </summary>
    private void ChangePanelType()
    {
        if (sceneType == LoadSceneType.One)
        {
            text_LoadNumber = OneTypePanel.transform.Find("text_LoadNumber").GetComponent<Text>();
            m_Background = OneTypePanel.GetComponent<Image>();
            m_slider = OneTypePanel.transform.Find("slider_LoadShow").GetComponent<Slider>();
            OpenUIPanel(OneTypePanel);
            CloseUIPanel(TwoTypePanel);

        }
        else
        {
            text_LoadNumber = TwoTypePanel.transform.Find("text_LoadNumber").GetComponent<Text>();
            m_Background = TwoTypePanel.GetComponent<Image>();
            OpenUIPanel(TwoTypePanel);
            CloseUIPanel(OneTypePanel);
        }
    }


    //为内部提供切换加载界面
    private void OpenUIPanel(GameObject panelType)
    {
        panelType.SetActive(true);
        m_Background.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void CloseUIPanel(GameObject panelType)
    {
        panelType.SetActive(false);
    }


    public float GetNowIndexNumber()
    {
        return indexNumber;
    }

}
