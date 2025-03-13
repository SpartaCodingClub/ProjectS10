using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController_Refactor : MonoBehaviour
{
    private NavMeshAgent agent;
    private State _state;

    [Header("Stat")]
    public float attackRange;

    [Header("Target")]
    [SerializeField] Transform target;
    [SerializeField] Transform playerTarget;
    public float targetDistance;
    float playerDistance;
    public LayerMask layerMask;
    [Header("Wanderign")]
    public float playerWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    private Animator _animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        SetState(State.Wandering);
    }

    private void FixedUpdate()
    {

        if (target != null)targetDistance = Vector3.Distance(transform.position, target.position);

        _animator.SetBool("isMove", _state != State.Idle);

        switch (_state)
        {
            case State.Idle:
            case State.Wandering:
                PassivUpdate();
                break;
            case State.Attack:
                Attack();
                break;
        }
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
                agent.isStopped= false;
                break;   
        }
    }

    void PassivUpdate()
    {
        if (_state == State.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(State.Idle);
            Invoke("WanderToNewLocation", 1f);
        }

        if (targetDistance < attackRange && target != null)
        {
            SetState(State.Attack);
        }
    }


    void WanderToNewLocation()
    {
        if (_state != State.Idle) return;

        SetState(State.Wandering);
        FindNextTarget();
        agent.SetDestination(target.position);
    }

    // 목표 탐색
    private void FindNextTarget()
    {
        // 특정 반경 내에 레이어에 속한 콜라이더 모두 찾기
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxWanderDistance, layerMask);
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

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
            SetState(State.Idle); // 찾은 대상이 없으면 다시 배회 상태로 변경
        }

    }

    private void Attack()
    {
        if (targetDistance < attackRange && target != null)
        {
            Debug.Log("공격");
            _animator.SetTrigger("isAttack");
        }
        else
        {
            SetState(State.Wandering);
        }
    }
}

