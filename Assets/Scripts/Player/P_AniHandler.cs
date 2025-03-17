using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UIElements;

public class P_AniHandler : MonoBehaviour
{
    PlayerController player;
    public Animator animator;
    public bool isAnimationing = false;

    void Start()
    {
        player = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        player.PStat.DamageAction = PlayDamage;
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

    public void PlayAnim(string input)
    {
        if(isAnimationing == false)
        {
            isAnimationing = true;
            StartCoroutine(PlayAni(input));
        }
    }

    public void PlayDamage()
    {
        if (player.PStat.CanDamage == true)
        {
            isAnimationing = true;
            player.PStat.CanDamage = false;
            StartCoroutine(PlayDam());
        }
    }

    public void PlayDie()
    {
        isAnimationing = true;
        player.PStat.CanDamage = false;
        StartCoroutine(PlayDead());
    }

    IEnumerator PlayAni(string input)
    {
        animator.CrossFade(input, 0.1f);
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetNextAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        isAnimationing = false;
    }

    IEnumerator PlayDam()
    {
        yield return StartCoroutine(PlayAni("Damage"));
        yield return new WaitForSeconds(player.PStat.InvincibleTime);
        player.PStat.CanDamage = true;
        yield return null;
    }

    IEnumerator PlayDead()
    {
        animator.CrossFade("Die", 0.1f);
        yield return null;
    }
}
