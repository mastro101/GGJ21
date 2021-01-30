using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Props : MonoBehaviour
{
    [SerializeField] float minForce = 0f;
    [SerializeField] float maxForce = 1000f;

    [SerializeField] UnityEvent OnCalciato;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void CalcioSuperRandom()
    {
        rb.AddForce(VectorUtility.RandomV3(1f).normalized * Random.Range(minForce, maxForce));
        OnCalciato?.Invoke();
    }
}