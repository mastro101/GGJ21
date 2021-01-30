using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ViewTriggerFromTransform))]
public class Sock : MonoBehaviour
{
    [SerializeField] float range;

    NavMeshAgent agent;
    ViewTriggerFromTransform viewTrigger;
    SockState state;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        viewTrigger = GetComponent<ViewTriggerFromTransform>();
        state = SockState.FuckingRun;
    }

    private void Start()
    {
        MoveToRandomPos();
    }

    private void Update()
    {
        if (state == SockState.FuckingRun)
        {
            if (Vector3.Distance(agent.destination - (Vector3.up * agent.destination.y), transform.position - (Vector3.up * transform.position.y)) < 0.1f)
            {
                MoveToRandomPos();
            }

        }
    }

    public void MoveToRandomPos()
    {
        agent.SetDestination(VectorUtility.RandomV3OnPlaneY(range));
    }
}

public enum SockState
{
    RandomMove,
    FuckingRun,
}