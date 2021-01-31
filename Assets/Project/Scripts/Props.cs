using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Props : MonoBehaviour
{
    [SerializeField] float minForce = 0f;
    [SerializeField] float maxForce = 1000f;

    [SerializeField] UnityEvent OnCalciato;
    
    public AudioClip[] clips;

    Rigidbody rb;
    AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }

    public void CalcioSuperRandom()
    {
        rb.AddForce(VectorUtility.RandomV3(1f).normalized * Random.Range(minForce, maxForce));
        audioSource.Play();
        OnCalciato?.Invoke();
    }

    public void SuonaISuoni()
    {
        audioSource.pitch = Random.Range(.8f, 1.2f);
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}