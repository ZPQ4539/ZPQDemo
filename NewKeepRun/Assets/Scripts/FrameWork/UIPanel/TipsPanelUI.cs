using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanelUI : UIbase
{
    public Text tipsContent;
    public Button btn_close;
    public Button btn_View;


    public string content;

    public void SetContent(string content,bool isView)
    {
        this.content = content;
        tipsContent.text = this.content;

        if (isView)
        {
            btn_View.gameObject.SetActive(true);
            btn_close.gameObject.SetActive(false);
        }
        else
        {
            btn_close.gameObject.SetActive(true);
            btn_View.gameObject.SetActive(false);
        }
    }


    void Start()
    {
        btn_close.onClick.AddListener(CloseUI);
        btn_View.onClick.AddListener(() => { CloseUI(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
