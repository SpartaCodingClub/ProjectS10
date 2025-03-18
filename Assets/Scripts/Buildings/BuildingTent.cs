using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingTent : BuildingBase
{
    public float healthRecoveryAmount = 30f;
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

    public void RecoverHealth()
    {
        if (!canUse || !canInteract) return;

        P_Stat playerStat = Managers.Game.Player?.GetComponent<P_Stat>();
        if (playerStat != null)
        {
            playerStat.Health += healthRecoveryAmount;
            playerStat.Health = Mathf.Clamp(playerStat.Health, 0, 100);
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
