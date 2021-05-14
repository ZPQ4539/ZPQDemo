using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBuidingFactory
{
    public abstract ISolider CreatSolider(Vector3 pos);
}
