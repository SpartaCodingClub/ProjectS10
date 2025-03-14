using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController player;
    [SerializeField] string Miningtag;
    [SerializeField] string Gatheringtag;
    P_InteractionFinder finder;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        finder = GetComponent<P_InteractionFinder>();
    }

    public void Interact()
    {
        if (finder.CanInteract()) 
        {
            if (finder.curInteract.tag.Equals(Miningtag))
            {
                player.pAnimationHandler.PlayAnim("Mining");
            }
            else if (finder.curInteract.tag.Equals(Gatheringtag))
            {
                player.pAnimationHandler.PlayAnim("Gathering");
            }
        }
    }

    public void InteractFuction(GameObject obj)
    {
        //가져와서 상호작용하는 함수.
    }
}
