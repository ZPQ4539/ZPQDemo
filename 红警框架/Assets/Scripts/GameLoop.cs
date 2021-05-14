using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 游戏主进程
/// </summary>
public class GameLoop : MonoBehaviour
{
    void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);

        //设置过渡场景
        SceneStateController.Instance.SetLoadScene<LoadSceneState>("LoadScene");
        //设置主场景状态
        SceneStateController.Instance.AddState<MainSceneState>("MainScene");
        SceneStateController.Instance.AddState<GameSceneState>("GameScene");


        //加载全局tips 弹窗资源
        ResourcesManager.Instance.Load<GameObject>("prefab", "TipsPanel", false);
        ResourcesManager.Instance.Load<TextAsset>("Plugins", "tips",false);


        UIManager.Instance.OpenUI<TipsControl>("TipsPanel");

    }
    void Start()
    {
       
       

        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneStateController.Instance.SetState("MainScene",true);
        });

        //驱动MVC
        Control.Instance.Initialize();

    }
    void Update()
    {
        //驱动MVC
        Control.Instance.Update();

        SceneStateController.Instance.StateUpdate();
    }


    private void OnDestroy()
    {
        //驱动MVC
        Control.Instance.End();
    }
}
