using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public Button btn_Test;
    public PlayerControl playerControl;

    PlayerInit player;

    void Start()
    {

        //string tempInit = PlayerPrefs.GetString("playerInit");
        //if (tempInit != null)
        //{
        //    tempInit = "";
        //    PlayerPrefs.SetString("playerInit", tempInit);
        //}


        PassDate.Instance.PlayerRead();                               //读取配置文件
        StateContorl.Instance.LoadNewState<MainState>("MainState");   //设置到UI状态模式

        player = PassDate.Instance.playerInit;
    }

    void Update()
    {
        StateContorl.Instance.Update();

        foreach (var item in PassDate.Instance.AllDrinkPlayer)
        {
            if (item.drink.drinkType == DrinkType.金币)
            {
                item.prefab.transform.Rotate(Vector3.up);
            }
        }
    }


    public void OnDisable()
    {
       
    }

    private void OnApplicationQuit()
    {
        //string temp = player.money + "|" + player.NowPass + "|" + player.chooseNowRoleIndex;

       

        //foreach (var item in player.MyRole)
        //{
        //    temp += "|" + item.Key + ":" + item.Value;
        //}
        //PlayerPrefs.SetString("playerInit", temp);
    }
}
