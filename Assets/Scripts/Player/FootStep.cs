using UnityEngine;

public class FootStep : MonoBehaviour
{
    Animator animator;
    float _lastFootstep;

    void Start()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    void Update()
    {
        var footstep = animator.GetFloat("FootStep");

        if (_lastFootstep > 0 && footstep < 0 || _lastFootstep < 0 && footstep > 0)
        {
            Managers.Audio.Play(Clip.SoundFX_FootStep, 5.0f);
        }

        _lastFootstep = footstep;
    }
}