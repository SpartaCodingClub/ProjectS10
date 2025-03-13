using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
public enum State
{
    Idle,
    Wandering,
    Attack,
    Stop
}

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private State _state;

    [Header("Stat")]
    public float attackRange;
    public float attackRate;
    bool isAttacking = false;


    [Header("Target")]
    [SerializeField] Transform target;
    [SerializeField] Transform playerTarget;
    [SerializeField] private float targetDistance;
    private float playerDistance;
    public LayerMask layerMask;
    [Header("Wanderign")]
    public float playerWanderDistance;
    public float targetWanderDistance;
    public float wanderWaitTime;

    private Animator _animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        playerTarget = GameObject.Find("Player").transform;

    }

    private void Start()
    {
        SetState(State.Wandering);
    }

    private void FixedUpdate()
    {
        if (target != null) targetDistance = Vector3.Distance(transform.position, target.position);

        _animator.SetBool("isMove", _state == State.Wandering);

        switch (_state)
        {
            case State.Idle:
            case State.Wandering:
                PassivUpdate();
                break;
            case State.Attack:
                if (!isAttacking) Attack();
                break;
        }

        Debug.Log(_state);
    }

    public void SetState(State state)
    {
        _state = state;

        switch (_state)
        {
            case State.Idle:
            case State.Attack:
                agent.isStopped = true;
                break;
            case State.Wandering:
                agent.isStopped = false;
                break;
        }
    }

    void PassivUpdate()
    {
        //목표로 이동 or 목표 공격
        if (_state == State.Wandering && agent.remainingDistance < attackRange / 2)
        {
            SetState(State.Idle);
            Invoke("WanderToNewLocation", wanderWaitTime);
        }

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange, layerMask))
        {
            if (hit.transform == target)
            {
                SetState(State.Attack);
            }
        }
    }

    // 이동
    void WanderToNewLocation()
    {
        //if (_state != State.Idle) return;
        SetState(State.Wandering);
        FindNextTarget();

        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider != null)
        {
            Vector3 closestPoint = targetCollider.ClosestPoint(transform.position);
            agent.SetDestination(closestPoint);
        }
        else
        {
            agent.SetDestination(target.position); // 콜라이더가 없으면 원래 방식 사용
        }
    }

    // 목표 탐색
    private void FindNextTarget()
    {
        FindPlayerTarget();
        if (target == playerTarget) return;
        // 특정 반경 내에 레이어에 속한 콜라이더 모두 찾기
        Collider[] colliders = Physics.OverlapSphere(transform.position, targetWanderDistance, layerMask);
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        // 전체 콜라이더 중 가장 가까운 목표를 지정
        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = collider.transform;
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget;
        }
        else
        {
            SetState(State.Wandering); // 찾은 대상이 없으면 다시 배회 상태로 변경
        }        
    }

    public void FindPlayerTarget()
    {
        // 플레이어가 탐색 범위 안에 들어오면 목표를 플레이어로 변경
        if (playerTarget == null) return;
        playerDistance = Vector3.Distance(transform.position, playerTarget.position);
        if (playerWanderDistance > playerDistance)
        {
            target = playerTarget;
        }
        else
        {
            target = null;
        }
    }

    private void Attack()
    {
        FindNextTarget();
        
        //transform.LookAt(target);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange, layerMask))
        {
            if (hit.transform == target)
            {
                isAttacking = true;
                Debug.Log("공격");
                _animator.SetTrigger("isAttack");
            }
        }

        else
        {
            SetState(State.Wandering);
        }
    }
    // 애니메이션 이벤트가 호출하는 함수
    public void OnAttackEnd()
    {
        StartCoroutine(AttackRateRoutine());
    }
    //공격 주기를 코루틴으로 설정
    private IEnumerator AttackRateRoutine()
    {
        yield return new WaitForSeconds(attackRate); 
        isAttacking = false;
    }
}
