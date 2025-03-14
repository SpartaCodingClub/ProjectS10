using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public virtual void OnInteraction()
    {
        Debug.Log("상호작용");
    }
}