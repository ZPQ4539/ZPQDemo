using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

public class PassEditor : EditorWindow
{
    Dictionary<int, PassInit> AllPass = new Dictionary<int, PassInit>();  //要保存的数据

    private static GameObject Floors;                                    //存放地板的
    private static GameObject WallContents;                              //存放墙体的
    private static GameObject DrinkContents;                             //存放饮料的


    public string passId;                                              //临时关卡ID
    public PassInit passinit;                                          //临时关卡
    public PassType passType;                                          //关卡类型
    public string award;                                               //通关奖励

    [MenuItem("编辑器/Pass")]
    public static void Init()
    {
        PassEditor temp = GetWindow<PassEditor>("地图编辑器");
        temp.Show();

        Read();
    }
    static void Read()
    {

        Floors = GameObject.Find("Floors");
        if (Floors == null)
        {
            Floors = new GameObject();
            Floors.name = "Floors";
        }

        WallContents = GameObject.Find("WallContents");
        if (WallContents == null)
        {
            WallContents = new GameObject();
            WallContents.name = "WallContents";
        }


        DrinkContents = GameObject.Find("DrinkContents");
        if (DrinkContents == null)
        {
            DrinkContents = new GameObject();
            DrinkContents.name = "DrinkContents";
        }
    }

    private void OnGUI()
    {
        ShowWindow();
        ShowPassItem();
        CreatButton();
    }

    private void ShowWindow()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("关卡ID：");
        passId = GUILayout.TextField(passId);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("关卡类型：");
        passType = (PassType)EditorGUILayout.EnumPopup(passType);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("通关奖励：");
        award = GUILayout.TextField(award);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("读取场景"))
        {
            ReadScene();
        }

        if (GUILayout.Button("创建关卡"))
        {
            if (award == null || passId == null)
            {
                Debug.LogError("请输入关卡ID");
            }
            else if (AllPass.ContainsKey(int.Parse(passId)))
            {
                Debug.LogError("已有相同的关卡ID");
            }
            else
            {
                PassInit init =  new PassInit();
                init.id = int.Parse(passId);
                init.passType = passType;
                init.award = int.Parse(award);
                AllPass.Add(int.Parse(passId), init);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("-----------------------------------------------------------------------");
    }

    void ReadScene()
    {
        if (passinit != null)
        {
            passinit.floors.Clear();
            passinit.drinks.Clear();
            passinit.barriers.Clear();
            for (int i = 0; i < DrinkContents.transform.childCount; i++)
            {
                GameObject child = DrinkContents.transform.GetChild(i).gameObject;
                Drink drink = new Drink();

                if (child.name.StartsWith("Coin"))
                {
                    drink.drinkType = DrinkType.金币;
                }
                else if (child.name.StartsWith("DrinkWater"))
                {
                    drink.drinkType = DrinkType.饮料;
                }
                else
                {
                    drink.drinkType = DrinkType.酒;
                }
                drink.Modelname = child.name;




                drink.x = child.transform.position.x;
                drink.y = child.transform.position.y;
                drink.z = child.transform.position.z;

                drink.sx = child.transform.localScale.x;
                drink.sy = child.transform.localScale.y;
                drink.sz = child.transform.localScale.z;

                drink.zoffect = 1;
                drink.yoffect = 1;

                passinit.drinks.Add(drink);

            }

            for (int i = 0; i < WallContents.transform.childCount; i++)
            {
                GameObject child = WallContents.transform.GetChild(i).gameObject;
                Barrier barrier = new Barrier();

                barrier.Modelname = child.name;

                if (child.name == "Home")
                {
                    barrier.barriertype = BarrierType.床;
                }
                else
                {
                    barrier.barriertype = BarrierType.障碍物;
                }
                barrier.x = child.transform.position.x;
                barrier.y = child.transform.position.y;
                barrier.z = child.transform.position.z;

                barrier.sx = child.transform.localScale.x;
                barrier.sy = child.transform.localScale.y;
                barrier.sz = child.transform.localScale.z;

                barrier.zoffect = 1;
                barrier.yoffect = 1;
                passinit.barriers.Add(barrier);

            }
            for (int i = 0; i < Floors.transform.childCount; i++)
            {
                GameObject child = Floors.transform.GetChild(i).gameObject;
                Floor floor = new Floor();

                floor.Modelname = child.name;


                floor.x = child.transform.position.x;
                floor.y = child.transform.position.y;
                floor.z = child.transform.position.z;

                floor.sx = child.transform.localScale.x;
                floor.sy = child.transform.localScale.y;
                floor.sz = child.transform.localScale.z;

                floor.zoffect = 1;
                floor.yoffect = 1;
                passinit.floors.Add(floor);

            }
        }
    }

    Vector2 vector = new Vector2(0, 0);
    Vector2 vector1 = new Vector2(0,0);

    void ShowPassItem()
    {
        vector = GUILayout.BeginScrollView(vector);

        foreach (var itemPass in AllPass)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("关卡ID:" + itemPass.Key + "/显示"))
            {
                passinit = itemPass.Value;
                passinit.isShow = !passinit.isShow;
            }
            if (GUILayout.Button("删除"))
            {
                AllPass.Remove(itemPass.Key);
                break;
            }


            GUILayout.EndHorizontal();


            if (itemPass.Value.isShow)
            {
                passinit = itemPass.Value;
                vector1 = GUILayout.BeginScrollView(vector1);

                GUILayout.Label("地板--------------------------------------------------");
                EditorGUILayout.Space(5);
                foreach (var item in itemPass.Value.floors)
                {
                    GUILayout.Space(5);
                    GUILayout.Label("地板名称：" + item.Modelname + "\t位置 x:" + item.x + "y:" + item.y + "z:" + item.z);
                    GUILayout.Label("大小 x:" + item.sx + "y:" + item.sy + "z:" + item.sz);

                    string ztemp = item.zoffect.ToString();
                    string ytemp = item.yoffect.ToString();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("z 偏移:");
                    item.zoffect = float.Parse(GUILayout.TextField(ztemp));
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("y 偏移:");
                    item.yoffect = float.Parse(GUILayout.TextField(ztemp));
                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.Space(5);
                GUILayout.Label("饮品--------------------------------------------------");
                EditorGUILayout.Space(5);
                foreach (var item in itemPass.Value.drinks)
                {
                    GUILayout.Space(5);
                    GUILayout.Label("饮品名称：" + item.Modelname + "\t位置 x:" + item.x + "y:" + item.y + "z:" + item.z);
                    GUILayout.Label("大小 x:" + item.sx + "y:" + item.sy + "z:" + item.sz);

                    string ztemp = item.zoffect.ToString();
                    string ytemp = item.yoffect.ToString();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("z 偏移:");
                    item.zoffect = float.Parse(GUILayout.TextField(ztemp));
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                    GUILayout.Label("y 偏移:");
                    item.yoffect = float.Parse(GUILayout.TextField(ztemp));

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();

                    GUILayout.Label("饮品类型");
                    item.drinkType = (DrinkType)EditorGUILayout.EnumPopup(item.drinkType);

                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.Space(5);
                GUILayout.Label("障碍物--------------------------------------------------");
                EditorGUILayout.Space(5);
                foreach (var item in itemPass.Value.barriers)
                {
                    EditorGUILayout.Space(5);
                    GUILayout.Label("障碍物名称：" + item.Modelname + "\t位置 x:" + item.x + "y:" + item.y + "z:" + item.z);
                    GUILayout.Label("大小 x:" + item.sx + "y:" + item.sy + "z:" + item.sz);

                    string ztemp = item.zoffect.ToString();
                    string ytemp = item.yoffect.ToString();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("z 偏移:");
                    item.zoffect = float.Parse(GUILayout.TextField(ztemp));

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("y 偏移:");
                    item.yoffect = float.Parse(GUILayout.TextField(ztemp));
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();


                    GUILayout.Label("障碍物类型");
                    item.barriertype = (BarrierType)EditorGUILayout.EnumPopup(item.barriertype);

                    GUILayout.EndHorizontal();
                }


               GUILayout.EndScrollView();
            }
        }

       
        GUILayout.EndScrollView();

    }
    void CreatButton()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("创建饮品"))
        {
            GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("model/Cube"), DrinkContents.transform, false);
            temp.transform.name = "Cube";
        }
        if (GUILayout.Button("创建障碍物"))
        {
            GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("model/Cube"), WallContents.transform, false);
            temp.transform.name = "Cube";
        }
        if (GUILayout.Button("创建地面"))
        {
            GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("model/Cube"), Floors.transform, false);
            temp.transform.name = "Cube";
        }
       

        GUILayout.EndHorizontal();

        if (GUILayout.Button("加载当前关卡到场景"))
        {
            Debug.LogError("清空当前场景操作");
            if (passinit != null)
            {
                foreach (var item in passinit.floors)
                {
                    GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("model/" + item.Modelname), Floors.transform, false);
                    temp.transform.name = item.Modelname;
                    SetPos(temp, item.x, item.y, item.z, item.sx, item.sy, item.sz);


                }
                foreach (var item in passinit.drinks)
                {
                    GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("model/" + item.Modelname), DrinkContents.transform, false);
                    temp.transform.name = item.Modelname;
                    SetPos(temp, item.x, item.y, item.z, item.sx, item.sy, item.sz);
                }
                foreach (var item in passinit.barriers)
                {
                    GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("model/" + item.Modelname), WallContents.transform, false);
                    temp.transform.name = item.Modelname;
                    SetPos(temp, item.x, item.y, item.z, item.sx, item.sy, item.sz);
                }
            }

        }


        if (GUILayout.Button("保存Json"))
        {
            string temp = JsonConvert.SerializeObject(AllPass);
            File.WriteAllText(Application.dataPath + "/Resources/Plugins/Pass.json", temp);
            AssetDatabase.Refresh();
        }
    }
    private void SetPos(GameObject temp,float x,float y ,float z,float sx,float sy,float sz)
    {
        temp.transform.position = new Vector3(x,y,z);
        temp.transform.localScale = new Vector3(sx,sy,sz);
    }

    private void OnEnable()
    {
        ReadJson();
    }

    public void ReadJson()
    {
        if (File.Exists(Application.dataPath + "/Resources/Plugins/Pass.json"))
        {
            string temp = File.ReadAllText(Application.dataPath + "/Resources/Plugins/Pass.json");
            AllPass = JsonConvert.DeserializeObject<Dictionary<int, PassInit>>(temp);
        }
    }
}
