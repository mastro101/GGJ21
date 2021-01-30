using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ViewTriggerFromTransform))]
public class Crock : MonoBehaviour
{
    [SerializeField] float range;

    NavMeshAgent agenteNavigante;
    ViewTriggerFromTransform vedoSeLoVedo;
    SockState comeSto;


    private void OnEnable()
    {
        vedoSeLoVedo.OnTriggerEnter += ILikeThereThisIsGoing;
    }

    private void Awake()
    {
        agenteNavigante = GetComponent<NavMeshAgent>();
        vedoSeLoVedo = GetComponent<ViewTriggerFromTransform>();
        comeSto = SockState.RandomMove;
    }

    private void Start()
    {
        MoveACazzoDiCane();
    }

    private void Update()
    {
        if (comeSto == SockState.RandomMove)
        {
            if (Vector3.Distance(agenteNavigante.destination - (Vector3.up * agenteNavigante.destination.y), transform.position - (Vector3.up * transform.position.y)) < 0.1f)
            {
                MoveACazzoDiCane();
            }
        }
        else if (comeSto == SockState.FuckingRun)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (vedoSeLoVedo.GetObj().position - transform.position).normalized, out hit))
            {
                if(!hit.transform.IsChildOf(vedoSeLoVedo.GetObj()))
                    ChangeState(SockState.RandomMove);
            }
        }
    }

    public void MoveACazzoDiCane()
    {
        agenteNavigante.SetDestination(VectorUtility.RandomV3OnPlaneY(range));
    }

    void ChangeState(SockState newState)
    {
        // exitState
        if (comeSto == SockState.FuckingRun)
        {
            if (corutineSetDestination != null)
            {
                StopCoroutine(corutineSetDestination);
            }
        }
        comeSto = newState;
        // enterState
        if (comeSto == SockState.RandomMove)
        {
            MoveACazzoDiCane();
        }
        else if (comeSto == SockState.FuckingRun)
        {
            StartInseguimentoAntiFashion(vedoSeLoVedo.GetObj());
        }
    }

    void ILikeThereThisIsGoing(Transform playerT)
    {
        ChangeState(SockState.FuckingRun);
    }

    void StartInseguimentoAntiFashion(Transform t)
    {
        if (corutineSetDestination != null)
            StopCoroutine(corutineSetDestination);
        corutineSetDestination = SetNewDestination(t);
        StartCoroutine(corutineSetDestination);
    }

    IEnumerator corutineSetDestination;
    IEnumerator SetNewDestination(Transform t)
    {
        while (true)
        {
            agenteNavigante.SetDestination(t.position);
            yield return new WaitForSeconds(1f);
        }
    }
}