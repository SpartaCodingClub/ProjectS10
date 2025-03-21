using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class P_Action : MonoBehaviour
{
    PlayerController player;
    [SerializeField] private Queue<BuildingBase> actionQueue = new Queue<BuildingBase>();
    [Header("현재 건물")]
    [SerializeField] private BuildingBase curBuilding;
    [Header("Nav Mesh 관련")]
    [SerializeField] Vector3 offset;
    [SerializeField] bool isChasing;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] bool forceMod;
    float navMeshAgentDistance;
    public bool IsChasing { get { return isChasing; } }
    Coroutine curBuildCoroutine;
    Coroutine curMoveToCoroutine;
    public NavMeshAgent NavMeshAg {  get { return navMeshAgent; } }

    private void Start()
    {
        player = GetComponent<PlayerController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        isChasing = false;
        navMeshAgentDistance = navMeshAgent.radius / 2 + 0.2f;
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        forceMod = false;
    }

    private void Update()
    {
        navMeshAgent.velocity = player.CharacterController.velocity;
        if (actionQueue.Count > 0 && curBuildCoroutine == null && curBuilding == null)  
            StartBuilding();
        navMeshAgent.nextPosition = transform.position;
    }


    #region 취소 관련
    public void CancelcurBuildCoroutine()
    {
        if(curBuildCoroutine != null)
            StopCoroutine(curBuildCoroutine);
        curBuildCoroutine = null;
    }

    public void CancelCurMoveCoroutine()
    {
        if(curMoveToCoroutine != null)
            StopCoroutine(curMoveToCoroutine);
        curMoveToCoroutine = null;
    }
    public void CancelBuilding()
    {
        if (curBuildCoroutine != null && forceMod == false) 
        {
            CancelFunction();
        }
    }

    public void navMeshAgentReset()
    {
        navMeshAgent.ResetPath();
        navMeshAgent.velocity = Vector3.zero;
    }

    void CancelFunction()
    {
        navMeshAgent.updateRotation = false;
        CancelcurBuildCoroutine();
        CancelCurMoveCoroutine();
        forceMod = false;
        if (curBuilding != null)
        {
            curBuilding.StartRemoving(1.5f);
        }
        curBuilding = null;
        navMeshAgentReset();
        isChasing = false;
        player.pAnimationHandler.ChangeIsWorking(false);
        if (!player.PStat.isDead) 
            player.pAnimationHandler.isAnimationing = false;
        AllListDelete();
    }
    #endregion
    #region 큐 조작 관련
    public void AddAction(BuildingBase build)
    {
        Managers.Game.NavMeshSurface.BuildNavMesh();
        actionQueue.Enqueue(build);
        Debug.Log("큐 추가");
    }
    public void AllListDelete()
    {
        foreach(var action in actionQueue)
        {
            action.StartRemoving(1.5f);
        }
        actionQueue.Clear();
        Debug.Log("큐 청소");
    }

    #endregion

    public void WarpingNavmesh()
    {
        navMeshAgent.Warp(transform.position);
    }
    private void StartBuilding()
    {
        if (curBuildCoroutine == null) 
            curBuildCoroutine = StartCoroutine(Building());
    }
    public void ForceMove(Vector3 targetPos, bool DoorClose)
    {
        CancelFunction();
        StartCoroutine(ForceMoveTo(targetPos, DoorClose));
    }
    IEnumerator Building()
    {
        navMeshAgent.updateRotation = true;
        actionQueue.TryDequeue(out curBuilding);
        if (curBuilding == null)
            yield break;
        curMoveToCoroutine = StartCoroutine(MoveTo(curBuilding.transform.position));
        yield return curMoveToCoroutine;
        Debug.Log("도착!");
        //도착 후 건물 건설 시작 and 그만큼 대기.
        isChasing = true;
        curBuilding.Initialize();
        player.pAnimationHandler.PlayBuilding(curBuilding.BuildTime);
        yield return new WaitForSeconds(curBuilding.BuildTime);
        curBuilding = null;
        isChasing = false;
        curBuildCoroutine = null;
        navMeshAgent.updateRotation = false;
        yield return new WaitForFixedUpdate();
    }

    IEnumerator ForceMoveTo(Vector3 targetPos,bool DoorClose)
    {
        forceMod = true;
        curMoveToCoroutine = StartCoroutine(MoveTo(targetPos));
        yield return curMoveToCoroutine;
        CancelFunction();
        if (DoorClose == true)
            Managers.Game.CurrentMap.Close();
        yield return null;
    }

    IEnumerator MoveTo(Vector3 targetPos)
    {
        //경로 지정
        navMeshAgent.updateRotation = true;
        Managers.Game.NavMeshSurface.BuildNavMesh();
        navMeshAgent.Warp(transform.position);
        navMeshAgent.SetDestination(targetPos);
        isChasing = true;

        player.pAnimationHandler.isAnimationing = true;
        while (true)
        {
            Vector3 direction = navMeshAgent.desiredVelocity.normalized;
            player.CharacterController.Move(direction * player.PStat.curSpeed * Time.fixedDeltaTime);
            player.pAnimationHandler.ChangeMoveAngle(0);
            player.pAnimationHandler.ChangeMoveValue(1);

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                Debug.Log("navMeshAgentAgent 이동 종료!");
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        player.pAnimationHandler.isAnimationing = false;
        player.pAnimationHandler.ChangeMoveValue(0);
        isChasing = false;
        curMoveToCoroutine = null;
        navMeshAgentReset();
        navMeshAgent.updateRotation = false;
        yield return new WaitForFixedUpdate();
    }
}
