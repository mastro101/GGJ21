using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject uIFinePartitona;
    public GameObject menuDiPausa;
    public GameObject bottoneAssegnato;
    
    public bool hideTimer;
    public bool muteSounds;

    public Image lancettona;
    public TextMeshProUGUI testoTempo;

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

    public void RuotaLancettonaEAggiornaScritta(float tempoRimasto, float tempoTotale)
    {
        int tempoRimastoIntero = (int)tempoRimasto;
        testoTempo.text = tempoRimastoIntero.ToString();
        lancettona.rectTransform.rotation = Quaternion.Euler(0,0,0 - (360 * (1 - tempoRimasto / tempoTotale)));
    }
}
