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

    private void Start()
    {
        tempoPartitonaRimasto = tempoPartitonaIniziale;
    }

    void Update()
    {
        tempoPartitonaRimasto -= 1 * Time.deltaTime;
        
        
        if (Input.GetButtonDown("Options"))
        {
            PausaIlGioco();
        }
        
        UIManager.instance.RuotaLancettonaEAggiornaScritta(tempoPartitonaRimasto, tempoPartitonaIniziale);
    }

    void FinePartitona()
    {
        UIManager.instance.FinePartita();
    }

    void PausaIlGioco()
    {
        if (!giocoPausato)
        {
            giocoPausato = true;
            UIManager.instance.MostraMenuDiPausa();
            Time.timeScale = 0;
        }
        else
        {
            giocoPausato = false;
            UIManager.instance.MostraMenuDiPausa();
            Time.timeScale = 1;
        }
    }
}
