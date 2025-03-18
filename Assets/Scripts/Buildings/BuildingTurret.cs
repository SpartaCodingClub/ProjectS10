public class BuildingTurret : BuildingBase
{
    private TurretShooting turretShooting;
    private TurretRotation turretRotation;

    public override void Initialize()
    {
        base.Initialize();
        turretShooting = GetComponent<TurretShooting>();
        turretRotation = GetComponent<TurretRotation>();

        if (turretShooting != null)
            turretShooting.EnableShooting();

        if (turretRotation != null)
            turretRotation.EnableRotation();
    }

    public override void DestroyBuilding()
    {
        if (turretShooting != null)
            turretShooting.DisableShooting();

        base.DestroyBuilding();
    }

    public override void StartRemoving(float removeTime)
    {
        if (turretShooting != null)
            turretShooting.DisableShooting();

        base.StartRemoving(removeTime);
    }
}