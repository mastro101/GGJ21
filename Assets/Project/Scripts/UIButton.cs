using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayConfirmSound);
    }

    void PlayConfirmSound()
    {
        UISoundsManager.instance.PlayAudioClip(0);
    }    
    
    public void PlayHoverSound()
    {
        UISoundsManager.instance.PlayAudioClip(1);
    }
}
