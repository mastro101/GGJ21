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
    
    public void PlayAudioClip(string clipToPlay)
    {
        foreach (AudioClip clip in clips)
        {
            if(clip.name == clipToPlay)
                _aSource.PlayOneShot(clip);
        }
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