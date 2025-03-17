using DG.Tweening;
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
    Die
}

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private State _state;

    [Header("Stat")]
    public float attackRange;
    public float attackRate;
    bool isAttacking = false;
    EnemyStat enemyStat;
    ProjectileHandler projectileHandler;

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

    [Header("Raycast")]
    [SerializeField] Vector3 boxOffset;
    [SerializeField] Vector3 boxSize;
    RaycastHit[] hits;
    bool attackRaycast;
    private float lastRaycastTime = 0f;
    [SerializeField] private float raycastInterval = 0.2f; // 0.2초마다 실행


    private Animator _animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        enemyStat = GetComponent<EnemyStat>();
        playerTarget = GameObject.Find("Player").transform;
        boxSize = new Vector3(1, 1, attackRange);

        if (enemyStat.eclass == E_Class.Ranged || enemyStat.eclass == E_Class.FinalBoss)
            projectileHandler = GetComponent<ProjectileHandler>();
    }

    private void Start()
    {
        agent.speed = enemyStat.Speed;
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
            case State.Die:
                break;
        }

        Debug.Log(_state);

        if (enemyStat.isDead)
        {
            agent.isStopped = true;
            _animator.SetTrigger("isDie");
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
                agent.isStopped = false;
                break;
            case State.Die:
                break;
        }
    }

    void PassivUpdate()
    {
        //목표로 이동 or 목표 공격
        if (_state == State.Wandering && agent.remainingDistance < 0.5f)
        {
            SetState(State.Idle);
            Invoke("WanderToNewLocation", wanderWaitTime);
        }

        if (RayCastRange()) SetState(State.Attack);
    }

    // 이동
    void WanderToNewLocation()
    {
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
        

        if (RayCastRange())
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            isAttacking = true;
            Debug.Log("공격");
            _animator.SetTrigger("isAttack");
            Attacking();
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

    bool RayCastRange()
    {
        if (Time.time - lastRaycastTime < raycastInterval) return attackRaycast;
        lastRaycastTime = Time.time;

        attackRaycast = false;

        RaycastHit[] curHits = hits = Physics.BoxCastAll(transform.position + transform.forward * boxSize.z / 2 + transform.TransformDirection(boxOffset),
            boxSize / 2, transform.forward, transform.rotation, 0, layerMask);
        foreach (RaycastHit hit in curHits)
        {
            if (hit.transform.gameObject == target.gameObject)
            {
                Vector3 targetPoint = hit.collider.ClosestPoint(transform.position);
                
                transform.DOLookAt(targetPoint, 1.0f);
                attackRaycast = true;
                break;
            }
                
        }

        return attackRaycast;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = transform.position + transform.forward * boxSize.z / 2 + transform.TransformDirection(boxOffset);
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }

    public void Attacking()
    {
        switch (enemyStat.eclass)
        {
            case E_Class.Melee:
                //target.gameObject.GetComponent<StatHandler>().Damage(enemyStat.Attack);
                break;
            case E_Class.Ranged:
                projectileHandler.Shoot();
                break;
            case E_Class.MiniBoss:

                break;
            case E_Class.FinalBoss:
                projectileHandler.Shoot();
                break;
        }

        
    }
}
