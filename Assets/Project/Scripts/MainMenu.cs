using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;

    private void Start()
    {
        FindObjectOfType<EventSystem>().SetSelectedGameObject(startButton);
    }

    public void StartNewGame()
    {
        LoadLevelsManager.instance.LoadGameScene();
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }
}
