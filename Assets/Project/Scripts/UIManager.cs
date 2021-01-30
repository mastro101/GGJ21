using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject uIFinePartitona;
    public GameObject menuDiPausa;
    public GameObject bottoneAssegnato;
    
    public bool hideTimer;
    public bool muteSounds;
    private void Awake() 
    {
        SetSingleton();
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

    public void FinePartita()
    {
        Instantiate(uIFinePartitona);
    }

    public void MostraMenuDiPausa()
    {
        if (FindObjectOfType<GameManager>().giocoPausato)
        {
            menuDiPausa.SetActive(true);
            BottoncioneDaSelezionare();
        }
        else if(!FindObjectOfType<GameManager>().giocoPausato)
        {
            menuDiPausa.SetActive(false);
        }
    }
    
    void BottoncioneDaSelezionare()
    {
        FindObjectOfType<EventSystem>().SetSelectedGameObject(bottoneAssegnato);
    }
}
