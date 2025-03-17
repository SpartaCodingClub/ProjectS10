using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using VFolders.Libs;

public class P_Action : MonoBehaviour
{
    PlayerController player;
    [SerializeField] private Queue<BuildingBase> actionQueue = new Queue<BuildingBase>();
    [Header("Nav Mesh 관련")]
    [SerializeField] Vector3 offset;
    [SerializeField] bool isChasing;
    [SerializeField] NavMeshAgent navMesh;
    [SerializeField] bool forceMod;
    float navMeshDistance;
    public bool IsChasing { get { return isChasing; } }
    Coroutine curCoroutine;
    Coroutine curMoveToCoroutine;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        navMesh = GetComponent<NavMeshAgent>();
        isChasing = false;
        navMeshDistance = navMesh.radius / 2 + 0.2f;
        navMesh.updatePosition = false;
        navMesh.updateRotation = false;
        forceMod = false;
    }

    private void Update()
    {
        navMesh.velocity = player.CharacterController.velocity;
    }

    public void CancelBuilding()
    {
        if (curCoroutine != null && forceMod != true) 
        {
            CancelFunction();
        }
    }

    void CancelFunction()
    {
        if (curCoroutine != null)
            StopCoroutine(curCoroutine);
        if (curMoveToCoroutine != null)
            StopCoroutine(curMoveToCoroutine);
        forceMod = false;
        curCoroutine = null;
        curMoveToCoroutine = null;
        navMesh.ResetPath();
        navMesh.velocity = Vector3.zero;
        isChasing = false;
        player.pAnimationHandler.ChangeIsWorking(false);
        player.pAnimationHandler.isAnimationing = false;
        AllListDelete();
    }
    public void AddAction(BuildingBase build)
    {
        Managers.Game.surface.BuildNavMesh();
        actionQueue.Enqueue(build);
        Debug.Log("큐 추가");
        if(curCoroutine == null)
            curCoroutine = StartCoroutine(Building());
    }
    public void AllListDelete()
    {
        actionQueue.Clear();
    }

    public void ForceMove(Vector3 targetPos)
    {
        if (curCoroutine != null || curMoveToCoroutine != null)
        {
            CancelFunction();
        }
        StartCoroutine(ForceMoveTo(targetPos));
    }
    IEnumerator Building()
    {
        BuildingBase ac;
        actionQueue.TryDequeue(out ac);
        if (ac == null)
            yield break;
        curMoveToCoroutine = StartCoroutine(MoveTo(ac.transform.position));
        yield return curMoveToCoroutine;
        Debug.Log("도착!");
        //도착 후 건물 건설 시작 and 그만큼 대기.
        isChasing = true;
        player.pAnimationHandler.PlayBuilding(3);
        yield return new WaitForSeconds(3);
        if (actionQueue.Count > 0)
        {
            curCoroutine = StartCoroutine(Building());
        }
        isChasing = false;
        curCoroutine = null;
        yield return new WaitForFixedUpdate();
    }

    IEnumerator ForceMoveTo(Vector3 targetPos)
    {
        forceMod = true;
        curMoveToCoroutine = StartCoroutine(MoveTo(targetPos));
        yield return curMoveToCoroutine;
        CancelFunction();
        yield return null;
    }

    IEnumerator MoveTo(Vector3 targetPos)
    {
        //경로 지정
        navMesh.Warp(transform.position);
        navMesh.SetDestination(targetPos);
        isChasing = true;

        player.pAnimationHandler.isAnimationing = true;
        while (true)
        {
            Vector3 direction = navMesh.desiredVelocity.normalized;
            player.CharacterController.Move(direction * player.PStat.curSpeed * Time.fixedDeltaTime);
            navMesh.velocity = player.CharacterController.velocity;
            player.pAnimationHandler.ChangeMoveAngle(0);
            player.pAnimationHandler.ChangeMoveValue(1);

            if (!navMesh.pathPending && navMesh.remainingDistance <= navMesh.stoppingDistance)
            {
                Debug.Log("NavMeshAgent 이동 종료!");
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        player.pAnimationHandler.isAnimationing = true;
        player.pAnimationHandler.ChangeMoveValue(0);
        isChasing = false;
        curMoveToCoroutine = null;
        navMesh.ResetPath();
        navMesh.velocity = Vector3.zero;
        yield return new WaitForFixedUpdate();
    }
}
