﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class PiedoneAcchiappone : MonoBehaviour
{
    public Movement controlleronePiedoni;
    public float tempoPrimaDiUccisione = 0.1f;
    private float timer;
    
    private void OnTriggerEnter(Collider other)
    {
        Props oggettoneColpitone = other.GetComponentInParent<Props>();
        Sock calzaDiDDDDDDDio = other.GetComponent<Sock>();
        Crock ilMostrone = other.GetComponentInParent<Crock>();

        if (oggettoneColpitone != null)
        {
            oggettoneColpitone.CalcioSuperRandom();
        }
        
        if (calzaDiDDDDDDDio != null)
        {
            if (controlleronePiedoni.stiamoCalciando == true)
            {
                calzaDiDDDDDDDio.Calcione();
            }

            else
            {
                controlleronePiedoni.SettaCalzonaPresona(calzaDiDDDDDDDio.index, other.gameObject);
            }
        }

        if (ilMostrone != null)
        {
            if (controlleronePiedoni.stiamoCalciando == true)
            {
                ilMostrone.SpingitoneCrockkone();
            }
            
        }

        if (other.CompareTag("Environment") && controlleronePiedoni.stiamoCalciando == true && controlleronePiedoni.iPiediInchiodatiComeGesu == false)
        {
            controlleronePiedoni.InchiodaPiedoni();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Environment") && controlleronePiedoni.stiamoCalciando == true && controlleronePiedoni.iPiediInchiodatiComeGesu == false)
        {
            controlleronePiedoni.InchiodaPiedoni();
        }
        
        Props oggettoneColpitone = other.GetComponentInParent<Props>();
        Crock ilMostrone = other.GetComponentInParent<Crock>();

        if (oggettoneColpitone != null)
        {
            oggettoneColpitone.CalcioSuperRandom();
        }

        if (ilMostrone == null)
        {
            timer = tempoPrimaDiUccisione;
        }

        if (ilMostrone != null)
        {
            timer -= 0.02f;
            
            if (timer <= 0)
            {
                FindObjectOfType<GameManager>().FinePartitona();
            }
            
            if (controlleronePiedoni.stiamoCalciando == true)
            {
                ilMostrone.SpingitoneCrockkone();
            }
        }
        
    }
}
