using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class P_AniHandler : MonoBehaviour
{
    PlayerController player;
    Animator animator;
    public bool isAnimationing = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(PlayerController pcon)
    {
        player = pcon;
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

    public void MeleeAttackAnim()
    {
        if (isAnimationing == false)
            StartCoroutine(MeleeAttack());
    }

    public void ThrowAnim()
    {
        if (isAnimationing == false)
        {
            StartCoroutine(Throw());
        }
    }

    IEnumerator MeleeAttack()
    {
        isAnimationing = true;
        animator.CrossFade("MeleeAttack", 0f);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAnimationing = false;
    }

    IEnumerator Throw()
    {
        isAnimationing = true;
        animator.CrossFade("Throw", 0f);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAnimationing = false;
    }
}
