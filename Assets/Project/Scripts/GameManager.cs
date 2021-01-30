using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float tempoPartitona;
    [SerializeField] private bool partitaFinitona;
    [SerializeField] public bool giocoPausato;
    
    void Update()
    {
        if (Input.GetButtonDown("Options"))
        {
            PausaIlGioco();
        }
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
