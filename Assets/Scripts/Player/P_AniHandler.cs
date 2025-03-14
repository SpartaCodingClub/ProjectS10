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

    public void PlayAnim(string input)
    {
        if(isAnimationing == false)
        {
            isAnimationing = true;
            StartCoroutine(PlayAni(input));
        }
    }

    IEnumerator PlayAni(string input)
    {
        animator.CrossFade(input, 0.1f);
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetNextAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        isAnimationing = false;
    }
}
