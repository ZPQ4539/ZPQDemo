using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanelUI : UIbase
{
    public Slider m_SliderLoad;               //进度条
    public Text   m_TextIndexnumber;          //显示进度
    public Image  m_Advertising;              //广告位


    public bool isPlay = false;              //是否开始加载
    public Action LoadEnd;                   //加载完后执行方法
    private float indexNumber;               //加载进度

    void Start()
    {
        UIName = "LoadPanelUI";
        m_Advertising.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {

            if (m_SliderLoad.value >= 1)
            {
                if (LoadEnd != null)
                {
                    indexNumber = 0;
                    m_SliderLoad.value = 0;
                    LoadEnd.Invoke();
                }
            }
            else
            {
                indexNumber += Time.deltaTime * 0.5f;
                m_SliderLoad.value = indexNumber;
                m_TextIndexnumber.text = "加载中..." + Math.Round(indexNumber * 100).ToString() + "%";
            }
        }
    }
}
