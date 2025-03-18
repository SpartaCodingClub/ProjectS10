using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWell : BuildingBase
{
    public float waterRecoveryAmount = 20f;
    public float hungerRecoveryAmount = 15f;
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

    public void Recovery()
    {
        if (!canUse || !canInteract) return;

        P_Stat playerStat = Managers.Game.Player?.GetComponent<P_Stat>();
        if (playerStat != null)
        {
            playerStat.Water += waterRecoveryAmount;
            playerStat.Water = Mathf.Clamp(playerStat.Water, 0, 100);

            playerStat.Water = Mathf.Clamp(playerStat.Water, 0, 100);
            playerStat.Hunger = Mathf.Clamp(playerStat.Hunger, 0, 100);

            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(10f);
        canUse = true;
    }
}