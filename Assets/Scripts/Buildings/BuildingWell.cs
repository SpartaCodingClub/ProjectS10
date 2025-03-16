using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWell : MonoBehaviour
{
    public float waterRecoveryAmount = 20f;

    public void RecoverWater()
    {
        if (Managers.Game.Player != null)
        {
            P_Stat playerStat = Managers.Game.Player.GetComponent<P_Stat>();
            if (playerStat != null)
            {
                playerStat.Water += waterRecoveryAmount;
                playerStat.Water = Mathf.Clamp(playerStat.Water, 0, 100);
                Debug.Log("식수 게이지: " + playerStat.Water);
            }
        }
    }
}

// 상호작용 시 쿨타임 기능
