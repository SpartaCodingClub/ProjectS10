using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSJTest : InteractableObject
{
    [SerializeField] BuildingBase building;

    private void Start()
    {
        building = GetComponent<BuildingBase>();
        Invoke(nameof(ForceTest), 3.0f);
        //InvokeRepeating(nameof(AddBuildingAction), 3.0f, 5);
    }

    private void AddBuildingAction()
    {
        //Managers.Game.Player.PStat.Damage(20);
        Managers.Game.Player.PlayerAction.AddAction(building);
    }

    private void ForceTest()
    {
        Managers.Game.Player.ForceMovePlayer(new Vector3(0, 0, 5));
    }
}
