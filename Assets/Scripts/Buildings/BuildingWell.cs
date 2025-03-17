using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWell : MonoBehaviour
{
    public float waterRecoveryAmount = 20f;
    private bool canUse = true;
    private bool canInteract = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canInteract = false;
        }
    }

    public void RecoverWater()
    {
        if (!canUse)
        {
            Debug.Log("쿨타임");
            return;
        }

        if (!canInteract)
        {
            Debug.Log("플레이어가 너무 멀리 있음");
            return;
        }

        if (Managers.Game.Player != null)
        {
            P_Stat playerStat = Managers.Game.Player.GetComponent<P_Stat>();

            if (playerStat != null)
            {
                playerStat.Water += waterRecoveryAmount;
                playerStat.Water = Mathf.Clamp(playerStat.Water, 0, 100);
                Debug.Log("식수 게이지: " + playerStat.Water);

                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(10f);
        canUse = true;
    }
}