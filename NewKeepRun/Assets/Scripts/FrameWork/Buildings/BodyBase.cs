using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyBase
{
    public Animator anim;                  //动画
    public BuildingBase building;          //数据
    public GameObject prefab;              //游戏物体


    public float zoffect;
    public float yoffect;

    public BodyBase(BuildingBase building)
    {
        this.building = building;
        zoffect = this.building.zoffect;
        yoffect = this.building.yoffect;
    }

    public Animator Init(GameObject prefab)
    {
        this.prefab = prefab;
        anim = this.prefab.GetComponent<Animator>();

        return anim;
    }

    /// <summary>
    /// 创建单个
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public GameObject CreatPrefab(Transform content)
    {
         prefab = GameObject.Instantiate(AssetManager.Instance.Load("model/", building.Modelname), content.transform, false);
        if (prefab != null)
        {
            prefab.transform.name = building.Modelname;
            SetPos(prefab, building.x, building.y, building.z, building.sx, building.sy, building.sz);

            anim = prefab.GetComponent<Animator>();
            zoffect = prefab.transform.localScale.z;
            yoffect = prefab.transform.localScale.y;

            return prefab;
        }
        return null;
    }


    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="sx"></param>
    /// <param name="sy"></param>
    /// <param name="sz"></param>
    protected void SetPos(GameObject temp, float x, float y, float z, float sx, float sy, float sz)
    {
        temp.transform.position = new Vector3(x, y, z);
        temp.transform.localScale = new Vector3(sx, sy, sz);
    }

    public abstract bool Distancejudgment(GameObject other);

    public abstract bool IsDownOrUp(GameObject other);
}
