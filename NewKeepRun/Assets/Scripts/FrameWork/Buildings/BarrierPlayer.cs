using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障碍物
/// </summary>
public class BarrierPlayer : BodyBase
{
    public Barrier barrier;
    public BarrierPlayer(BuildingBase building) : base(building)
    {
        if (building is Barrier)
        {
            barrier = building as Barrier;
        }
    }

    public override bool Distancejudgment(GameObject other)
    {
        //float left = -prefab.transform.localScale.x / 2;
        //float rigth = prefab.transform.localScale.x / 2;

        float left = prefab.transform.position.x - prefab.transform.localScale.x / 2;
        float right = (prefab.transform.lossyScale.x) / 2 + prefab.transform.position.x;

        //Debug.Log("左边：" + left);
        //Debug.Log("右边：" + rigth);

        if (other.transform.position.z >= prefab.transform.position.z - zoffect )
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
