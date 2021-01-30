using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float tempoPartitonaIniziale;
    [SerializeField] private float tempoPartitonaRimasto;
    [SerializeField] private bool partitaFinitona;
    [SerializeField] public bool giocoPausato;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        tempoPartitonaRimasto = tempoPartitonaIniziale;
    }

    void Update()
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

    void FinePartitona()
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
}
