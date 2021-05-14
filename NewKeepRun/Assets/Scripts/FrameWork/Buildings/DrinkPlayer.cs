using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 饮品 金币
/// </summary>
public class DrinkPlayer : BodyBase
{
    public Drink drink;                     //转换类型数据
    public DrinkPlayer(BuildingBase building):base(building)
    {
        if (building is Drink)
        {
            drink = building as Drink;
        }
    }

    public override bool Distancejudgment(GameObject other)
    {
        float left = prefab.transform.position.x - prefab.transform.localScale.x / 2;
        float right = (prefab.transform.lossyScale.x) / 2 + prefab.transform.position.x;

        //Debug.Log("左边：" + left);
        //Debug.Log("右边：" + rigth);

        if (other.transform.position.z >= prefab.transform.position.z - zoffect)
        {
            if (left <= other.transform.position.x && other.transform.position.x <= right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override bool IsDownOrUp(GameObject other)
    {
        if (other.transform.position.z >= prefab.transform.position.z + zoffect)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
