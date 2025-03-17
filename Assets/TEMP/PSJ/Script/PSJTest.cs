using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSJTest : InteractableObject
{
    [SerializeField] BuildingObject building;

    private void Start()
    {
        building = GetComponent<BuildingObject>();
        Invoke(nameof(AddBuildingAction), 3.0f);
        //InvokeRepeating(nameof(AddBuildingAction), 3.0f, 5);
    }

    private void AddBuildingAction()
    {
        //Managers.Game.Player.PStat.Damage(20);
        Managers.Game.Player.PlayerAction.AddAction(building);
    }
}
