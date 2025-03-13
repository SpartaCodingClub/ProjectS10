using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWall : BuildingBase
{
    public override void Initialize()
    {
        base.Initialize();
        StartConstruction(2f);
    }
}
