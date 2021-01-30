using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneAfterSeconds : MonoBehaviour
{
    public int seconds;
    void Start()
    {
        Invoke("LoadScene", seconds);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
