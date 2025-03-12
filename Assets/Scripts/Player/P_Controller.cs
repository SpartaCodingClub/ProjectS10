using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer MainRenderer;

    private void Awake()
    {
        MainRenderer = gameObject.FindComponent<SpriteRenderer>(nameof(MainRenderer));
    }
}