using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 弹窗UI tips
/// </summary>
public class TipsPanel : ViewBase
{
    public Button btn_confirm;
    public Button btn_cancel;
    public Text contentText;

    public GameObject m_prefab;

    public Action m_confirm;
    public Action m_cancel;
    public Action<string> textCon;

    public override void Init()
    {
        if (m_prefab != null)
        {
            btn_confirm = m_prefab.transform.Find("btn_confirm").GetComponent<Button>();
            btn_cancel = m_prefab.transform.Find("btn_cancel").GetComponent<Button>();
            contentText = m_prefab.transform.Find("text_TipsText").GetComponent<Text>();

            btn_confirm.onClick.AddListener(()=> { m_confirm(); });
            btn_cancel.onClick.AddListener(()=> { m_cancel(); });

        }
    }


    public override void End()
    {

    }

    public override void Update()
    {

    }

    public void ShowContent(string text)
    {
        contentText.text = text;
    }
   
}
