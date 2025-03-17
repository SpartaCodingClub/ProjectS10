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
    [Header("Nav Mesh 관련")]
    [SerializeField] Vector3 offset;
    [SerializeField] bool isChasing;
    [SerializeField] NavMeshAgent navMesh;
    [SerializeField] NavMeshSurface navMeshSurface;
    [SerializeField] GameObject navMeshSurfaceObject;
    float navMeshDistance;
    public bool IsChasing { get { return isChasing; } }
    Coroutine curCoroutine;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        navMesh = GetComponent<NavMeshAgent>();
        navMeshSurfaceObject = Resources.Load<GameObject>($"{Define.PATH_PLAYER}/PlayerSurface");
        navMeshSurface = Instantiate(navMeshSurfaceObject).GetComponent<NavMeshSurface>();
        isChasing = false;
        navMeshDistance = navMesh.radius / 2 + 0.2f;
        navMesh.updatePosition = false;
    }

    public void CancelBuilding()
    {
        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
            isChasing = false;
            player.pAnimationHandler.ChangeIsWorking(false);
            player.pAnimationHandler.isAnimationing = false;
            AllListDelete();
        }
    }
    public void AddAction(BuildingBase build)
    {
        navMeshSurface.BuildNavMesh();
        actionQueue.Enqueue(build);
        Debug.Log("큐 추가");
        if(curCoroutine == null)
            curCoroutine = StartCoroutine(Building());
    }
    public void AllListDelete()
    {
        actionQueue.Clear();
    }

    IEnumerator Building()
    {
        BuildingBase ac;
        actionQueue.TryDequeue(out ac);
        if (ac == null)
            yield break;
        //경로 지정
        Vector3 targetPos = new Vector3(ac.transform.position.x, transform.position.y, ac.transform.position.z);
        navMesh.SetDestination(targetPos);
        isChasing = true;

        player.pAnimationHandler.isAnimationing = true;
        while (true)
        {
            Vector3 direction = navMesh.nextPosition - transform.position;
            player.CharacterController.Move(direction * player.PStat.curSpeed * Time.fixedDeltaTime);
            player.pAnimationHandler.ChangeMoveAngle(0);
            player.pAnimationHandler.ChangeMoveValue(navMesh.speed / player.PStat.curSpeed);

            if (!navMesh.pathPending && navMesh.remainingDistance <= navMesh.stoppingDistance)
            {
                Debug.Log("NavMeshAgent 이동 종료!");
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        player.pAnimationHandler.isAnimationing = true;
        player.pAnimationHandler.ChangeMoveValue(0);
        Debug.Log("도착!");
        //도착 후 건물 건설 시작 and 그만큼 대기.
        player.pAnimationHandler.PlayBuilding(3);
        if (actionQueue.Count > 0)
        {
            curCoroutine = StartCoroutine(Building());
        }
        yield return new WaitForFixedUpdate();
    }
}
