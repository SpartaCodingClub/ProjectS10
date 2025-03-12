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
    Move,
    Attack
}

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private State _state;

    [Header("Stat")]
    public float attackRange;

    [Header("Target")]
    [SerializeField] Transform target;
    public float targetDistance;
    public LayerMask layerMask;
    [Header("Wanderign")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetState(State.Idle);
    }

    private void Update()
    {
        if (target == null) FindNextTarget();

        if (target !=  null) targetDistance = Vector3.Distance(transform.position, target.position);

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
            case State.Wandering:
            case State.Move:
                agent.isStopped = false;
                break;
            case State.Attack:
                agent.isStopped = true;
                break;
        }

        
    }

    public void Wandering()
    {
        // 타겟팅을 한 오브젝트가 파괴 되었을 때 잠시 대기
        if (target != null)
        {
            if (_state == State.Idle || _state == State.Wandering && agent.remainingDistance < 0.1f)
            {
                Invoke("Moving", Random.Range(minWanderWaitTime, maxWanderWaitTime));
            }

            if (targetDistance < attackRange)
            {
                SetState(State.Attack);
            }
        }
        else
        {
            agent.isStopped = true;
        }
        
    }
    public void Moving()
    {
        // 타겟팅을 한 오브젝트를 쫓아감
        agent.SetDestination(target.position);

        if (targetDistance < attackRange)
        {
            SetState(State.Attack);
        }
                
    }

    public void Attacking()
    {
        if (targetDistance < attackRange)
        {
            Debug.Log("공격");
        }
        else
        {
            SetState(State.Wandering);
        }
    }

    public void FindNextTarget()
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
            SetState(State.Move);
        }
        else
        {
            SetState(State.Wandering); // 찾은 대상이 없으면 다시 배회 상태로 변경
        }

    }
}
