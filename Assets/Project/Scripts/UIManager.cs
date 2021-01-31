using System;
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
    public GameObject bottoneAssegnatoFine;
    public GameObject bottoneAssegnatoTutorial;
    
    public bool hideTimer;
    public bool muteSounds;

    public Image lancettona;
    public TextMeshProUGUI testoTempo;

    [SerializeField] Image calzonaImmaginona;

    private void Start()
    {
        FindObjectOfType<EventSystem>().SetSelectedGameObject(bottoneAssegnatoTutorial);
    }

    public void FinePartita()
    {
        uIFinePartitona.SetActive(true);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(bottoneAssegnatoFine);
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

    public void DammiImmaginonaCalzaCheLaMostro(Sprite spriteCalza)
    {
        if (calzonaImmaginona)
        {
            if (spriteCalza)
            {
                calzonaImmaginona.sprite = spriteCalza;
                calzonaImmaginona.enabled = true;
            }
            else
                calzonaImmaginona.enabled = false;
        }
    }
}
