using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTurret : BuildingBase
{
    private TurretShooting turretShooting;
    private TurretRotation turretRotation;

    public override void Initialize()
    {
        base.Initialize();
        turretShooting = GetComponent<TurretShooting>();
        turretRotation = GetComponent<TurretRotation>();

        StartConstruction(7f);

        if (turretShooting != null)
            turretShooting.EnableShooting(); 

        if (turretRotation != null)
            turretRotation.EnableRotation();
    }
}
