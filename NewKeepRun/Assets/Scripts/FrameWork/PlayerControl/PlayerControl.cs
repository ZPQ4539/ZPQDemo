using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerType
{
    oneType,
    TwoType,
    ThreeType,
    Null,
}

public class PlayerControl : MonoBehaviour
{
    public List<DrinkPlayer> AllDrinkPlayer = new List<DrinkPlayer>();         //所有的饮品金币
    public List<BarrierPlayer> AllBarrierPlayers = new List<BarrierPlayer>();  //所有墙体以及家或者 床


    private DrinkPlayer drinkPlayer = null;                                    //控制的主玩家
    private Animator anim;                                                     //控制的主玩家动画


    public List<DrinkPlayer> MyContorlPrefab = new List<DrinkPlayer>();       //我控制的所有的物体
    public bool isPlay;                                                       //开始游戏

    public float isSpeed = 5;                                                //移动速度
    public bool isGameOver = false;                                           //游戏是否结束

    private BarrierPlayer barrierPlayer;                                      //碰到的墙体
    private Vector3 playerInitPos = new Vector3();                            //最下面游戏物体的位置

    private Transform DestroyContent;                                         //垃圾箱

    private int index = 0;
    private int number;

    int numberCole = 0;

    Quaternion right;                                                        //摇晃
    Quaternion lefght;
    Quaternion quat;

    private AudioSource audioSource;

    public void Init()
    {
        AllDrinkPlayer = PassDate.Instance.AllDrinkPlayer;
        AllBarrierPlayers = PassDate.Instance.AllBarrierPlayers;

        DestroyContent = GameObject.Find("Destroy").transform;

        index = 0;


        right = Quaternion.AngleAxis(0, Vector3.forward);
        lefght = Quaternion.AngleAxis(0, Vector3.forward);
        quat = right;

        audioSource = transform.GetComponent<AudioSource>();

        numberCole = 0;
        MessManager.Instance.BroadCast(0002, new Notification(numberCole));
        MyContorlPrefab.Clear();

    }

    public void CreatJues(BuildingBase modelName)          //游戏开始后创建选择的人物
    {
        drinkPlayer = new DrinkPlayer(modelName);
        drinkPlayer.CreatPrefab(this.transform);


        anim = drinkPlayer.anim;
        MyContorlPrefab.Add(drinkPlayer);
    }

    void FixedUpdate()
    {
        if (isPlay && isGameOver == false)
        {
            CollWall();
           
            CollDrink();

            DownPlayer();
        }   
    }

    void CollWall()    //碰到墙或者家
    {
        for (int i = 0; i < AllBarrierPlayers.Count; i++)
        {
            if (AllBarrierPlayers[i].Distancejudgment(drinkPlayer.prefab))
            {

                if (AllBarrierPlayers[i].barrier.barriertype == BarrierType.床)
                {

                    isPlay = false;
                    isGameOver = true;

                    drinkPlayer = MyContorlPrefab[MyContorlPrefab.Count - 1];
                    for (int j = 0; j < index; j++)
                    {
                        GameObject.Destroy(MyContorlPrefab[j].prefab);
                    }
                    MyContorlPrefab.Clear();


                    right = Quaternion.AngleAxis(0, Vector3.forward);
                    lefght = Quaternion.AngleAxis(0, Vector3.forward);

                    DownPlayer();

                    anim.SetTrigger("Die");
                    drinkPlayer.prefab.transform.position = new Vector3(drinkPlayer.prefab.transform.position.x,AllBarrierPlayers[i].prefab.transform.position.y + 2,drinkPlayer.prefab.transform.position.z);
                    drinkPlayer.prefab.transform.LookAt(Vector3.back);

                    isSpeed = 0;
                    break;
                }
                if (AllBarrierPlayers[i].barrier.barriertype == BarrierType.家)
                {
                    barrierPlayer = null;
                    MessManager.Instance.BroadCast(0001,new Notification(true));
                }

                else if (AllBarrierPlayers[i].barrier.barriertype == BarrierType.障碍物)
                {
                    if (MyContorlPrefab.Count > AllBarrierPlayers[i].yoffect)
                    {
                        index--;
                        barrierPlayer = AllBarrierPlayers[i];
                        drinkPlayer.prefab.transform.SetParent(DestroyContent, true);
                        MyContorlPrefab.Remove(drinkPlayer);


                        drinkPlayer = MyContorlPrefab[0];
                        AllBarrierPlayers.Remove(barrierPlayer);
                        break;
                    }
                    else
                    {
                        MessManager.Instance.BroadCast(0001, new Notification(false));
                        isSpeed = 0;
                        anim.SetBool("Run", false);
                        anim.SetTrigger("Die");
                        isPlay = false;
                        isGameOver = true;
                        break;
                    }
                  
                } 
            }
            if (AllBarrierPlayers[i].IsDownOrUp(drinkPlayer.prefab))
            {
                AllBarrierPlayers.Remove(AllBarrierPlayers[i]);
            }
        }
    }

    void CollDrink()   //碰到饮料或者金币
    {
        for (int i = 0; i < AllDrinkPlayer.Count; i++)
        {
            if (AllDrinkPlayer[i].Distancejudgment(drinkPlayer.prefab))
            {
                UpPlayer(AllDrinkPlayer[i]);
                break;
            }
            if (AllDrinkPlayer[i].IsDownOrUp(drinkPlayer.prefab))
            {
                AllDrinkPlayer.Remove(AllDrinkPlayer[i]);
            }
        }
    }

    void DownPlayer()
    {
        if (barrierPlayer != null)
        {
            if (barrierPlayer.IsDownOrUp(drinkPlayer.prefab))
            {
                float tempY = drinkPlayer.prefab.transform.position.y - PassDate.Instance.Floors.transform.position.y;
                for (int i = 0; i < MyContorlPrefab.Count; i++)
                {
                    MyContorlPrefab[i].prefab.transform.position = new Vector3(MyContorlPrefab[i].prefab.transform.position.x, MyContorlPrefab[i].prefab.transform.position.y -tempY, MyContorlPrefab[i].prefab.transform.position.z);
                }
                
                number--;

                if (isPlay != false && isGameOver != true)
                {
                    MessManager.Instance.BroadCast(1000, new Notification(MyContorlPrefab.Count,isPlay));
                }

                audioSource.clip = AssetManager.Instance.LoadAudio("Audio/", "Se_UI_JinBi");

                audioSource.Stop();
                audioSource.Play();

             

                barrierPlayer = null;
            }
        }
    }

   

    void UpPlayer(DrinkPlayer drinkNow)
    {
        if (drinkNow.drink.drinkType == DrinkType.酒)
        {
            PosRefresh(drinkNow);
            audioSource.clip = AssetManager.Instance.LoadAudio("Audio/", "Eat");

            audioSource.Stop();
            audioSource.Play();

            number++;
        }
        if (drinkNow.drink.drinkType == DrinkType.饮料)
        {
            PosRefresh(drinkNow);

            audioSource.clip = AssetManager.Instance.LoadAudio("Audio/", "Eat");

            audioSource.Stop();
            audioSource.Play();
        }
        if (drinkNow.drink.drinkType == DrinkType.金币)
        {
            GameObject.Destroy(drinkNow.prefab);
            AllDrinkPlayer.Remove(drinkNow);
            numberCole++;
            MessManager.Instance.BroadCast(0002, new Notification(numberCole));
            audioSource.clip = AssetManager.Instance.LoadAudio("Audio/", "Coic1");

            audioSource.Stop();
            audioSource.Play();
        }
    }
    void PosRefresh(DrinkPlayer drinkNow)  //位置刷新
    {
        playerInitPos.x = drinkPlayer.prefab.transform.position.x;
        playerInitPos.y = drinkPlayer.prefab.transform.position.y;
        playerInitPos.z = drinkPlayer.prefab.transform.position.z;


        drinkNow.prefab.transform.SetParent(this.transform, true);
        drinkNow.prefab.transform.position = playerInitPos;

        MyContorlPrefab.Insert(0, drinkNow);


        for (int i = 1; i < MyContorlPrefab.Count; i++)
        {
            MyContorlPrefab[i].prefab.transform.position = new Vector3(MyContorlPrefab[i].prefab.transform.position.x, MyContorlPrefab[i].prefab.transform.position.y + drinkNow.yoffect, MyContorlPrefab[i].prefab.transform.position.z);
        }


        MessManager.Instance.BroadCast(1000, new Notification(MyContorlPrefab.Count, isPlay));
        index++;

        drinkPlayer = drinkNow;
        AllDrinkPlayer.Remove(drinkNow);
    }


    float timeOver = 0;

    void Update()
    {
        Control();               //玩家控制
        if (isGameOver && drinkPlayer != null)
        {
            
            if (timeOver >= 4)
            {
                MessManager.Instance.BroadCast(0000, null);
                timeOver = 0;
                drinkPlayer = null;
            }
            else
            {
                timeOver += Time.deltaTime;

            }
        }
    }

    private void OnDisable()
    {
        AllDrinkPlayer.Clear();
        AllBarrierPlayers.Clear();
    }



    void Control()
    {
        if (isPlay == false && drinkPlayer != null)
        {
            if (Input.GetMouseButton(0))
            {
                isPlay = true;
                anim.SetBool("Run", true);
            }
        }
        if (isPlay && drinkPlayer != null)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * isSpeed);




            if (number == 2)
            {
                right = Quaternion.AngleAxis(-1, Vector3.forward);
                lefght = Quaternion.AngleAxis(1, Vector3.forward);
            }
            if (number == 4)
            {
                right = Quaternion.AngleAxis(-1.5f, Vector3.forward);
                lefght = Quaternion.AngleAxis(1.5f, Vector3.forward);
            }
            if (number >= 6)
            {
                right = Quaternion.AngleAxis(-2, Vector3.forward);
                lefght = Quaternion.AngleAxis(2, Vector3.forward);
            }

           

            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 0.02f);
            if (transform.rotation == quat)
            {
                if (quat == lefght)
                {
                    quat = right;
                }
                else
                {
                    quat = lefght;
                }
            }

            if (transform.position.z >= 75)
            {
                if (transform.position.x < -0.5f)
                {
                    transform.Translate(Vector3.right * isSpeed * Time.deltaTime);
                }
                else if (transform.position.x > 0.5f)
                {
                    transform.Translate(Vector3.left * isSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (Input.GetMouseButton(0) && barrierPlayer == null)
                {
                    if ((Camera.main.pixelWidth / 2) > Input.mousePosition.x)
                    {
                        if (transform.position.x > -2)
                        {
                            transform.Translate(Vector3.left * isSpeed * Time.deltaTime);
                        }
                    }
                    else
                    {
                        if (transform.position.x < 2)
                        {
                            transform.Translate(Vector3.right * isSpeed * Time.deltaTime);
                        }
                    }
                }

            }


        }

    }           //玩家控制

}
