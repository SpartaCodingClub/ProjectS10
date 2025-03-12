using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    Idle,
    Wandering,
    Move,
    Attack
}

public class EnemyController : MonoBehaviour
{
    private State _state;

    private void Start()
    {
        SetState(State.Idle);
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
            case State.Wandering:
                Wandering();
                break;
            case State.Move:
                Moving();
                break;
            case State.Attack:
                Attacking();
                break;
        }
    }

    public void SetState(State state)
    {
        _state = state;

        switch (_state)
        {
            case State.Idle:
                break;
            case State.Wandering:
                break;
            case State.Move:
                break;
            case State.Attack:
                break;
        }
    }

    public void Wandering()
    {
        // 타겟팅을 한 오브젝트가 파괴 되었을 때 잠시 대기
    }
    public void Moving()
    {
        // 타겟팅을 한 오브젝트를 쫓아감
    }

    public void Attacking()
    {
        // 타겟팅을 한 오브젝트가 공격 사거리 이내에 들어 왔을 때 공격
    }
}
