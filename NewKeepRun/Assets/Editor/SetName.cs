using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum GameObjectType
{
    Floors,
    WallContents,
    DrinkContents,
}

public class SetName : EditorWindow
{
    public string nametemp;
    public GameObjectType gameObjectType;


    [MenuItem("编辑器/NameSet")]



    public static void Init()
    {
        SetName temp = GetWindow<SetName>("地图编辑器");
        temp.Show();
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("批量修改名称");
        nametemp = GUILayout.TextField(nametemp);
        GUILayout.EndHorizontal();

        gameObjectType = (GameObjectType)EditorGUILayout.EnumPopup(gameObjectType);

        if (GUILayout.Button("修改"))
        {
            if (nametemp != null)
            {
                Read();
            }
            else
            {
                Debug.LogError("输入名称");
            }
        }

    }

    void Read()
    {
        GameObject temp = GameObject.Find(gameObjectType.ToString());
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            temp.transform.GetChild(i).transform.name  = nametemp;
        }
    }
}
