using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevelsManager : MonoBehaviour
{
    public static LoadLevelsManager instance;
    public GameObject loadingScreen;
    public Image bar;
    public TextMeshProUGUI tipsText;
    public string[] tipsList;

    private void Awake()
    {
        Application.targetFrameRate = 90;

        instance = this;

        SceneManager.LoadSceneAsync((int)Scenes.MAIN_MENU, LoadSceneMode.Additive);
    }

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();

    public void LoadGameScene()
    {
        loadingScreen.gameObject.SetActive(true);

        StartCoroutine(GenerateTips());

        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)Scenes.MAIN_MENU));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)Scenes.GAME_SCENE, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void ReloadGameScene()
    {
        loadingScreen.gameObject.SetActive(true);

        StartCoroutine(GenerateTips());

        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)Scenes.GAME_SCENE));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)Scenes.GAME_SCENE, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void FromGameToMainMenu()
    {
        loadingScreen.gameObject.SetActive(true);

        StartCoroutine(GenerateTips());

        sceneLoading.Add(SceneManager.UnloadSceneAsync((int)Scenes.GAME_SCENE));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)Scenes.MAIN_MENU, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    float totalSceneProress;
    public IEnumerator GetSceneLoadProgress()
    {
        for(int i=0; i<sceneLoading.Count; i++)
        {
            while(!sceneLoading[i].isDone)
            {
                totalSceneProress = 0;

                foreach(AsyncOperation operation in sceneLoading)
                {
                    totalSceneProress += operation.progress;
                }

                totalSceneProress = (totalSceneProress / sceneLoading.Count);

                bar.fillAmount = Mathf.RoundToInt(totalSceneProress);

                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
    }

    public int tipCount;
    public IEnumerator GenerateTips()
    {
        tipCount = Random.Range(0, tipsList.Length);
        tipsText.text = tipsList[tipCount];
        while(loadingScreen.activeInHierarchy)
        {
            yield return new WaitForSeconds(3f);

            tipCount++;

            if(tipCount >= tipsList.Length)
            {
                tipCount = 0;
            }

            tipsText.text = tipsList[tipCount];
            
        }
    }
  
}
