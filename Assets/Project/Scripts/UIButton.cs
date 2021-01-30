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
        _button.onClick.AddListener(PlayButtonSound);
    }

    void PlayButtonSound()
    {
        UISoundsManager.instance.PlayAudioClip("SFX_UIButton");
    }
}
