using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class P_AniHandler : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMoveValue(float value)
    {
        animator.SetFloat("MovementValue", Mathf.Clamp01(value));
    }

    public void ChangeMoveAngle(float value)
    {
        animator.SetFloat("MovementAngle", Mathf.Clamp(value, -180, 180));
    }
}
