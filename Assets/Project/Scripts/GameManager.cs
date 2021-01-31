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
    [SerializeField] public bool giocoIniziato;

    private UIManager _uiManager;
    public GameObject tutorial;
    public GameObject countdown;
    public int puntissimi;
    public GameObject[] socks;

    private void Start()
    {
        Time.timeScale = 0;
        _uiManager = FindObjectOfType<UIManager>();
        tempoPartitonaRimasto = tempoPartitonaIniziale;
        puntissimi = 0;
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
        puntissimi += 1;
        bool endgame = true;
        foreach (GameObject calzona in socks)
        {
            if (calzona.activeInHierarchy)
            {
                endgame = false;
            }
        }

        if (endgame == true)
        {
            FinePartitona();
        }
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
        Time.timeScale = 1;
        StartCoroutine(CheIlTempoTorniAScorrere());
    }

    IEnumerator CheIlTempoTorniAScorrere()
    {
        yield return new WaitForSeconds(.5f);
        Debug.Log("il tempo scorre");
        giocoIniziato = true;
    }
}
