using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : IBuilderAI
{
    public SoldierAI(IBuding buding):base(buding)
    {
        ChangeAIState<IdleAIState>("Idle");
    }

}
