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

    #region 값 조절
    public void ChangeMoveValue(float value)
    {
        animator.SetFloat("MovementValue", Mathf.Clamp01(value));
    }

    public void ChangeMoveAngle(float value)
    {
        animator.SetFloat("MovementAngle", Mathf.Clamp(value, -180, 180));
    }

    public void ChangeIsWorking(bool value)
    {
        animator.SetBool("IsWorking", value);
    }

    public void PlayAnim(string input)
    {
        if(isAnimationing == false)
        {
            isAnimationing = true;
            StartCoroutine(PlayAni(input));
        }
    }
    #endregion

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
    
    public void PlayBuilding(float waitingTime = 0f)
    {
        isAnimationing = true;
        StartCoroutine(PlayBuild(waitingTime));
    }

    #region 코루틴
    IEnumerator PlayAni(string input)
    {
        animator.CrossFade(input, 0.1f);
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
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

    IEnumerator PlayBuild(float waitingTime)
    {
        animator.SetBool("IsWorking", true);
        animator.CrossFade("Work", 0.1f);
        yield return new WaitForSeconds(waitingTime);
        animator.SetBool("IsWorking", false);
        isAnimationing = false;
        yield return null;
    }

    float GetAnimationClipLength(string name)
    {
        if (animator.runtimeAnimatorController == null) return 0f;

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip.length;
            }
        }

        return 0f;
    }
}
    #endregion