﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float tempoPartitonaIniziale;
    [SerializeField] private float tempoPartitonaRimasto;
    [SerializeField] private bool partitaFinitona;
    [SerializeField] public bool giocoPausato;
    [SerializeField] public bool giocoIniziato;

    private UIManager _uiManager;
    public GameObject tutorial;
    public GameObject countdown;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        tempoPartitonaRimasto = tempoPartitonaIniziale;
    }

    void Update()
    {
        if (giocoIniziato)
        {
            if (tempoPartitonaRimasto > 0 && !partitaFinitona)
            {
                tempoPartitonaRimasto -= 1 * Time.deltaTime;
            }
            else
            {
                partitaFinitona = true;
                FinePartitona();
            }
        
        
            if (Input.GetButtonDown("Options"))
            {
                PausaIlGioco();
            }
        
            _uiManager.RuotaLancettonaEAggiornaScritta(tempoPartitonaRimasto, tempoPartitonaIniziale);
        }
    }

    public void FinePartitona()
    {
        _uiManager.FinePartita();
    }

    void PausaIlGioco()
    {
        if (!giocoPausato)
        {
            giocoPausato = true;
            _uiManager.MostraMenuDiPausa();
            Time.timeScale = 0;
        }
        else
        {
            giocoPausato = false;
            _uiManager.MostraMenuDiPausa();
            Time.timeScale = 1;
        }
    }

    public void PuntonePresone()
    {
        
    }

    public void RicominciaLivelloneEroe()
    {
        LoadLevelsManager.instance.ReloadGameScene();
    }

    public void SeiDeboleETorniAlMenu()
    {
        LoadLevelsManager.instance.FromGameToMainMenu();
    }

    public void IniziaIlGiocone()
    {
        tutorial.SetActive(false);
        countdown.SetActive(true);
        StartCoroutine(CheIlTempoTorniAScorrere());
    }

    IEnumerator CheIlTempoTorniAScorrere()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("il tempo scorre");
        giocoIniziato = true;
    }
}
