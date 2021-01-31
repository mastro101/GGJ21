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

    [SerializeField] float tempoSpingitone;
    [SerializeField] float forzaSpingitone;

    public int index;
    [SerializeField] Sprite immaginettaCarina;
    
    public AudioClip[] idle;
    public AudioClip[] dolore;
    private AudioSource suonaIlSock;

    NavMeshAgent agenteNavigante;
    ViewTriggerFromTransform vedoSeLoVedo;
    Animator animator;
    UIManager uiManager;
    SockState comeSto;

    private void OnEnable()
    {
        vedoSeLoVedo.OnTriggerEnter += IDontLikeThereThisIsGoing;
        ChangeState(SockState.RandomMove);
    }

    private void Awake()
    {
        agenteNavigante = GetComponent<NavMeshAgent>();
        vedoSeLoVedo = GetComponent<ViewTriggerFromTransform>();
        animator = GetComponentInChildren<Animator>();
        comeSto = SockState.RandomMove;
        uiManager = FindObjectOfType<UIManager>();
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

    public void ChangeAnimation(string s)
    {
        if (animator)
            animator.SetTrigger(s);
    }

    public Sprite SpriteDammelaSubito(bool b)
    {
        if (uiManager)
        {
            if (b)
                uiManager.DammiImmaginonaCalzaCheLaMostro(immaginettaCarina);
            else
                uiManager.DammiImmaginonaCalzaCheLaMostro(null);
        }
        return immaginettaCarina;
    }

    public void Calcione()
    {
        if (comeSto != SockState.OMGtheyKickMe)
        {
            Transform t = vedoSeLoVedo.GetObj();
            ChangeState(SockState.OMGtheyKickMe);
        }
    }

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
            ChangeAnimation("Move");
            SuoniIdlosi();
            agenteNavigante.isStopped = false;
            MoveACazzoDiCane();
        }
        else if (comeSto == SockState.OMGtheyKickMe)
        {
            ChangeAnimation("Stun");
            SuoniDolorosi();
            if (corutineIniziaSpingitone != null)
                StopCoroutine(corutineIniziaSpingitone);
            corutineIniziaSpingitone = IniziaSpingitone(vedoSeLoVedo.GetObj());
            StartCoroutine(corutineIniziaSpingitone);
        }
        else if (comeSto == SockState.FuckingRun)
        {
            ChangeAnimation("Run");
            SuoniIdlosi();
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
        suonaIlSock.pitch = Random.Range(0.8f, 1.2f);
        suonaIlSock.PlayOneShot(idle[Random.Range(0, idle.Length)]);
    }

    public void SuoniDolorosi()
    {
        suonaIlSock.pitch = Random.Range(0.8f, 1.2f);
        suonaIlSock.PlayOneShot(dolore[Random.Range(0, dolore.Length)]);
    }
}

public enum SockState
{
    RandomMove,
    FuckingRun,
    OMGtheyKickMe,
}