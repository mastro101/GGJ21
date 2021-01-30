using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIControllerMenu : MonoBehaviour
{
    private Button _button;
    
    public GameObject nextBottoncione;
    public GameObject previousPannellone;
    public GameObject nextPannellone;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButton);
    }

    public void OnButton()
    {
        SetNextBottoncione();
        SwichPannellone();
    }

    void SetNextBottoncione()
    {
        FindObjectOfType<EventSystem>().SetSelectedGameObject(nextBottoncione);
    }

    void SwichPannellone()
    {
        if (previousPannellone != null && nextPannellone != null)
        {
            previousPannellone.SetActive(false);
            nextPannellone.SetActive(true);
        }
    }
}