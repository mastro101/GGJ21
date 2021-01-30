using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playGameButton;

    private void Start()
    {
        playGameButton = GetComponent<Button>();
        playGameButton.onClick.AddListener(StartNewGame);
    }

    void StartNewGame()
    {
        FindObjectOfType<LoadLevelsManager>().LoadGameScene();
    }
}
