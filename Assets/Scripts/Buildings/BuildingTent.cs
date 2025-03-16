using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTent : MonoBehaviour
{
    public float healthRecoveryAmount = 30f; 

    public void RecoverHealth()
    {
        if (Managers.Game.Player != null)
        {
            P_Stat playerStat = Managers.Game.Player.GetComponent<P_Stat>();
            if (playerStat != null)
            {
                playerStat.Health += healthRecoveryAmount;
                playerStat.Health = Mathf.Clamp(playerStat.Health, 0, 100); 
                Debug.Log("현재 체력: " + playerStat.Health);
            }
        }
    }
}

// 상호작용 시 쿨타임 기능
