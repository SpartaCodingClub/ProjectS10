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
    [SerializeField] private Queue<BuildingObject> actionQueue = new Queue<BuildingObject>();
    [Header("Nav Mesh 관련")]
    [SerializeField] bool isChasing;
    [SerializeField] NavMeshAgent navMesh;
    [SerializeField] NavMeshSurface navMeshSurface;
    float navMeshDistance;

    Coroutine curCoroutine;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.updatePosition = false;
        navMesh.updateRotation = false;
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        isChasing = false;
        navMeshDistance = navMesh.radius / 2 + 0.2f;
    }
    public void AddAction(BuildingObject build)
    {
        navMeshSurface.BuildNavMesh();
        actionQueue.Enqueue(build);
        curCoroutine = StartCoroutine(Building());
    }

    private void Update()
    {
        if (isChasing)
            navMesh.speed = Mathf.Lerp(navMesh.speed, player.PStat.curSpeed, player.SpeedChangeValue * Time.deltaTime);
        else
            navMesh.speed = 0;
    }
    public void AllListDelete()
    {
        actionQueue.Clear();
    }

    IEnumerator Building()
    {
        BuildingObject ac;
        actionQueue.TryDequeue(out ac);
        if (ac == null)
            yield break;
        //경로 지정
        navMesh.SetDestination(ac.transform.position);
        isChasing = true;
        while (true)
        {
            float distance = Vector3.Distance(transform.position, ac.transform.position);
            if (distance < navMeshDistance)
                break;
            Vector3 direction = (navMesh.nextPosition - transform.position).normalized;
            player.CharacterController.Move(direction * navMesh.speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("도착!");
        //도착 후 건물 건설 시작 and 그만큼 대기.
        yield return null;
    }
}
