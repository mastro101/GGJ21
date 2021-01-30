using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundsManager : MonoBehaviour
{
    public static UISoundsManager instance;
    AudioSource _aSource;

    public List<AudioClip> clips; 

    private void Awake() 
    {
        SetSingleton();
        _aSource = GetComponent<AudioSource>();
    }
    
    public void PlayAudioClip(int sfxNum)
    {
        _aSource.pitch = Random.Range(.8f, 1.2f);
        _aSource.PlayOneShot(clips[sfxNum]);
    }

    void SetSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}