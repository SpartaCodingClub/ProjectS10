using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    Animator animator;
    float _lastFootstep;
    // Start is called before the first frame update
    void Start()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var footstep = animator.GetFloat("FootStep");

        if (_lastFootstep > 0 && footstep < 0 || _lastFootstep < 0 && footstep > 0)
        {
            Managers.Audio.Play(Clip.SoundFX_FootStep);
            Debug.Log("발자국!");
        }

        _lastFootstep = footstep;
    }
}
