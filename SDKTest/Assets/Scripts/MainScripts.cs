using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScripts : MonoBehaviour
{
    private AndroidJavaClass jc;
    private AndroidJavaObject jo;

    public Button btn;
    public Text textDeg;
    private Text text;

    private void Awake()
    {
        btn = transform.Find("Button").GetComponent<Button>();
        text = transform.Find("Text").GetComponent<Text>();

        //这两行是固定写法
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        btn.onClick.AddListener(OnBtnClickHandler);
    }

    private void OnBtnClickHandler()
    {
        textDeg.text = "执行点击事件";
        //调用Android中的方法UnityCallAndroid
        jo.Call("UnityCallAndroid");
       
        
    }

    public void UnityMethod(string str)
    {
        Debug.Log("UnityMethod被调用，参数：" + str);
        text.text = str;

        textDeg.text = "And回馈";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AndroidCallUnity()
    {

    }


}
