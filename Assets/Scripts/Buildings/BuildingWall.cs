using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWall : BuildingBase
{
    public override void Initialize()
    {
        base.Initialize();
        gameObject.SetActive(true);
        StartConstruction(2f);
    }
}
