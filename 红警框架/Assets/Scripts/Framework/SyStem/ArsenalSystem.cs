using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArsenalSystem : IGameSystem
{
    //存放所有兵的对象
    public List<IBuding> AllSolider = new List<IBuding>();

    public ArsenalSystem(PBDG_GameMain pBDG_Game) : base(pBDG_Game)
    {
        Initialize();
    }

    public void AddSolider(ISolider solider)
    {
        AllSolider.Add(solider);
    }

    public void RemoveSolider(ISolider solider)
    {
        if (AllSolider.Contains(solider))
        {
            AllSolider.Remove(solider);
        }
    }

    public List<IBuding> GetSolider()
    {
        return AllSolider;
    }

    public override void Initialize()
    {

    }

    public override void Release()
    {

    }

    float time = 10;

    public override void Update()
    {
        if (AllSolider.Count > 0)
        {
            time -= Time.deltaTime;
            m_PBDGame.SetTime(time);
            if (time <= 0)
            {
                foreach (var item in m_PBDGame.AllChooseBuding)
                {
                    if (item.GetGameObject() == AllSolider[0].GetGameObject())
                    {
                        m_PBDGame.AllChooseBuding.Remove(item);
                        break;
                    }
                }

                foreach (var item in m_PBDGame.AllBuding["工兵"])
                {
                    if (item == AllSolider[0].GetGameObject())
                    {
                        m_PBDGame.AllBuding["工兵"].Remove(item);
                        break;
                    }
                }
                GameObject.Destroy(AllSolider[0].GetGameObject());
                AllSolider.RemoveAt(0);
                time = 50;
            }
        }

        foreach (var item in AllSolider)
        {
            item.Update(m_PBDGame.AllBuding["建筑物"]);
        }
    }
}
