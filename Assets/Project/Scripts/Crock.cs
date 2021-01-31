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

    [SerializeField] float tempoSpingitone;
    [SerializeField] float forzaSpingitone;
    
    public AudioClip[] ringhi;
    public AudioClip[] idle;
    public AudioClip[] dolore;
    private AudioSource suonaIlCrock;

    NavMeshAgent agenteNavigante;
    ViewTriggerFromTransform vedoSeLoVedo;
    Animator animator;

    SockState comeSto;


    private void OnEnable()
    {
        vedoSeLoVedo.OnTriggerEnter += ILikeThereThisIsGoing;
    }

    private void Awake()
    {
        agenteNavigante = GetComponent<NavMeshAgent>();
        vedoSeLoVedo = GetComponent<ViewTriggerFromTransform>();
        animator = GetComponent<Animator>();
        comeSto = SockState.RandomMove;
        suonaIlCrock = GetComponent<AudioSource>();
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

    public void ChangeAnimation(string s)
    {
        if (animator)
            animator.SetTrigger(s);
            
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
            ChangeAnimation("Move");
            MoveACazzoDiCane();
        }
        else if (comeSto == SockState.FuckingRun)
        {
            ChangeAnimation("Run");
            StartInseguimentoAntiFashion(vedoSeLoVedo.GetObj());
        }
        else if (comeSto == SockState.OMGtheyKickMe)
        {
            ChangeAnimation("Stun");
            if (corutineIniziaSpingitone != null)
                StopCoroutine(corutineIniziaSpingitone);
            corutineIniziaSpingitone = IniziaSpingitone(vedoSeLoVedo.GetObj());
            StartCoroutine(corutineIniziaSpingitone);
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

    public void SpingitoneCrockkone()
    {
        if (comeSto != SockState.OMGtheyKickMe)
        {
            Transform t = vedoSeLoVedo.GetObj();
            ChangeState(SockState.OMGtheyKickMe);
        }
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

    IEnumerator corutineIniziaSpingitone;
    IEnumerator IniziaSpingitone(Transform t)
    {
        float timer = .5f;
        Vector3 pushDir = (transform.position - t.position).normalized;
        while (true)
        {
            agenteNavigante.Move(pushDir * forzaSpingitone * Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ChangeState(SockState.RandomMove);
                StopCoroutine(corutineIniziaSpingitone);
            }
            yield return null;
        }
    }

    public void SuoniIdlosi()
    {
        suonaIlCrock.pitch = Random.Range(0.8f, 1.2f);
        suonaIlCrock.PlayOneShot(idle[Random.Range(0, idle.Length)]);
    }
    
    public void SuoniRinghiosi()
    {
        suonaIlCrock.pitch = Random.Range(0.8f, 1.2f);
        suonaIlCrock.PlayOneShot(ringhi[Random.Range(0, ringhi.Length)]);
    }
    
    public void SuoniDolorosi()
    {
        suonaIlCrock.pitch = Random.Range(0.8f, 1.2f);
        suonaIlCrock.PlayOneShot(dolore[Random.Range(0, dolore.Length)]);
    }
    
}