using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ViewTriggerFromTransform))]
public class Sock : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float velocitàDiFuga;

    [SerializeField] float velocitàRotazioneInFugaComeLeGalline;
    [SerializeField] float livelloDiFrappèNellaRotazione;

    public int index;

    NavMeshAgent agenteNavigante;
    ViewTriggerFromTransform vedoSeLoVedo;
    SockState comeSto;

    private void OnEnable()
    {
        vedoSeLoVedo.OnTriggerEnter += IDontLikeThereThisIsGoing;
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
            Run_TUUUUUTUTUTUTUTUTUTUTU(vedoSeLoVedo.GetObj());
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (vedoSeLoVedo.GetObj().position - transform.position).normalized, out hit))
            {
                if(!hit.transform.IsChildOf(vedoSeLoVedo.GetObj()))
                    ChangeState(SockState.RandomMove);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (vedoSeLoVedo)
    //        if (vedoSeLoVedo.GetObj())
    //            Gizmos.DrawLine(transform.position, vedoSeLoVedo.GetObj().position);
    //}

    public void MoveACazzoDiCane()
    {
        agenteNavigante.SetDestination(VectorUtility.RandomV3OnPlaneY(range));
    }

    void ChangeState(SockState newState)
    {
        // exitState
        if (comeSto == SockState.RandomMove)
        {
            agenteNavigante.isStopped = true;
        }
        comeSto = newState;
        // enterState
        if (comeSto == SockState.RandomMove)
        {
            agenteNavigante.isStopped = false;
            MoveACazzoDiCane();
        }
    }

    void IDontLikeThereThisIsGoing(Transform playerT)
    {
        ChangeState(SockState.FuckingRun);
    }

    void Run_TUUUUUTUTUTUTUTUTUTUTU(Transform playerT)
    {
        Vector3 newPos = ((playerT.position - transform.position) * -1).normalized * velocitàDiFuga * Time.deltaTime;
        agenteNavigante.Move(newPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newPos, transform.up), Time.deltaTime * livelloDiFrappèNellaRotazione);
    }
}

public enum SockState
{
    RandomMove,
    FuckingRun,
}