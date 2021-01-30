using UnityEngine;
using UnityEngine.Events;

public class ViewTriggerFromTransform : MonoBehaviour
{
    [SerializeField] TransformData obj = null;
    [SerializeField][HideInInspector] float radius = 1f;
    [SerializeField][HideInInspector] float viewAngle = 45f;
    [SerializeField][Tooltip("this var work only if \"obj\" or \"obj.value\" are null")] Transform _objTransform;
    public Transform objTransform { get { return _objTransform; } private set { _objTransform = value; } }

    [SerializeField] UnityEvent<Transform> OnTriggerEnterUE = null;
    public System.Action<Transform> OnTriggerEnter;
    
    [SerializeField] UnityEvent<Transform> OnTriggerExitUE = null;
    public System.Action<Transform> OnTriggerExit;

    Vector3 differenceVector = Vector2.zero;
    float dist = 0;
    float angleViewToObj;
    bool inside = false;

    private void OnEnable()
    {
        if (obj)
            obj.OnChangeValue += SetObjTransform;
    }

    private void OnDisable()
    {
        if (obj)
            obj.OnChangeValue -= SetObjTransform;
    }

    private void Start()
    {
        if (CheckObj())
            SetObjTransform(obj.value);

        if (CheckTrigger())
            InvokeEnterEvent(obj.value);
    }

    private void Update()
    {
        Handler();
    }


    void Handler()
    {
        if (objTransform)
        {
            if (CheckTrigger() && !inside)
                InvokeEnterEvent(objTransform);
            else if (!CheckTrigger() && inside)
                InvokeExitEvent(objTransform);
        }
    }

    public bool CheckTrigger()
    {
        if (objTransform)
            return CheckDistance(objTransform) && CheckAngle(objTransform) && CheckNoObstacle(objTransform);

        return false;
    }
    void SetObjTransform(Transform t)
    {
        this.objTransform = t;
    }

    void InvokeEnterEvent(Transform t)
    {
        inside = true;
        OnTriggerEnter?.Invoke(t);
        OnTriggerEnterUE?.Invoke(t);
    }

    void InvokeExitEvent(Transform t)
    {
        inside = false;
        OnTriggerExit?.Invoke(t);
        OnTriggerExitUE?.Invoke(t);
    }

    public bool CheckDistance(Transform t)
    {
        differenceVector = t.position - transform.position;
        dist = differenceVector.magnitude;
        return dist <= radius;
    }

    public bool CheckAngle(Transform t)
    {
        differenceVector = t.position - transform.position;
        angleViewToObj = Vector3.Angle(transform.forward, differenceVector);

        return angleViewToObj <= viewAngle / 2f;
    }

    public bool CheckNoObstacle(Transform t)
    {
        Vector3 tLocalPos = t.position - transform.position;
        RaycastHit closestValidHit = new RaycastHit();
        RaycastHit[] hits = Physics.RaycastAll(transform.position, tLocalPos, tLocalPos.magnitude);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.IsChildOf(transform) && (closestValidHit.collider == null || closestValidHit.distance > hit.distance))
            {
                closestValidHit = hit;
            }
        }

        //RaycastHit hit;
        if (closestValidHit.transform != null)
        {
            if (closestValidHit.transform == t || closestValidHit.transform.IsChildOf(t))
                return true;
            else
                return false;

        }

        return true;
    }

    bool CheckObj()
    {
        if (obj)
            if (obj.value)
                return true;
        return false;
    }

    public Transform GetObj()
    {
        return objTransform;
    }
}