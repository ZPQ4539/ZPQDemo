using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TipsEditor : EditorWindow
{
    Dictionary<string, string> TipsText = new Dictionary<string, string>();
    string tempID = ""; // 临时id
    string Tipstex = ""; // 临时数据

    [MenuItem("菜单/Tips编辑器")]
    public static void Init()
    {
        TipsEditor tips = GetWindow<TipsEditor>("Tips编辑器");
        tips.Show();

       
    }

    public void OnEnable()
    {
        string[] temp = File.ReadAllLines(Application.dataPath + "/Resources/Plugins/tips.csv");
        for (int i = 1; i < temp.Length; i++)
        {
            Add(temp[i].Split(',')[0], temp[i].Split(',')[1]);
        }
    }

    Vector2 vector = new Vector2(0,0);

    void OnGUI()
    {
        GUILayout.Label("Tips编辑,可编辑Tips的提示的提示语句");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("tips编号");
         tempID = GUILayout.TextField(tempID);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("tips内容");
        Tipstex = GUILayout.TextField(Tipstex);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("添加提示语"))
        {
            Add(tempID,Tipstex);
        }


        EditorGUILayout.BeginScrollView(vector);
        foreach (var item in TipsText)
        {
            GUILayout.Label("编号："+item.Key + "\t提示语:" + item.Value);
            if (GUILayout.Button("移除提示语"))
            {
                TipsText.Remove(item.Key);
                break;
            }
        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("保存"))
        {
            File.WriteAllText(Application.dataPath + "/Resources/Plugins/tips.csv", "");
            string temp = "编号,提示语\n";
            string[] content = new string[TipsText.Count];
            int k = 0;
            foreach (var item in TipsText)
            {
                content[k++] = item.Key + "," + item.Value;
            }
            File.WriteAllText(Application.dataPath + "/Resources/Plugins/tips.csv", temp);
            File.AppendAllLines(Application.dataPath + "/Resources/Plugins/tips.csv", content);
            AssetDatabase.Refresh();
        }
    }

    private void Add(string tempID,string Tipstex)
    {
        if (TipsText.ContainsKey(tempID))
        {
            Debug.LogError("已存在相同的ID");
        }
        else
        {
            TipsText.Add(tempID, Tipstex);
        }
    }
}
