using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Sock : MonoBehaviour
{
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToNewPos(Vector3 newPos)
    {
        agent.SetDestination(newPos);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            MoveToNewPos(Vector3.zero);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agent.Move(Vector3.right);
        }
    }
}