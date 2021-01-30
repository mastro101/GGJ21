using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        Check();
    }

    void Check()
    {
        if (FindObjectOfType<UIManager>().hideTimer)
        {
            timerText.enabled = false;
        }
        else
        {
            timerText.enabled = true; 
        }
    }
}
